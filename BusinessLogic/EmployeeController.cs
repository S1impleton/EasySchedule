﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using DatabaseAccess;
using DatabaseAccess.Employees;

namespace BusinessLogic
{
    public class EmployeeController : IEmployeeController
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public List<Employee> GetAllEmployees()
        {
            return _employeeRepository.GetAllEmployees();
        }

        public Employee GetEmployeeByUsername(string username)
        {
            return _employeeRepository.GetEmployeeByUsername(username);
        }

        public List<Employee> GetEmployeesByDepartmentId(int departmentId)
        {
            return _employeeRepository.GetEmployeesByDepartmentId(departmentId);
        }

        public Employee ValidatePassword(string username, string password)
        {
            Employee employee = GetEmployeeByUsername(username);
            string salt = _employeeRepository.GetSaltFromEmployeePassword(employee);
            password = PasswordHashing.HashPassword(salt+password);
            bool res;
            res = employee.Password.Equals(password);
            if (res)
            {
                return employee;
            }
            return null;
        }

        public void InsertEmployee(Employee employee)
        {
            _employeeRepository.InsertEmployee(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            _employeeRepository.UpdateEmployee(employee);
        }

    }
}
