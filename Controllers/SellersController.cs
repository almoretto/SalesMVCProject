using Microsoft.AspNetCore.Mvc;
using SalesMVC.Models;
using SalesMVC.Services;
using SalesMVC.Models.ViewModels;
using System.Diagnostics;
using System.Threading.Tasks;
using System;

namespace SalesMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }
        public async Task<IActionResult> Index() //IActionResult is the type of all method in control that calls a view
        {
            var sellersList = await _sellerService.FindAllAsync();
            //Using the deendency injection from Services the Controller gets the data from model and passes to the view as parameter
            //The IActionResult will generate in index the result with the list
            return View(sellersList);
        }
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel); //Simple call to view Create 
        }

        [HttpPost] //Anotation indicating that the method is POST not GET
        [ValidateAntiForgeryToken] //Prevent Cross-Site Request Forgery (XSRF/CSRF) attacks in ASP.NET Core
        public async Task<IActionResult> Create(Seller seller)
        {
            try
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };

                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }
                if (await TryUpdateModelAsync<Seller>(
                    seller,
                    "Seller",
                    s => s.Name,
                    s => s.Email,
                    s => s.BirthDate,
                    s => s.BaseSalary,
                    s => s.DepartmentId))
                {
                    await _sellerService.InsertAsync(seller);//Calling the method Insert and passing the content of the form post
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(viewModel);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException("Create with error: " + ex.Message);
            }
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var slr = await _sellerService.FindByIdAsync(id.Value);
            if (slr == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Seller not Found" });
            }
            return View(slr);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var slr = await _sellerService.FindByIdAsync(id.Value);
            if (slr == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Seller not Found" });
            }
            return View(slr);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var slr = await _sellerService.FindByIdAsync(id.Value);

            if (slr == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Seller not Found" });
            }

            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Seller = slr, Departments = departments };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            try
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };

                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }
                if (await TryUpdateModelAsync<Seller>(
                    seller,
                    "Seller",
                    s => s.Name,
                    s => s.Email,
                    s => s.BirthDate,
                    s => s.BaseSalary,
                    s => s.DepartmentId))
                {
                    await _sellerService.UpdateAsync(seller);//Calling the method Insert and passing the content of the form post
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(viewModel);
                }
            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException("Create with error: " + ex.Message);
            }
        }
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }



    }
}
