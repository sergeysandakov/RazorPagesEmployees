using EmployeesCore.Models;
using EmployeesCore.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.IO;

namespace EmployeesGeneral.Pages.Employees
{
    public class EditModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _webHostEnviromment;

        public EditModel(IEmployeeRepository employeeRepository, IWebHostEnvironment webHostEnviromment)
        {
            _employeeRepository = employeeRepository;
            _webHostEnviromment = webHostEnviromment;
        }

        [BindProperty]
        public Employee Employee { get; set; }

        // TODO : 1
        [BindProperty]
        public IFormFile Photo { get; set; }

        [BindProperty]
        public bool Notify { get; set; }

        public string Message { get; set; }

        public IActionResult OnGet(int? id)
        {
            if (id.HasValue)
                Employee = _employeeRepository.GetEmployee(id.Value);
            else
                Employee = new Employee();

            if(Employee == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    if (Employee.PhotoPath != null)
                    {
                        string filePath = Path.Combine(_webHostEnviromment.WebRootPath, "images", Employee.PhotoPath);

                        if(Employee.PhotoPath != "noimage.png")
                            System.IO.File.Delete(filePath);
                    }

                    Employee.PhotoPath = ProcessUploadedFile();
                }
                if (Employee.Id > 0)
                {
                    Employee = _employeeRepository.Update(Employee);
                    TempData["SuccessMessage"] = $"Update {Employee.Name} Successfull";
                }
                else
                {
                    Employee = _employeeRepository.Add(Employee);
                    TempData["SuccessMessage"] = $"Adding {Employee.Name} Successfull";
                }
                return RedirectToPage("Employees");
            }
            return Page();
        }

        public void OnPostUpdateNotificationPreferences(int id)
        {
            if (Notify)
                Message = "Thank you for turning on notifications";
            else
                Message = "You have turned off email notifications";

            Employee = _employeeRepository.GetEmployee(id);
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;

            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnviromment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fs);
                }
            }

            return uniqueFileName;
        }
    }
}
