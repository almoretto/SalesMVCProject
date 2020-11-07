using System;
using System.ComponentModel.DataAnnotations;
using SalesMVC.Models.Enums;

namespace SalesMVC.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString ="{0: dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        public double Amount { get; set; }
        public SalesStatus Status { get; set; }

        //Relation
        public Seller Seller { get; set; }

        public SalesRecord() { }

        public SalesRecord(int id, DateTime date, double amount, SalesStatus status, Seller seller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
        }
    }
}
