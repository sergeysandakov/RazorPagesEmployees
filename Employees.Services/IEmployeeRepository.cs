using EmployeesCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeesCore.Services
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> Search(string searchTerm);
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployee(int id);
        Employee Delete(int id);
        Employee Update(Employee updatedEmployee);
        Employee Add(Employee newEmployee);
    }
}
