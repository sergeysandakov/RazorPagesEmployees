using EmployeesCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmployeesCore.Services
{
    public class MockEmployeeRepository : IEmployeeRepository
    {
        private List<Employee> _employeeList;

        public MockEmployeeRepository()
        {
            _employeeList = new List<Employee>()
            {
                new Employee()
                {
                    Id = 0, Name = "John", Age = 26, Dept = Dept.IT, Email = "Test1@mail.ru", PhotoPath = "avatar1.jpg"
                },
                new Employee()
                {
                    Id = 1, Name = "Mike", Age = 29, Dept = Dept.Payroll, Email = "Test2@mail.ru", PhotoPath = "avatar2.jpg"
                },
                new Employee()
                {
                    Id = 2, Name = "Sam", Age = 22, Dept = Dept.HR, Email = "Test3@mail.ru", PhotoPath = "avatar3.jpg"
                },
                new Employee()
                {
                    Id = 2, Name = "Nick", Age = 22, Dept = Dept.IT, Email = "Test4@mail.ru", PhotoPath = "avatar4.jpg"
                },
                new Employee()
                {
                    Id = 3, Name = "Bob", Age = 22, Dept = Dept.HR, Email = "Test5@mail.ru", PhotoPath = "avatar1.jpg"
                },
                new Employee()
                {
                    Id = 4, Name = "Martin", Age = 22, Dept = Dept.HR, Email = "Test6@mail.ru", PhotoPath = "avatar2.jpg"
                },
                new Employee()
                {
                    Id = 5, Name = "Tom", Age = 22, Dept = Dept.Payroll, Email = "Test7@mail.ru", PhotoPath = "avatar3.jpg"
                },
                new Employee()
                {
                    Id = 6, Name = "Rob", Age = 22, Dept = Dept.None, Email = "Test8@mail.ru", PhotoPath = "avatar4.jpg"
                },
                new Employee()
                {
                    Id = 7, Name = "Artur", Age = 22, Dept = Dept.IT, Email = "Test9@mail.ru", PhotoPath = "avatar1.jpg"
                },
                new Employee()
                {
                    Id = 8, Name = "Sam", Age = 22, Dept = Dept.None, Email = "Test10@mail.ru", PhotoPath = "avatar2.jpg"
                },
            };
        }

        public Employee Add(Employee newEmployee)
        {
            newEmployee.Id = _employeeList.Max(x => x.Id) + 1;
            _employeeList.Add(newEmployee);
            return newEmployee;
        }

        public Employee Delete(int id)
        {
            Employee employeeToDelete = _employeeList.FirstOrDefault(x => x.Id == id);

            if (employeeToDelete != null)
                _employeeList.Remove(employeeToDelete);

            return employeeToDelete;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employeeList;
        }

        public Employee GetEmployee(int id)
        {
            return _employeeList.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Employee> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return _employeeList;

            return _employeeList.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()) || x.Email.ToLower().Contains(searchTerm.ToLower()));
        }

        public Employee Update(Employee updatedEmployee)
        {
            Employee employee = _employeeList.FirstOrDefault(x => x.Id == updatedEmployee.Id);

            if(employee != null)
            {
                employee.Name = updatedEmployee.Name;
                employee.Age = updatedEmployee.Age;
                employee.Dept = updatedEmployee.Dept;
                employee.Email = updatedEmployee.Email;
                employee.PhotoPath = updatedEmployee.PhotoPath;
            }
            return employee;
        }
    }
}
