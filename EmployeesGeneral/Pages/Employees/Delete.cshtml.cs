using EmployeesCore.Models;
using EmployeesCore.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace EmployeesGeneral.Pages.Employees
{
    public class DeleteModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _webHostEnviromment;

        public DeleteModel(IEmployeeRepository employeeRepository, IWebHostEnvironment webHostEnviromment)
        {
            _employeeRepository = employeeRepository;
            _webHostEnviromment = webHostEnviromment;
        }

        [BindProperty]
        public Employee Employee { get; set; }

        public IActionResult OnGet(int id)
        {
            Employee = _employeeRepository.GetEmployee(id);

            if (Employee == null)
                RedirectToPage("/NotFound");

            return Page();
        }

        public IActionResult OnPost()
        {
            Employee deletedEmployee = _employeeRepository.Delete(Employee.Id);

            if (deletedEmployee.PhotoPath != null)
            {
                string filePath = Path.Combine(_webHostEnviromment.WebRootPath, "images", deletedEmployee.PhotoPath);

                if (deletedEmployee.PhotoPath != "noimage.png")
                    System.IO.File.Delete(filePath);
            }

            if (deletedEmployee == null)
                return RedirectToPage("/NotFound");

            return RedirectToPage("Employees");
        }
    }
}
