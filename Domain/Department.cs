﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Core
{
    [DataContract]
    public class Department
    {
        #region Properties
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public int WorkplaceId { get; set; }

        [DataMember]
        public List<Employee> Employees { get; set; }
        #endregion

        public Department()
        {
            Employees = new List<Employee>();
        }

    }
}
