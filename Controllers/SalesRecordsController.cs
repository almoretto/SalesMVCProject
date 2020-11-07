using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesMVC.Services;

namespace SalesMVC.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _salesRecordService;

        public SalesRecordsController(SalesRecordService salesRecordService) { _salesRecordService = salesRecordService; }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> SimpleSearch(DateTime? minD, DateTime? maxD)
        {
            if (!minD.HasValue)
            {
                minD = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxD.HasValue)
            {
                maxD = DateTime.Now;
            }
            ViewData["minDate"] = minD.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxD.Value.ToString("yyyy-MM-dd");
            var salesFound = await _salesRecordService.FindByDateAsync(minD, maxD);
            return View(salesFound);
        }
        public async Task<IActionResult> GroupingSearch(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = DateTime.Now;
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var salesFound = await _salesRecordService.FindByDateGroupedAsync(minDate, maxDate);
            return View(salesFound);
        }
    }
}
