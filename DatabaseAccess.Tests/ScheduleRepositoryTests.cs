﻿using System;
using System.Collections.Generic;
using Core;
using DatabaseAccess.Departments;
using DatabaseAccess.Employees;
using DatabaseAccess.Schedules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogic;

namespace DatabaseAccess.Tests
{
    [TestClass]
    public class ScheduleRepositoryTests
    {
        IScheduleRepository _scheduleRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _scheduleRepository = new ScheduleRepository();
        }

        [TestMethod]
        public void InsertScheduleTest()
        {
            ScheduleShift shift1 = new ScheduleShift() { Employee = new EmployeeRepository().GetEmployeeByUsername("MikkelP"), Hours = 8, StartTime = new DateTime(2017, 11, 28, 8, 0, 0) };
            Schedule schedule = new Schedule() { Department = new DepartmentRepository().GetDepartmentById(3), StartDate = new DateTime(2017, 11, 27, 0, 0, 0, DateTimeKind.Utc), EndDate = new DateTime(2017, 12, 18, 0, 0, 0, DateTimeKind.Utc) };
            schedule.Shifts.Add(shift1);

            int beforeInsert = _scheduleRepository.GetSchedulesByDepartmentId(3).Count;
            int afterInsert = 0;

            _scheduleRepository.InsertSchedule(schedule);
            afterInsert = _scheduleRepository.GetSchedulesByDepartmentId(3).Count;
            Assert.AreEqual(beforeInsert, afterInsert - 1);
        }

        [TestMethod()]
        public void GetSchedulesByDepartmentIdTest()
        {
            List<Schedule> schedules = _scheduleRepository.GetSchedulesByDepartmentId(1);
            Assert.IsNotNull(schedules);
            Assert.AreNotEqual(0, schedules.Count);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            DbSetUp.SetUpDb();
        }
    }
}
