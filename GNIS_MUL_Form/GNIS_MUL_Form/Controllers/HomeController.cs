using GNIS_MUL_Form.Models;
using GNIS_MUL_Form.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GNIS_MUL_Form.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new MeetingMinutesViewModel
            {
                ProductsServices = await _context.ProductsServiceTbls
                    .Select(p => new ProductServiceViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Unit = p.Unit
                    }).ToListAsync()
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<JsonResult> GetCustomers(string type)
        {
            var customers = type == "corporate"
                ? await _context.CorporateCustomerTbls
                    .Select(c => new { value = c.Id, text = c.Name })
                    .ToListAsync()
                : await _context.IndividualCustomerTbls
                    .Select(c => new { value = c.Id, text = c.Name })
                    .ToListAsync();

            return Json(customers);
        }

        [HttpGet]
        public async Task<JsonResult> GetProducts()
        {
            var products = await _context.ProductsServiceTbls
                .Select(p => new ProductServiceViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Unit = p.Unit
                }).ToListAsync();

            return Json(products);
        }

        [HttpPost]
        public async Task<IActionResult> SaveMeetingMinutes(MeetingMinutesViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newMasterIdParam = new SqlParameter
                    {
                        ParameterName = "@NewMasterId",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Output
                    };

                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC Meeting_Minutes_Master_Save_SP @CustomerType, @CustomerId, @Date, @MeetingPlace, @AttendsClient, @AttendsHost, @MeetingAgenda, @MeetingDiscussion, @MeetingDecision, @NewMasterId OUTPUT",
                        new SqlParameter("@CustomerType", model.SelectedCustomerType),
                        new SqlParameter("@CustomerId", model.SelectedCustomerId),
                        new SqlParameter("@Date", model.MeetingDate),
                        new SqlParameter("@MeetingPlace", model.MeetingPlace),
                        new SqlParameter("@AttendsClient", model.AttendsClient),
                        new SqlParameter("@AttendsHost", model.AttendsHost),
                        new SqlParameter("@MeetingAgenda", model.MeetingAgenda),
                        new SqlParameter("@MeetingDiscussion", model.MeetingDiscussion),
                        new SqlParameter("@MeetingDecision", model.MeetingDecision),
                        newMasterIdParam
                    );

                    var newMasterId = (int)newMasterIdParam.Value;

                    foreach (var detail in model.MeetingMinutesDetails)
                    {
                        await _context.Database.ExecuteSqlRawAsync(
                            "EXEC Meeting_Minutes_Details_Save_SP @MeetingMasterId, @ProductServiceId, @Quantity, @Unit",
                            new SqlParameter("@MeetingMasterId", newMasterId),
                            new SqlParameter("@ProductServiceId", detail.ProductServiceId),
                            new SqlParameter("@Quantity", detail.Quantity),
                            new SqlParameter("@Unit", detail.Unit)
                        );
                    }
                    TempData["SuccessMessage"] = "Meeting minutes saved successfully.";

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "An error occurred while saving the data.");

                    model.ProductsServices = await _context.ProductsServiceTbls
                        .Select(p => new ProductServiceViewModel
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Unit = p.Unit
                        }).ToListAsync();

                    return View("Index", model);
                }
            }

            model.ProductsServices = await _context.ProductsServiceTbls
                .Select(p => new ProductServiceViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Unit = p.Unit
                }).ToListAsync();

            return View("Index", model);
        }
    }
}
