﻿using System;
using BusinessLogic;
using Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.BusinessLogic
{
    [TestClass]
    public class TestScheduleCtrl
    {
        
        ScheduleController schCtrl = new ScheduleController();

        [TestMethod]
        public void TestGetSchedueleByCurrentDate()
        {
            DateTime currentDate = new DateTime(2017,11,13);

            Schedule schedule = schCtrl.GetScheduleByCurrentDate(currentDate);

            Assert.IsNotNull(schedule);
            Assert.AreEqual(3, schedule.Shifts.Count);

        }
    }
}
