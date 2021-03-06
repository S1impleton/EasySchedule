﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using Core;
using DatabaseAccess.Employees;

namespace DatabaseAccess.ScheduleShifts
{
    public class ScheduleShiftRepository : IScheduleShiftRepository
    {
        public ScheduleShift GetShiftById(int id)
        {
            ScheduleShift shift = new ScheduleShift();
            using (SqlConnection connection = new DbConnection().GetConnection())
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM ScheduleShift WHERE id=@param1;";
                    SqlParameter p1 = new SqlParameter(@"param1", SqlDbType.Int);
                    p1.Value = id;
                    command.Parameters.Add(p1);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            shift = (BuildShiftObject(reader));
                        }
                    }
                }
            }
            return shift;
        }

        public List<ScheduleShift> GetShiftsByScheduleId(int scheduleId)
        {
            List<ScheduleShift> scheduleShifts = new List<ScheduleShift>();

            using (SqlConnection connection = new DbConnection().GetConnection())
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM ScheduleShift WHERE ScheduleShift.scheduleId = @param1;";
                    SqlParameter p1 = new SqlParameter(@"param1", SqlDbType.Int);
                    p1.Value = scheduleId;
                    command.Parameters.Add(p1);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            scheduleShifts.Add(BuildShiftObject(reader));
                        }
                    }
                }
            }
            return scheduleShifts;
        }

        public List<ScheduleShift> GetShiftsByEmployeeId(int employeeId)
        {
            List<ScheduleShift> scheduleShifts = new List<ScheduleShift>();

            using (SqlConnection connection = new DbConnection().GetConnection())
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM ScheduleShift WHERE ScheduleShift.employeeId = @param1;";
                    SqlParameter p1 = new SqlParameter(@"param1", SqlDbType.Int);
                    p1.Value = employeeId;
                    command.Parameters.Add(p1);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            scheduleShifts.Add(BuildShiftObject(reader));
                        }
                    }
                }
            }
            return scheduleShifts; ;
        }

        public void AddShiftsFromSchedule(Schedule schedule)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection connection = new DbConnection().GetConnection())
                    {
                        foreach (ScheduleShift shift in schedule.Shifts)
                        {
                            if (shift.Id == 0)
                            {
                                using (SqlCommand command = connection.CreateCommand())
                                {
                                    command.CommandText =
                                        "INSERT INTO ScheduleShift(startTime, hours, scheduleId, employeeId, isForSale) " +
                                        "VALUES (@param1, @param2, @param3, @param4, @param5)";

                                    SqlParameter p1 = new SqlParameter(@"param1", SqlDbType.DateTime);
                                    SqlParameter p2 = new SqlParameter(@"param2", SqlDbType.Float);
                                    SqlParameter p3 = new SqlParameter(@"param3", SqlDbType.Int);
                                    SqlParameter p4 = new SqlParameter(@"param4", SqlDbType.Int);
                                    SqlParameter p5 = new SqlParameter(@"param5", SqlDbType.Bit);


                                    p1.Value = shift.StartTime;
                                    p2.Value = shift.Hours;
                                    p3.Value = schedule.Id;
                                    p4.Value = shift.Employee.Id;
                                    p5.Value = shift.IsForSale;

                                    command.Parameters.Add(p1);
                                    command.Parameters.Add(p2);
                                    command.Parameters.Add(p3);
                                    command.Parameters.Add(p4);
                                    command.Parameters.Add(p5);

                                    command.ExecuteNonQuery();
                                }
                            }
                            else
                            {
                                UpdateScheduleShift(shift, schedule.Id, connection);
                            }
                        }
                    }
                    scope.Complete();
                }
            }
            catch (DataInInvalidStateException e)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong In AddShiftsFromScheduleToDB!" + e.Message);
            }
        }

        private void UpdateScheduleShift(ScheduleShift shift, int scheduleId, SqlConnection connection)
        {
            try
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE ScheduleShift SET startTime = @param1, hours = @param2, " +
                        "scheduleId = @param3, IsForSale = @param4 WHERE ScheduleShift.id = @param5 AND RV = @param6";

                    SqlParameter p1 = new SqlParameter("@param1", SqlDbType.DateTime, 100);
                    SqlParameter p2 = new SqlParameter("@param2", SqlDbType.Float);
                    SqlParameter p3 = new SqlParameter("@param3", SqlDbType.Int);
                    SqlParameter p4 = new SqlParameter("@param4", SqlDbType.Bit);
                    SqlParameter p5 = new SqlParameter("@param5", SqlDbType.Int);
                    command.Parameters.AddWithValue("@param6", shift.RowVersion);

                    p1.Value = shift.StartTime;
                    p2.Value = shift.Hours;
                    p3.Value = scheduleId;
                    p4.Value = shift.IsForSale;
                    p5.Value = shift.Id;

                    command.Parameters.Add(p1);
                    command.Parameters.Add(p2);
                    command.Parameters.Add(p3);
                    command.Parameters.Add(p4);
                    command.Parameters.Add(p5);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected == 0)
                    {
                        throw new DataInInvalidStateException();
                    }
                }
            }
            catch (DataInInvalidStateException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong in UpdateShifts!" + e.Message);
            }
        }

        /// <summary>
        /// This method will set the chosen Shift that an employee has picked, for "sale" in the System.
        /// This means that other employees will be able to take this shifts, and make it theirs.
        /// </summary>
        /// <param name="scheduleShift"></param>
        public void SetScheduleShiftForSale(ScheduleShift scheduleShift)
        {
            try
            {
                using (SqlConnection connection = new DbConnection().GetConnection())
                {
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "UPDATE ScheduleShift SET IsForSale = @param1 WHERE Id = @param2";
                        command.Parameters.AddWithValue("@param1", true);
                        command.Parameters.AddWithValue("@param2", scheduleShift.Id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {

                throw new Exception("Something went wrong when setting the shift for sale." + e.Message);
            }
        }

        /// <summary>
        /// This method accepts a shift which has been set for sale.
        /// </summary>
        /// <param name="shift"></param>
        /// <param name="employee"></param>
        public void AcceptAvailableShift(ScheduleShift shift, Employee employee)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (SqlConnection connection = new DbConnection().GetConnection())
                    {
                        using (SqlCommand command = connection.CreateCommand())
                        {
                            command.CommandText = "UPDATE ScheduleShift SET employeeId = @param1, isForSale = 0 WHERE id = @param2 AND isForSale = 1;";

                            SqlParameter p1 = new SqlParameter("@param1", SqlDbType.Int);
                            SqlParameter p2 = new SqlParameter("@param2", SqlDbType.Int);

                            p1.Value = employee.Id;
                            p2.Value = shift.Id;

                            command.Parameters.Add(p1);
                            command.Parameters.Add(p2);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected == 0)
                            {
                                throw new Exception("Too slow! Shift is already accepted by another employee");
                            }
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Something went wrong accepting shift" + e.Message);
            }
        }

        public IEnumerable<ScheduleShift> GetAllAvailableShiftsByDepartmentId(int departmentId)
        {
            List<ScheduleShift> scheduleShifts = new List<ScheduleShift>();

            using (SqlConnection connection = new DbConnection().GetConnection())
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM ScheduleShift WHERE isForSale = 1;";

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ScheduleShift scheduleShift = BuildShiftObject(reader);
                            if (scheduleShift.Employee.DepartmentId == departmentId)
                            {
                                scheduleShifts.Add(scheduleShift);
                            }
                        }
                    }
                }
            }
            return scheduleShifts; ;
        }

        public void DeleteScheduleShift(ScheduleShift scheduleShift)
        {
            using (SqlConnection connection = new DbConnection().GetConnection())
            {
                using (SqlCommand deleteScheduleShift = new SqlCommand(
                      "DELETE FROM ScheduleShift WHERE id = @param1", connection))
                {
                    deleteScheduleShift.Parameters.AddWithValue(@"param1", scheduleShift.Id);
                    deleteScheduleShift.ExecuteNonQuery();
                }
            }
        }

        private ScheduleShift BuildShiftObject(SqlDataReader reader)
        {
            ScheduleShift scheduleShift = new ScheduleShift();
            scheduleShift.Id = reader.GetInt32(0);
            scheduleShift.Employee = new EmployeeRepository().GetEmployeeById(Convert.ToInt32(reader["EmployeeId"].ToString()));
            scheduleShift.StartTime = reader.GetDateTime(1);
            scheduleShift.Hours = Convert.ToDouble(reader["Hours"].ToString());
            scheduleShift.IsForSale = Convert.ToBoolean(reader["IsForSale"]);
            scheduleShift.RowVersion = reader.GetFieldValue<byte[]>(6);
            return scheduleShift;
        }
    }
}
