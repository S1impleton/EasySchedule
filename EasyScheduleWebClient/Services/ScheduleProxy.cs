﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Core;
using EasyScheduleWebClient.ScheduleService;

namespace EasyScheduleWebClient.Services
{
    public class ScheduleProxy : IScheduleService
    {
        readonly IScheduleService _scheduleService = new ScheduleServiceClient();

        public Schedule GenerateScheduleFromTemplateScheduleAndStartDate(TemplateSchedule templateSchedule, DateTime startTime)
        {
            throw new NotImplementedException();
        }

        public Task<Schedule> GenerateScheduleFromTemplateScheduleAndStartDateAsync(TemplateSchedule templateSchedule, DateTime startTime)
        {
            throw new NotImplementedException();
        }

        public Schedule GetScheduleByDepartmentIdAndDate(int departmentId, DateTime date)
        {
            return _scheduleService.GetScheduleByDepartmentIdAndDate(departmentId, date);
        }

        public Task<Schedule> GetScheduleByDepartmentIdAndDateAsync(int departmentId, DateTime date)
        {
            throw new NotImplementedException();
        }

        public void InsertScheduleToDb(Schedule schedule)
        {
            throw new NotImplementedException();
        }

        public Task InsertScheduleToDbAsync(Schedule schedule)
        {
            throw new NotImplementedException();
        }

        public void UpdateSchedule(Schedule schedule)
        {
            _scheduleService.UpdateSchedule(schedule);
        }

        public void UpdateScheduleWithDelete(Schedule schedule, List<ScheduleShift> deletedScheduleShifts)
        {
            throw new NotImplementedException();
        }

        public Task UpdateScheduleAsync(Schedule schedule)
        {
            throw new NotImplementedException();
        }

        public Task UpdateScheduleAsync(Schedule schedule, List<ScheduleShift> deletedScheduleShifts)
        {
            throw new NotImplementedException();
        }

        List<Schedule> IScheduleService.GetSchedulesByDepartmentId(int departmentId)
        {
            return _scheduleService.GetSchedulesByDepartmentId(departmentId);
        }

        Task<List<Schedule>> IScheduleService.GetSchedulesByDepartmentIdAsync(int departmentId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateScheduleWithDeleteAsync(Schedule schedule, List<ScheduleShift> deletedScheduleShifts)
        {
            throw new NotImplementedException();
        }
    }
}