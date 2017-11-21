﻿using Core;
using DatabaseAccess.Employees;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess
{
    public class TemplateShiftDB
    {

        DbConnectionADO dbConADO = new DbConnectionADO();



        public void AddTempShiftsFromTempScheduleToDB(int tempScheduleIDFromDB, List<TemplateShift> TShift)
        {
            using (SqlConnection dBCon = new SqlConnection(dbConADO.KrakaConnectionString()))
            {
                dBCon.Open();
                foreach (TemplateShift ts in TShift)
                {
                    using (SqlCommand insertTempShift = new SqlCommand("INSERT INTO TemplateShift(weekDay, hours, startTime, templateScheduleId, employeeId) VALUES (@param1, @param2, @param3, @param4, @param5)", dBCon))
                    {
                        insertTempShift.Parameters.AddWithValue("@param1", ts.WeekDay.ToString());
                        insertTempShift.Parameters.AddWithValue("@param2", ts.Hours);
                        insertTempShift.Parameters.AddWithValue("@param3", ts.StartTime);
                        insertTempShift.Parameters.AddWithValue("@param4", tempScheduleIDFromDB);
                        insertTempShift.Parameters.AddWithValue("@param5", ts.Employee.Id);

                        insertTempShift.ExecuteNonQuery();
                    }
                }
                dBCon.Close();
            }
        }

        public IEnumerable<TemplateShift> GetAllShifts()
        {
            List<TemplateShift> tempList = new List<TemplateShift>();
            using (SqlConnection dBCon = new SqlConnection(dbConADO.KrakaConnectionString()))
            {
                dBCon.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM TemplateShift", dBCon))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader.HasRows)
                            {

                                TemplateShift tempShift = new TemplateShift();
                                tempShift.ID = Convert.ToInt32(reader["Id"].ToString());
                                tempShift.WeekDay = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), reader["weekDay"].ToString());
                                tempShift.Hours = Convert.ToDouble(reader["Hours"].ToString());
                                tempShift.StartTime = TimeSpan.Parse(reader["StartTime"].ToString());
                                tempShift.TemplateScheduleID = Convert.ToInt32(reader["TemplateScheduleId"].ToString());
                                tempShift.Employee = new EmployeeRepository().GetEmployeeById(Convert.ToInt32(reader["employeeId"].ToString()));
                                tempList.Add(tempShift);
                            }
                        }
                    }
                }
                dBCon.Close();
            }
            return tempList;
        }



        public TemplateShift FindTempShiftByID(int tempShiftID)
        {
            TemplateShift tShift = null;
            using (SqlConnection dBCon = new SqlConnection(dbConADO.KrakaConnectionString()))
            {
                dBCon.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM TemplateShift WHERE ID = @param1", dBCon);
                command.Parameters.AddWithValue("@param1", tempShiftID);

                using (DbDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tShift = new TemplateShift(reader.GetInt32(0), GetDayOfweekBasedOnString(reader.GetString(1)), reader.GetFloat(2), new TimeSpan(reader.GetDateTime(3).Hour, reader.GetDateTime(3).Minute, reader.GetDateTime(3).Second), reader.GetInt32(4), new Employee() { Id = reader.GetInt32(5) });
                    }
                }
                dBCon.Close();
            }
            return tShift;
        }
        public DayOfWeek GetDayOfweekBasedOnString(string day)
        {
            DayOfWeek currentDay = default(DayOfWeek);
            switch (day)
            {
                case "Monday":
                    currentDay = DayOfWeek.Monday;
                    break;
                case "Tuesday":
                    currentDay = DayOfWeek.Tuesday;
                    break;
                case "Wednesday":
                    currentDay = DayOfWeek.Wednesday;
                    break;
                case "Thursday":
                    currentDay = DayOfWeek.Thursday;
                    break;
                case "Friday":
                    currentDay = DayOfWeek.Friday;
                    break;
                case "Saturday":
                    currentDay = DayOfWeek.Saturday;
                    break;
                case "Sunday":
                    currentDay = DayOfWeek.Sunday;
                    break;
            }
            return currentDay;
        }
    }

}
