﻿using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseAccess.TemplateSchedules;
using System.Transactions;

namespace BusinessLogic
{
    public class TemplateScheduleController : ITemplateScheduleController
    {
        private ITemplateScheduleRepository _templateScheduleRepository;
        private ITemplateShiftControlller _templateShiftController;

        public TemplateScheduleController()
        {
            _templateShiftController = new TemplateShiftController();
            _templateScheduleRepository = new TemplateScheduleRepository();
        }

        public TemplateSchedule CreateTemplateSchedule(int numberOfWeeks, string name)
        {
            return new TemplateSchedule(numberOfWeeks, name);
        }

        public List<TemplateSchedule> GetAllTemplateSchedules()
        {
            List<TemplateSchedule> templateSchedules = _templateScheduleRepository.GetAllTemplateSchedules().ToList();
            templateSchedules.ForEach(x => x.TemplateShifts = _templateShiftController.GetTemplateShiftsByTemplateScheduleId(x.Id));
            return templateSchedules;
        }

        public void AddTemplateScheduleToDb(TemplateSchedule templateSchedule)
        {
            if (ValidateTemplateSchedule(templateSchedule))
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    templateSchedule.Id = _templateScheduleRepository.AddTemplateScheduleToDatabase(templateSchedule);
                    _templateShiftController.AddTemplateShiftsFromTemplateSchedule(templateSchedule.Id, templateSchedule.TemplateShifts);
                    scope.Complete();
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void UpdateTemplateSchedule(TemplateSchedule templateSchedule, List<TemplateShift> deletedShifts)
        {
            if (ValidateTemplateSchedule(templateSchedule))
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    _templateScheduleRepository.UpdateTemplateSchedule(templateSchedule);
                    _templateShiftController.AddTemplateShiftsFromTemplateSchedule(templateSchedule.Id, templateSchedule.TemplateShifts);
                    deletedShifts.ForEach(x => _templateShiftController.DeleteTemplateShift(x));
                    scope.Complete();
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void UpdateTemplateSchedule(TemplateSchedule templateSchedule)
        {
            if (ValidateTemplateSchedule(templateSchedule))
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    _templateScheduleRepository.UpdateTemplateSchedule(templateSchedule);
                    _templateShiftController.AddTemplateShiftsFromTemplateSchedule(templateSchedule.Id, templateSchedule.TemplateShifts);
                    scope.Complete();
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private bool ValidateTemplateSchedule(TemplateSchedule templateSchedule)
        {
            bool isOkToInsert = true;
            if (templateSchedule.DepartmentId <= 0)
            {
                isOkToInsert = false;
            }
            else if (templateSchedule.Name == null || templateSchedule.Name == string.Empty)
            {
                isOkToInsert = false;
            }
            else if (templateSchedule.NoOfWeeks <= 0)
            {
                isOkToInsert = false;
            }
            return isOkToInsert;
        }
    }
}
