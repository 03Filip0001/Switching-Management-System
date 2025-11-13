using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WorkPlanClass
{
    public enum WorkPlansStates { Draft, Approved, Executed, Cancelled }

    [DataContract(Name = "WorkPlan", Namespace ="")]
    public class WorkPlanClass
    {
        [DataMember(Name ="ID")]
        public int ID { get; set; }

        [DataMember(Name ="WorkPlanName")]
        public string WorkPlanName { get; set; }

        [DataMember(Name = "WorkPlanState")]
        public WorkPlansStates WorkPlanState { get; set; }

        [DataMember(Name = "OperatorName")]
        public string OperatorName { get; set; }

        [DataMember(Name = "OperatorSurname")]
        public string OperatorSurname { get; set; }

        [DataMember(Name = "StartDate")]
        public string StartDate { get; set; }

        [DataMember(Name = "EndDate")]
        public string EndDate { get; set; }

        public override string ToString()
        {
            string ret = "ID: " + this.ID + "\n";
            ret += "Work plan name: " + this.WorkPlanName + "\n";
            ret += "Work plan state: " + this.WorkPlanState + "\n";
            ret += "Operator name: " + this.OperatorName + "\n";
            ret += "Operator surname: " + this.OperatorSurname + "\n";
            ret += "Start date: " + this.StartDate + "\n";
            ret += "End date: " + this.EndDate + "\n";
            return ret;
        }
    }

    [DataContract(Name = "WorkPlansCollection", Namespace = "")]
    public class WorkPlanCollection
    {
        [DataMember(Name ="WorkPlans")]
        public List<WorkPlanClass> WorkPlans { get; set; }

        public WorkPlanCollection()
        {
            WorkPlans = new List<WorkPlanClass>();
        }
    }
}
