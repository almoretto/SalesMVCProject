using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SalesMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} field is required!")]
        //On this notation the order of the index is {0} prop, {1} frist param - Max, {2} sec param - Min
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} length must be between {2} and {1}")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} field is required!")]
        [EmailAddress(ErrorMessage = "Enter a Valid Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} field is required!")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} field is required!")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public double BaseSalary { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        //Relation
        public Department Department { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        //Constructor
        public Seller() { }
        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }
        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }
        public double TotalSales(DateTime ini, DateTime fin)
        {
            //This method returns the Sum os the Sales in a period
            return Sales
                .Where(sr => sr.Date >= ini && sr.Date <= fin)
                .Sum(sr => sr.Amount);
        }
    }
}
