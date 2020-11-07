using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesMVC.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //Relation
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        //Constructors
        public Department() { }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public void AddSeller(Seller s)
        {
            Sellers.Add(s);
        }
        public double TotalSales(DateTime ini, DateTime fin)
        {
            return Sellers.Sum(s => s.TotalSales(ini, fin));
        }
    }
}
