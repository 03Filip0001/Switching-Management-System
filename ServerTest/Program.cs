using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            WorkPlanClass.WorkPlanClass reply = client.GetWorkPlan();
            Console.WriteLine(reply);
            client.Close();
        }
    }
}
