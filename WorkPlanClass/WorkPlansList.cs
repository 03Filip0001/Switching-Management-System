using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CommonLibrarySE
{
    public enum WorkPlansStates { Draft, Approved, Executed, Cancelled }

    [DataContract(Name = "WorkPlan", Namespace ="")]
    public class WorkPlan
    {
        [DataMember(Name = "ID")]
        public int ID { get; set; }

        [DataMember(Name ="Name")]
        public string Name { get; set; }

        [DataMember(Name = "State")]
        public WorkPlansStates State { get; set; }

        [DataMember(Name = "OperatorName")]
        public string OperatorName { get; set; }

        [DataMember(Name = "OperatorSurname")]
        public string OperatorSurname { get; set; }

        [DataMember(Name = "StartDate")]
        public string StartDate { get; set; }

        [DataMember(Name = "EndDate")]
        public string EndDate { get; set; }

        [DataMember(Name = "Instructions")]
        public InstructionsList Instructions { get; set; }

        public override string ToString()
        {
            string ret = "ID: " + this.ID + "\n";
            ret += "Work plan name: " + this.Name + "\n";
            ret += "Work plan state: " + this.State + "\n";
            ret += "Operator name: " + this.OperatorName + "\n";
            ret += "Operator surname: " + this.OperatorSurname + "\n";
            ret += "Start date: " + this.StartDate + "\n";
            ret += "End date: " + this.EndDate + "\n";
            return ret;
        }
    }

    [DataContract(Name = "WorkPlansList", Namespace = "")]
    public class WorkPlanList
    {
        [DataMember(Name ="WorkPlans")]
        public List<WorkPlan> WorkPlans { get; set; }

        public WorkPlanList()
        {
            WorkPlans = new List<WorkPlan>();
        }
    }
}
