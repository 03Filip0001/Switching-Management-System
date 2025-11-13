using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using WorkPlanClass;

namespace ServerTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            for (int i = 0; i < 3; i++)
            {
                WorkPlanClass.WorkPlanClass plan = new WorkPlanClass.WorkPlanClass()
                {
                    ID = 1,
                    WorkPlanName = "Testing",
                    OperatorName = "Filip",
                    OperatorSurname = "Test",
                    WorkPlanState = WorkPlansStates.Draft,
                    StartDate = "test",
                    EndDate = "test",
                };
                Console.WriteLine(plan);

                bool status = client.SaveWorkPlan(plan);
                if (status)
                {
                    Console.WriteLine("saved");
                }
                else
                {
                    Console.WriteLine("NOT SAVED");
                }
            }
            client.Close();
        }
    }
}
