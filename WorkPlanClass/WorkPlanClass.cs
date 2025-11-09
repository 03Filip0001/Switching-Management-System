using System;

namespace WorkPlanClass
{
    public enum WorkPlansStates { Draft, Approved, Executed, Cancelled }

    public class WorkPlanClass
    {
        public int ID { get; set; }
        public string WorkPlanName { get; set; }
        public WorkPlansStates WorkPlanState { get; set; }
        public string OperatorName { get; set; }
        public string OperatorSurname { get; set; }

        public string StartDate { get; set; }
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
}
