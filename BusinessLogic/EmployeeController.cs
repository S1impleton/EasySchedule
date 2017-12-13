﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using DatabaseAccess;
using DatabaseAccess.Employees;
using System.Text.RegularExpressions;
using System.Transactions;

namespace BusinessLogic
{
    public class EmployeeController : IEmployeeController
    {
        private InputValidator _inputValidator;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController()
        {
            _employeeRepository = new EmployeeRepository();
            _inputValidator = new InputValidator();
        }

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _inputValidator = new InputValidator();
        }


        public List<Employee> GetAllEmployees()
        {
            return _employeeRepository.GetAllEmployees();
        }

        public Employee GetEmployeeById(int id)
        {
            return _employeeRepository.GetEmployeeById(id);
        }

        public Employee GetEmployeeByUsername(string username)
        {
            return _employeeRepository.GetEmployeeByUsername(username);
        }

        public List<Employee> GetEmployeesByDepartmentId(int departmentId)
        {
            return _employeeRepository.GetEmployeesByDepartmentId(departmentId);
        }

        /// <summary>
        /// This method validates a password. Its called upon login.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Employee ValidatePassword(string username, string password)
        {
            Employee employee = GetEmployeeByUsername(username);
            string salt = _employeeRepository.GetSaltByEmployee(employee);
            password = PasswordHashing.HashPassword(salt + password);
            if (employee.Password.Equals(password))
            {
                return employee;
            }
            return null;
        }

        public void InsertEmployee(Employee employee)
        {
            employee.Salt = PasswordHashing.GenerateSalt();
            employee.Password = PasswordHashing.HashPassword(employee.Salt + employee.Password);

            if (ValidateEmployeeObject(employee))
            {
                _employeeRepository.InsertEmployee(employee);
            }
            else
            {
                throw new ArgumentException("An error regarding input checks has arrised. Please check that the inputs are valid.");
            }
        }

        public void UpdateEmployee(Employee employee)
        {

            if (!employee.Password.Equals(GetEmployeeByUsername(employee.Username).Password))
            {
                employee.Salt = PasswordHashing.GenerateSalt();
                employee.Password = PasswordHashing.HashPassword(employee.Salt + employee.Password);
            }
            if (ValidateEmployeeObject(employee))
            {
                _employeeRepository.UpdateEmployee(employee);
            }
            else
            {
                throw new ArgumentException("An error regarding input checks has arrised. Please check that the inputs are valid.");
            }

        }

        private bool ValidateEmployeeObject(Employee employee)
        {
            bool isCorrectFormatted = true;

            if (!Regex.IsMatch(employee.Name, _inputValidator.NameCheck))
            {
                isCorrectFormatted = false;
            }
            else if (!Regex.IsMatch(employee.Phone, _inputValidator.PhoneCheck))
            {
                isCorrectFormatted = false;
            }
            else if (!Regex.IsMatch(employee.Email, _inputValidator.EmailCheck))
            {
                isCorrectFormatted = false;
            }
            else if (!Regex.IsMatch(employee.Username, _inputValidator.UsernameCheck))
            {
                isCorrectFormatted = false;
            }
            else if (!(employee.NoOfHours >= 0))
            {
                isCorrectFormatted = false;
            }

            return isCorrectFormatted;
        }

    }
}
