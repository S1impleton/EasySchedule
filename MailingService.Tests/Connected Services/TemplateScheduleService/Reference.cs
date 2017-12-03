﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tests.TemplateScheduleService
{
    using System.Runtime.Serialization;


    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name = "DayOfWeek", Namespace = "http://schemas.datacontract.org/2004/07/System")]
    public enum DayOfWeek : int
    {

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Sunday = 0,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Monday = 1,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Tuesday = 2,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Wednesday = 3,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Thursday = 4,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Friday = 5,

        [System.Runtime.Serialization.EnumMemberAttribute()]
        Saturday = 6,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "TemplateScheduleService.ITemplateScheduleService")]
    public interface ITemplateScheduleService
    {

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ITemplateScheduleService/GetAllTemplateSchedules", ReplyAction = "http://tempuri.org/ITemplateScheduleService/GetAllTemplateSchedulesResponse")]
        Core.TemplateSchedule[] GetAllTemplateSchedules();

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ITemplateScheduleService/GetAllTemplateSchedules", ReplyAction = "http://tempuri.org/ITemplateScheduleService/GetAllTemplateSchedulesResponse")]
        System.Threading.Tasks.Task<Core.TemplateSchedule[]> GetAllTemplateSchedulesAsync();

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ITemplateScheduleService/FindTemplateScheduleByName", ReplyAction = "http://tempuri.org/ITemplateScheduleService/FindTemplateScheduleByNameResponse")]
        Core.TemplateSchedule FindTemplateScheduleByName(string name);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ITemplateScheduleService/FindTemplateScheduleByName", ReplyAction = "http://tempuri.org/ITemplateScheduleService/FindTemplateScheduleByNameResponse")]
        System.Threading.Tasks.Task<Core.TemplateSchedule> FindTemplateScheduleByNameAsync(string name);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ITemplateScheduleService/AddTemplateScheduleToDb", ReplyAction = "http://tempuri.org/ITemplateScheduleService/AddTemplateScheduleToDbResponse")]
        void AddTemplateScheduleToDb(Core.TemplateSchedule templateSchedule);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ITemplateScheduleService/AddTemplateScheduleToDb", ReplyAction = "http://tempuri.org/ITemplateScheduleService/AddTemplateScheduleToDbResponse")]
        System.Threading.Tasks.Task AddTemplateScheduleToDbAsync(Core.TemplateSchedule templateSchedule);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ITemplateScheduleService/UpdateTemplateSchedule", ReplyAction = "http://tempuri.org/ITemplateScheduleService/UpdateTemplateScheduleResponse")]
        void UpdateTemplateSchedule(Core.TemplateSchedule templateSchedule);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/ITemplateScheduleService/UpdateTemplateSchedule", ReplyAction = "http://tempuri.org/ITemplateScheduleService/UpdateTemplateScheduleResponse")]
        System.Threading.Tasks.Task UpdateTemplateScheduleAsync(Core.TemplateSchedule templateSchedule);
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITemplateScheduleServiceChannel : Tests.TemplateScheduleService.ITemplateScheduleService, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TemplateScheduleServiceClient : System.ServiceModel.ClientBase<Tests.TemplateScheduleService.ITemplateScheduleService>, Tests.TemplateScheduleService.ITemplateScheduleService
    {

        public TemplateScheduleServiceClient()
        {
        }

        public TemplateScheduleServiceClient(string endpointConfigurationName) :
                base(endpointConfigurationName)
        {
        }

        public TemplateScheduleServiceClient(string endpointConfigurationName, string remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public TemplateScheduleServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public TemplateScheduleServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
                base(binding, remoteAddress)
        {
        }

        public Core.TemplateSchedule[] GetAllTemplateSchedules()
        {
            return base.Channel.GetAllTemplateSchedules();
        }

        public System.Threading.Tasks.Task<Core.TemplateSchedule[]> GetAllTemplateSchedulesAsync()
        {
            return base.Channel.GetAllTemplateSchedulesAsync();
        }

        public Core.TemplateSchedule FindTemplateScheduleByName(string name)
        {
            return base.Channel.FindTemplateScheduleByName(name);
        }

        public System.Threading.Tasks.Task<Core.TemplateSchedule> FindTemplateScheduleByNameAsync(string name)
        {
            return base.Channel.FindTemplateScheduleByNameAsync(name);
        }

        public void AddTemplateScheduleToDb(Core.TemplateSchedule templateSchedule)
        {
            base.Channel.AddTemplateScheduleToDb(templateSchedule);
        }

        public System.Threading.Tasks.Task AddTemplateScheduleToDbAsync(Core.TemplateSchedule templateSchedule)
        {
            return base.Channel.AddTemplateScheduleToDbAsync(templateSchedule);
        }

        public void UpdateTemplateSchedule(Core.TemplateSchedule templateSchedule)
        {
            base.Channel.UpdateTemplateSchedule(templateSchedule);
        }

        public System.Threading.Tasks.Task UpdateTemplateScheduleAsync(Core.TemplateSchedule templateSchedule)
        {
            return base.Channel.UpdateTemplateScheduleAsync(templateSchedule);
        }
    }
}
