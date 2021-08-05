using EmployeesCore.Models;
using EmployeesCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeesGeneral.Pages.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly IEmployeeRepository _db;

        public DetailsModel(IEmployeeRepository db)
        {
            _db = db;
        }

        public Employee Employee { get; private set; }

        public IActionResult OnGet(int id)
        {
            Employee = _db.GetEmployee(id);

            if(Employee == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }
    }
}
