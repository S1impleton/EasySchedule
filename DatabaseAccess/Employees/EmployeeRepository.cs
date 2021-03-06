﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Core;
using System.Transactions;

namespace DatabaseAccess.Employees
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public List<Employee> GetAllEmployees()
        {
            try
            {
                List<Employee> employees = new List<Employee>();

                using (SqlConnection connection = new DbConnection().GetConnection())
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM Employee";
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Employee employee = BuildEmployeeObject(reader);
                                employees.Add(employee);
                            }
                        }
                    }
                }
                return employees;
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong while retriving all employees! Try again");
            }
        }

        public Employee GetEmployeeById(int id)
        {
            Employee employee = new Employee();
            using (SqlConnection connection = new DbConnection().GetConnection())
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Employee WHERE Employee.id = @param1;";
                    SqlParameter p1 = new SqlParameter(@"param1", SqlDbType.Int);
                    p1.Value = id;
                    command.Parameters.Add(p1);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employee = BuildEmployeeObject(reader);
                        }
                    }
                }
            }
            return employee;
        }

        public Employee GetEmployeeByUsername(string username)
        {
            Employee employee = new Employee();
            using (SqlConnection connection = new DbConnection().GetConnection())
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Employee WHERE Employee.username = @param1;";
                    SqlParameter p1 = new SqlParameter(@"param1", SqlDbType.NVarChar);
                    p1.Value = username;
                    command.Parameters.Add(p1);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employee = BuildEmployeeObject(reader);
                        }
                    }
                }
            }
            return employee;
        }

        public string GetSaltByEmployee(Employee employee)
        {
            string salt = null;
            using (SqlConnection connection = new DbConnection().GetConnection())
            {
                using (SqlCommand commmand = connection.CreateCommand())
                {
                    commmand.CommandText = "SELECT salt FROM Employee where Id = @param1";
                    commmand.Parameters.AddWithValue("@param1", employee.Id);
                    using (SqlDataReader reader = commmand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            salt = reader.GetString(0);
                        }
                    }
                }
            }
            return salt;
        }

        public List<Employee> GetEmployeesByDepartmentId(int departmentId)
        {
            try
            {
                List<Employee> employees = new List<Employee>();
                using (SqlConnection connection = new DbConnection().GetConnection())
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT * FROM Employee WHERE Employee.departmentId = @param1;";
                        SqlParameter p1 = new SqlParameter(@"param1", SqlDbType.Int, 100);
                        p1.Value = departmentId;
                        command.Parameters.Add(p1);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Employee employee = BuildEmployeeObject(reader);
                                employees.Add(employee);
                            }
                        }
                    }
                }
                return employees;
            }
            catch (Exception)
            {
                throw new Exception("Something went wrong! Try again.");
            }
        }

        public void InsertEmployee(Employee employee)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection connection = new DbConnection().GetConnection())
                    {
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.CommandText =
                                "insert into Employee(name, email, phone, noOfHours, isAdmin, " +
                                "         username, password, departmentId, isEmployed, salt) " +
                                "values (@param1, @param2, @param3, @param4, @param5, " +
                                "        @param6, @param7, @param8, @param9, @param10);";

                            command.Parameters.AddWithValue("@param1", employee.Name);
                            command.Parameters.AddWithValue("@param2", employee.Email);
                            command.Parameters.AddWithValue("@param3", employee.Phone);
                            command.Parameters.AddWithValue("@param4", employee.NoOfHours);
                            command.Parameters.AddWithValue("@param5", employee.IsAdmin);
                            command.Parameters.AddWithValue("@param6", employee.Username);
                            command.Parameters.AddWithValue("@param7", employee.Password);
                            command.Parameters.AddWithValue("@param8", employee.DepartmentId);
                            command.Parameters.AddWithValue("@param9", employee.IsEmployed);
                            command.Parameters.AddWithValue("@param10", employee.Salt);

                            command.ExecuteNonQuery();

                            scope.Complete();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong while inserting an employee into the database! Try again");
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection connection = new DbConnection().GetConnection())
                    {
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.CommandText =
                                "UPDATE Employee Set name = @param1, email = @param2, phone = @param3, noOfHours = @param4, " +
                                "isAdmin = @param5, username = @param6, password = @param7, departmentId = @param8, isEmployed = @param9, salt = @param10 " +
                                "WHERE employee.id = @param11 AND RV = @param12";

                            SqlParameter p1 = new SqlParameter(@"param1", SqlDbType.VarChar);
                            SqlParameter p2 = new SqlParameter(@"param2", SqlDbType.VarChar);
                            SqlParameter p3 = new SqlParameter(@"param3", SqlDbType.VarChar);
                            SqlParameter p4 = new SqlParameter(@"param4", SqlDbType.Int);
                            SqlParameter p5 = new SqlParameter(@"param5", SqlDbType.Bit);
                            SqlParameter p6 = new SqlParameter(@"param6", SqlDbType.VarChar);
                            SqlParameter p7 = new SqlParameter(@"param7", SqlDbType.VarChar);
                            SqlParameter p8 = new SqlParameter(@"param8", SqlDbType.Int);
                            SqlParameter p9 = new SqlParameter(@"param9", SqlDbType.Bit);
                            SqlParameter p10 = new SqlParameter(@"param10", SqlDbType.VarChar);
                            SqlParameter p11 = new SqlParameter(@"param11", SqlDbType.Int);


                            p1.Value = employee.Name;
                            p2.Value = employee.Email;
                            p3.Value = employee.Phone;
                            p4.Value = employee.NoOfHours;
                            p5.Value = employee.IsAdmin;
                            p6.Value = employee.Username;
                            p7.Value = employee.Password;
                            p8.Value = employee.DepartmentId;
                            p9.Value = employee.IsEmployed;
                            p10.Value = employee.Salt;
                            p11.Value = employee.Id;
                            command.Parameters.AddWithValue("@param12", employee.RowVersion);

                            command.Parameters.Add(p1);
                            command.Parameters.Add(p2);
                            command.Parameters.Add(p3);
                            command.Parameters.Add(p4);
                            command.Parameters.Add(p5);
                            command.Parameters.Add(p6);
                            command.Parameters.Add(p7);
                            command.Parameters.Add(p8);
                            command.Parameters.Add(p9);
                            command.Parameters.Add(p10);
                            command.Parameters.Add(p11);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                throw new DataInInvalidStateException();
                            }

                            scope.Complete();
                        }
                    }
                }
            }
            catch (DataInInvalidStateException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong while updating an employee! Try again");
            }
        }

        private Employee BuildEmployeeObject(SqlDataReader reader)
        {
            Employee employee = new Employee();
            employee.Id = Convert.ToInt32(reader["id"].ToString());
            employee.Name = reader["name"].ToString();
            employee.Email = reader["email"].ToString();
            employee.Phone = reader["phone"].ToString();
            employee.NoOfHours = Convert.ToInt32(reader["noOfHours"].ToString());
            employee.IsAdmin = reader.GetBoolean(5);
            employee.Username = reader["username"].ToString();
            employee.Password = reader["password"].ToString();
            employee.DepartmentId = Convert.ToInt32(reader["departmentId"].ToString());
            employee.Salt = reader["salt"].ToString();
            employee.IsEmployed = reader.GetBoolean(10);
            employee.RowVersion = reader.GetFieldValue<byte[]>(11);
            return employee;
        }
    }
}
