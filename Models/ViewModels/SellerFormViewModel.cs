using System.Collections.Generic;

namespace SalesMVC.Models.ViewModels
{
    public class SellerFormViewModel
    {
        public Seller Seller { get; set; }
        public ICollection<Department> Departments { get; set; }

        /*OBS using the name of the properties in this viewmodel class,
         * makes easier for the framwork to interate and automaticaly bind
         * the data to the view*/
    }
}
