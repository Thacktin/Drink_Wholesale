using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

        private ObservableCollection<CategoryViewModel> _categories;
        private ObservableCollection<SubCategoryViewModel> _subcategories;
        private ObservableCollection<ProductViewModel> _products;
        private CategoryViewModel _selectedCategory;
        private SubCategoryViewModel _selectedSubCategory;
        #region Public Getter/Setters



        public ObservableCollection<CategoryViewModel> Categories
        {
            get => _categories;
            set { _categories = value; OnPropertyChanged();}
        }

        public ObservableCollection<SubCategoryViewModel> SubCategories
        {
            get => _subcategories;
            set { _subcategories = value; OnPropertyChanged(); }
        }

        public ObservableCollection<ProductViewModel> Products
        {
            get => _products;
            set { _products = value; OnPropertyChanged(); }
        }

        public CategoryViewModel SelectedCategory
        {
            get => _selectedCategory;
            set { _selectedCategory = value; OnPropertyChanged(); }
        }      
        public SubCategoryViewModel SelectedSubCategory
        {
            get => _selectedSubCategory;
            set { _selectedSubCategory = value; OnPropertyChanged(); }
        }
        #endregion

        #region Commands

        public DelegateCommand SelectCategoryCommand { get; private set; }
        public DelegateCommand RefreshCategoriesCommand { get; private set; }

        public DelegateCommand SelectSubCategoryCommand { get; private set; }
        public DelegateCommand AddingNewSubCategoryCommand { get; private set; }

        #endregion

        public MainViewModel(DrinkWholesaleAPIService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;

            RefreshCategoriesCommand = new DelegateCommand(async _ => await LoadCategoriesAsync());
            SelectCategoryCommand = new DelegateCommand(async _ => await LoadSubCategoriesAsync(SelectedCategory));

            SelectSubCategoryCommand = new DelegateCommand(async _ => await LoadProductsAsync(SelectedSubCategory));
            AddingNewSubCategoryCommand =
                new DelegateCommand(param => AddingNewSubCategory(param as AddingNewItemEventArgs));


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

        private async void SubCategoryViewModel_EditEnded(object sender ,EventArgs e)
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
            catch (Exception ex) when(ex is NetworkException || ex is HttpRequestException)
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
