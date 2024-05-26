using Drink_Wholesale.DTO;

namespace Drink_Wholesale.Admin.Helpers
{
    public static class EnumHelpers
    {
        //public static IEnumerable<SelectListItem> ToSelectList(List<Packaging> input)
        //{
        //    var list = new List<SelectListItem>();
        //    input.Add(Packaging.Single);
        //    input.ForEach(e=> list.Add(new SelectListItem {Value = e.ToString(), Text = e.ToString()}));
        //    return list;
        //}

        //public static List<Packaging> ToEnumList(this Packaging enumObj)
        //{
        //    var allValues = Enum.GetValues(typeof(Packaging));
        //    return allValues.Cast<Packaging>().Where(e => (enumObj & e) > 0).ToList();

        //}

        public static int PackagintToInt(Packaging packaging)
        {
            switch (packaging)
            {
                case Packaging.ShrinkWrap:
                    return 6;
                case Packaging.Tray:
                    return 12;
                case Packaging.Crate:
                    return 24;
                default:
                    return 1;
            }
        }

    }
}
