﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DesktopClient.ScheduleService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ScheduleService.IScheduleService")]
    public interface IScheduleService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IScheduleService/GetScheduleByDepartmentIdAndDate", ReplyAction="http://tempuri.org/IScheduleService/GetScheduleByDepartmentIdAndDateResponse")]
        Core.Schedule GetScheduleByDepartmentIdAndDate(int departmentId, System.DateTime date);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IScheduleService/GetScheduleByDepartmentIdAndDate", ReplyAction="http://tempuri.org/IScheduleService/GetScheduleByDepartmentIdAndDateResponse")]
        System.Threading.Tasks.Task<Core.Schedule> GetScheduleByDepartmentIdAndDateAsync(int departmentId, System.DateTime date);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IScheduleService/InsertScheduleIntoDb", ReplyAction="http://tempuri.org/IScheduleService/InsertScheduleIntoDbResponse")]
        void InsertScheduleIntoDb(Core.Schedule schedule);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IScheduleService/InsertScheduleIntoDb", ReplyAction="http://tempuri.org/IScheduleService/InsertScheduleIntoDbResponse")]
        System.Threading.Tasks.Task InsertScheduleIntoDbAsync(Core.Schedule schedule);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IScheduleService/UpdateSchedule", ReplyAction="http://tempuri.org/IScheduleService/UpdateScheduleResponse")]
        void UpdateSchedule(Core.Schedule schedule);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IScheduleService/UpdateSchedule", ReplyAction="http://tempuri.org/IScheduleService/UpdateScheduleResponse")]
        System.Threading.Tasks.Task UpdateScheduleAsync(Core.Schedule schedule);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IScheduleService/GetSchedulesByDepartmentId", ReplyAction="http://tempuri.org/IScheduleService/GetSchedulesByDepartmentIdResponse")]
        System.Collections.Generic.List<Core.Schedule> GetSchedulesByDepartmentId(int departmentId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IScheduleService/GetSchedulesByDepartmentId", ReplyAction="http://tempuri.org/IScheduleService/GetSchedulesByDepartmentIdResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<Core.Schedule>> GetSchedulesByDepartmentIdAsync(int departmentId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IScheduleService/GenerateScheduleFromTemplateScheduleAndStartD" +
            "ate", ReplyAction="http://tempuri.org/IScheduleService/GenerateScheduleFromTemplateScheduleAndStartD" +
            "ateResponse")]
        Core.Schedule GenerateScheduleFromTemplateScheduleAndStartDate(Core.TemplateSchedule tempSchedule, System.DateTime startTime);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IScheduleService/GenerateScheduleFromTemplateScheduleAndStartD" +
            "ate", ReplyAction="http://tempuri.org/IScheduleService/GenerateScheduleFromTemplateScheduleAndStartD" +
            "ateResponse")]
        System.Threading.Tasks.Task<Core.Schedule> GenerateScheduleFromTemplateScheduleAndStartDateAsync(Core.TemplateSchedule tempSchedule, System.DateTime startTime);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IScheduleServiceChannel : DesktopClient.ScheduleService.IScheduleService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ScheduleServiceClient : System.ServiceModel.ClientBase<DesktopClient.ScheduleService.IScheduleService>, DesktopClient.ScheduleService.IScheduleService {
        
        public ScheduleServiceClient() {
        }
        
        public ScheduleServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ScheduleServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ScheduleServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ScheduleServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Core.Schedule GetScheduleByDepartmentIdAndDate(int departmentId, System.DateTime date) {
            return base.Channel.GetScheduleByDepartmentIdAndDate(departmentId, date);
        }
        
        public System.Threading.Tasks.Task<Core.Schedule> GetScheduleByDepartmentIdAndDateAsync(int departmentId, System.DateTime date) {
            return base.Channel.GetScheduleByDepartmentIdAndDateAsync(departmentId, date);
        }
        
        public void InsertScheduleIntoDb(Core.Schedule schedule) {
            base.Channel.InsertScheduleIntoDb(schedule);
        }
        
        public System.Threading.Tasks.Task InsertScheduleIntoDbAsync(Core.Schedule schedule) {
            return base.Channel.InsertScheduleIntoDbAsync(schedule);
        }
        
        public void UpdateSchedule(Core.Schedule schedule) {
            base.Channel.UpdateSchedule(schedule);
        }
        
        public System.Threading.Tasks.Task UpdateScheduleAsync(Core.Schedule schedule) {
            return base.Channel.UpdateScheduleAsync(schedule);
        }
        
        public System.Collections.Generic.List<Core.Schedule> GetSchedulesByDepartmentId(int departmentId) {
            return base.Channel.GetSchedulesByDepartmentId(departmentId);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Core.Schedule>> GetSchedulesByDepartmentIdAsync(int departmentId) {
            return base.Channel.GetSchedulesByDepartmentIdAsync(departmentId);
        }
        
        public Core.Schedule GenerateScheduleFromTemplateScheduleAndStartDate(Core.TemplateSchedule tempSchedule, System.DateTime startTime) {
            return base.Channel.GenerateScheduleFromTemplateScheduleAndStartDate(tempSchedule, startTime);
        }
        
        public System.Threading.Tasks.Task<Core.Schedule> GenerateScheduleFromTemplateScheduleAndStartDateAsync(Core.TemplateSchedule tempSchedule, System.DateTime startTime) {
            return base.Channel.GenerateScheduleFromTemplateScheduleAndStartDateAsync(tempSchedule, startTime);
        }
    }
}
