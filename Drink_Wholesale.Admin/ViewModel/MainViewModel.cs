using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Printing.Interop;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using AutoMapper;
using Drink_Wholesale.Admin.Model;
using Drink_Wholesale.Desktop.Model;
using Drink_Wholesale.DTO;
using ELTE.TodoList.Desktop.ViewModel;

namespace Drink_Wholesale.Admin.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly DrinkWholesaleAPIService _service;
        private readonly IMapper _mapper;

        private ObservableCollection<CategoryViewModel> _categories = null!;
        private ObservableCollection<SubCategoryViewModel> _subcategories = null!;
        private ObservableCollection<ProductViewModel> _products = null!;
        private CategoryViewModel _selectedCategory = null!;
        private SubCategoryViewModel _selectedSubCategory = null!;
        private ProductViewModel _selecteProduct = null!;

        #region Public Getter/Setters



        public ObservableCollection<CategoryViewModel> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<SubCategoryViewModel> SubCategories
        {
            get => _subcategories;
            set
            {
                _subcategories = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ProductViewModel> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        public CategoryViewModel SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
            }
        }

        public SubCategoryViewModel SelectedSubCategory
        {
            get => _selectedSubCategory;
            set
            {
                _selectedSubCategory = value;
                OnPropertyChanged();
            }
        }

        public ProductViewModel SelectedProduct
        {
            get => _selecteProduct;
            set
            {
                _selecteProduct = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Commands

        public DelegateCommand SelectCategoryCommand { get; private set; }
        public DelegateCommand RefreshCategoriesCommand { get; private set; }

        public DelegateCommand SelectSubCategoryCommand { get; private set; }
        public DelegateCommand AddingNewSubCategoryCommand { get; private set; }
        //public DelegateCommand SubcategoryDeleteRequestCommand { get; private set; }

        public DelegateCommand EditProductCommand { get; private set; }
        public DelegateCommand CancelProductEditCommand { get; private set; }
        public DelegateCommand SaveProductEditCommand { get; private set; }
        public DelegateCommand AddProductCommand { get; private set; }
        public DelegateCommand OpenOrdersWindowCommand { get; private set; }

        #endregion

        #region Event Handlers

        public event EventHandler StartingProductEdit;
        public event EventHandler FinishingProductEdit;
        public event EventHandler RequestingOrdersWindow;
        #endregion

        public MainViewModel(DrinkWholesaleAPIService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;

            RefreshCategoriesCommand = new DelegateCommand(async _ => await LoadCategoriesAsync());
            SelectCategoryCommand = new DelegateCommand(async _ => await LoadSubCategoriesAsync(SelectedCategory));

            SelectSubCategoryCommand = new DelegateCommand(async _ => await LoadProductsAsync(SelectedSubCategory));
            //SubcategoryDeleteRequestCommand =
            //    new DelegateCommand(param => SubcategoryDeleteRequest(param as RoutedEventArgs));
            AddingNewSubCategoryCommand =
                new DelegateCommand(param => AddingNewSubCategory(param as AddingNewItemEventArgs));

            EditProductCommand = new DelegateCommand(
                _ => SelectedProduct is not null,
                async _ => StartEditProduct());
            CancelProductEditCommand = new DelegateCommand(_ => CancelProductEdit());
            SaveProductEditCommand = new DelegateCommand(async _ => await SaveProductEditAsync());
            AddProductCommand = new DelegateCommand(
                _=> SelectedSubCategory is not null,
                _ => AddProduct());
            OpenOrdersWindowCommand = new DelegateCommand(_ => RequestingOrdersWindow?.Invoke(this, EventArgs.Empty));
            //SaveProductEditCommand = new DelegateCommand(
            //    _ => string.IsNullOrEmpty(SelectedProduct?[nameof(ProductViewModel.Inventory)]),
            //    async _ => await SaveProductEditAsync());
            //AddingNewProductCommand = new DelegateCommand(param => StartEditProduct(param as AddingNewItemEventArgs));
            //EditProductCommand = new DelegateCommand(param => EditProduct(param as AddingNewItemEventArgs));

        }

        private void CancelProductEdit()
        {
            if (SelectedProduct is null || !SelectedProduct.IsDirty)
            {
                return;
            }

            if (SelectedProduct.Id == 0)
            {
                Products.Remove(SelectedProduct);
                SelectedProduct = null;

            }
            else
            {
                SelectedProduct.CancelEdit();
            }
            FinishingProductEdit?.Invoke(this, EventArgs.Empty);
        }

        private async Task SaveProductEditAsync()
        {
            try{
                if (SelectedProduct.Id == 0)
                {
                    var productDto = _mapper.Map<ProductDto>(SelectedProduct);
                    await _service.CreateProductAsync(productDto);
                    SelectedProduct.Id = productDto.Id;
                    SelectedProduct.EndEdit();
                }
                else
                {
                    await _service.UpdateProductAsync(_mapper.Map<ProductDto>(SelectedProduct));
                    SelectedProduct.EndEdit();
                    if (SelectedProduct.SubCategoryId != SelectedSubCategory.Id)
                    {
                        Products.Remove(SelectedProduct);
                        SelectedProduct = null;
                    }
                }
                FinishingProductEdit?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception ex) when(ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occurred! ({ex.Message})");
            }
        }

        private void AddProduct()
        {
            var newProduct = new ProductViewModel();
            Products.Add(newProduct);
            SelectedProduct = newProduct;
            StartEditProduct();
        }

        private void StartEditProduct()
        {
           SelectedProduct.BeginEdit();
           StartingProductEdit?.Invoke(this, EventArgs.Empty);

        }

        private void SubcategoryDeleteRequest(RoutedEventArgs? e)
        {

        }

        private async Task LoadProductsAsync(SubCategoryViewModel subCategory)
        {
            if (subCategory is null || subCategory.Id == 0)
            {
                Products = null;
                return;
            }

            try
            {
                Products = new ObservableCollection<ProductViewModel>((await _service.LoadProductsAsync(subCategory.Id)).Select(product =>
                {
                    var productVm = _mapper.Map<ProductViewModel>(product);
                    return productVm;
                }));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occurred! ({ex.Message})");
            }
        }

        private void AddingNewSubCategory(AddingNewItemEventArgs e)
        {
            var subCatVm = new SubCategoryViewModel();
            subCatVm.EditEnded += SubCategoryViewModel_EditEnded;
            e.NewItem = subCatVm;
            //throw new NotImplementedException();
        }

        private async void SubCategoryViewModel_EditEnded(object sender, EventArgs e)
        {
            try
            {
                var listVm = sender as SubCategoryViewModel;
                if (listVm.Id == 0)
                {
                    var subCategoryDto = _mapper.Map<SubCategoryDto>(listVm);
                    subCategoryDto.CategoryId = SelectedCategory.Id;
                    subCategoryDto.Products = new();
                    await _service.CreateSubCategoryAsync(subCategoryDto);
                    listVm.Id = subCategoryDto.Id;
                }
                else
                {
                    //await _service.UpdateSubCategoryAsync(_mapper.Map<SubCategoryDto>(listVm));
                }
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occurred! ({ex.Message})");
            }
        }

        private async Task LoadSubCategoriesAsync(CategoryViewModel category)
        {
            if (category is null || category.Id == 0)
            {
                SubCategories = null;
                return;
            }

            try
            {
                SubCategories = new ObservableCollection<SubCategoryViewModel>((await _service.LoadSubCategoriesAsync(category.Id)).Select(subCategory =>
                {
                    var subCatvm = _mapper.Map<SubCategoryViewModel>(subCategory);
                    return subCatvm;
                }));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occurred! ({ex.Message})");
            }
        }

        #region Category

        public async Task LoadCategoriesAsync()
        {
            try
            {
                Categories = new ObservableCollection<CategoryViewModel>((await _service.LoadCategoriesAsync()).Select(category =>
                {
                    var catvm = _mapper.Map<CategoryViewModel>(category);
                    return catvm;
                }));
            }
            catch (Exception ex)

                when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occurred! ({ex.Message})");
            }
        }

        #endregion
    }
}
