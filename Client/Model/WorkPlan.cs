namespace Mini_Switching_Management_System_Client.Model
{
    public enum WorkPlansStates { Draft, Approved, Executed, Cancelled}
    internal class WorkPlan
    {
        public int ID { get; set; }
        public string WorkPlanName { get; set; }
        public WorkPlansStates WorkPlanState { get; set; }
        public string OperatorName { get; set; }
        public string OperatorSurname { get; set; }

        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
