using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;
using System.Web.Hosting;
using System.Xml;
using WorkPlanClass;

namespace Server
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private static int _lastWorkPlanID;
        protected string pathWorkPlansData = HostingEnvironment.MapPath("~/App_Data/WorkPlans.xml");
        protected string pathWorkPlanID = HostingEnvironment.MapPath("~/App_Data/WorkPlanID.txt");

        Service1()
        {
            if (File.Exists(pathWorkPlanID) && new FileInfo(pathWorkPlanID).Length > 0)
            {
                string content = File.ReadAllText(pathWorkPlanID);
                int.TryParse(content, out _lastWorkPlanID);
            }
            else
            {
                _lastWorkPlanID = 0;
                File.WriteAllText(pathWorkPlanID, _lastWorkPlanID.ToString());
            }
        }
        
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public WorkPlanClass.WorkPlanCollection GetWorkPlans()
        {
            WorkPlanClass.WorkPlanCollection workPlanCollection;
            var serializer = new DataContractSerializer(typeof(WorkPlanClass.WorkPlanCollection));

            using (var stream = File.OpenRead(pathWorkPlansData))
            {
                Debug.WriteLine("before read");
                workPlanCollection = (WorkPlanCollection)serializer.ReadObject(stream);
                Debug.WriteLine("after read");
            }

            return workPlanCollection;
        }

        public bool SaveWorkPlan(WorkPlanClass.WorkPlanClass workPlan)
        {
            try
            {
                WorkPlanClass.WorkPlanCollection workPlanCollection;
                var serializer = new DataContractSerializer(typeof(WorkPlanClass.WorkPlanCollection));
                var settings = new XmlWriterSettings { Indent = true };

                Debug.WriteLine("checking if file exists");
                if (File.Exists(pathWorkPlansData) && new FileInfo(pathWorkPlansData).Length > 0)
                {
                    using (var stream = File.OpenRead(pathWorkPlansData))
                    {
                        Debug.WriteLine("before read");
                        workPlanCollection = (WorkPlanCollection)serializer.ReadObject(stream);
                        Debug.WriteLine("after read");
                    }
                }
                else
                {
                    workPlanCollection = new WorkPlanClass.WorkPlanCollection();
                }

                Debug.WriteLine("adding new work plan");
                workPlanCollection.WorkPlans.Add(workPlan);
                using (var writer = XmlWriter.Create(pathWorkPlansData, settings))
                {
                    serializer.WriteObject(writer, workPlanCollection);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public int GetNewWorkPlanUniqueID()
        {
            Interlocked.Increment(ref _lastWorkPlanID);
            File.WriteAllText(pathWorkPlanID, _lastWorkPlanID.ToString());
            return _lastWorkPlanID;
        }
    }
}
