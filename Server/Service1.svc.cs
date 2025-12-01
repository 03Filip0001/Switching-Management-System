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

namespace Server
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private static int _lastWorkPlanID;
        private static int _lastSubstationID;

        private static readonly string pathWorkPlansData;// = HostingEnvironment.MapPath("~/App_Data/WorkPlans.xml");
        private static readonly string pathWorkPlanID;// = HostingEnvironment.MapPath("~/App_Data/WorkPlanID.txt");

        private static readonly string pathSubstationsData;// = HostingEnvironment.MapPath("~/App_Data/WorkPlans.xml");
        private static readonly string pathSubstationID;// = HostingEnvironment.MapPath("~/App_Data/WorkPlanID.txt");

        static Service1()
        {
            pathWorkPlansData = HostingEnvironment.MapPath("~/App_Data/WorkPlans.xml");
            pathWorkPlanID = HostingEnvironment.MapPath("~/App_Data/WorkPlanID.txt");

            pathSubstationsData = HostingEnvironment.MapPath("~/App_Data/Substations.xml");
            pathSubstationID = HostingEnvironment.MapPath("~/App_Data/SubstationID.txt");


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

            if (File.Exists(pathSubstationID) && new FileInfo(pathSubstationID).Length > 0)
            {
                string content = File.ReadAllText(pathSubstationID);
                int.TryParse(content, out _lastSubstationID);
            }
            else
            {
                _lastSubstationID = 0;
                File.WriteAllText(pathSubstationID, _lastSubstationID.ToString());
            }
        }
        public Service1() { }
        
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

        public ObservableCollection<CommonLibrarySE.WorkPlan> GetWorkPlans()
        {
            ObservableCollection<CommonLibrarySE.WorkPlan> workPlanCollection = null;
            var serializer = new DataContractSerializer(typeof(ObservableCollection<CommonLibrarySE.WorkPlan>));

            if (File.Exists(pathWorkPlansData) && new FileInfo(pathWorkPlansData).Length > 0)
            {
                using (var stream = File.OpenRead(pathWorkPlansData))
                {
                    workPlanCollection = (ObservableCollection<CommonLibrarySE.WorkPlan>)serializer.ReadObject(stream);
                }
            }
            else
            {
                workPlanCollection = new ObservableCollection<CommonLibrarySE.WorkPlan>();
            }

            return workPlanCollection;
        }

        public bool SaveWorkPlan(CommonLibrarySE.WorkPlan workPlan)
        {
            try
            {
                ObservableCollection<CommonLibrarySE.WorkPlan> workPlanCollection;
                var serializer = new DataContractSerializer(typeof(ObservableCollection<CommonLibrarySE.WorkPlan>));
                var settings = new XmlWriterSettings { Indent = true };

                Debug.WriteLine("Checking if file exists");
                if (File.Exists(pathWorkPlansData) && new FileInfo(pathWorkPlansData).Length > 0)
                {
                    using (var stream = File.OpenRead(pathWorkPlansData))
                    {
                        Debug.WriteLine("Reading xaml file...");
                        workPlanCollection = (ObservableCollection<CommonLibrarySE.WorkPlan>)serializer.ReadObject(stream);
                    }
                }
                else
                {
                    Debug.WriteLine("Creating new list...");
                    workPlanCollection = new ObservableCollection<CommonLibrarySE.WorkPlan>();
                }

                Debug.WriteLine("Adding new work plan before saving...");
                workPlanCollection.Add(workPlan);
                using (var writer = XmlWriter.Create(pathWorkPlansData, settings))
                {
                    Debug.WriteLine("Saving new work plans to xaml...");
                    serializer.WriteObject(writer, workPlanCollection);
                }

                return true;
            }
            catch
            {
                Debug.WriteLine("ERROR SAVING NEW WORK PLAN");
                return false;
            }
        }

        public int GetNewWorkPlanUniqueID()
        {
            int newID = Interlocked.Increment(ref _lastWorkPlanID);
            File.WriteAllText(pathWorkPlanID, _lastWorkPlanID.ToString());
            return _lastWorkPlanID;
        }

        public ObservableCollection<CommonLibrarySE.Substation> GetSubstations()
        {
            ObservableCollection<CommonLibrarySE.Substation> subs = new ObservableCollection<CommonLibrarySE.Substation> ();
            subs.Add(new CommonLibrarySE.Substation
            {
                ID = 0,
                Name = "SE",
                Feeders = new ObservableCollection<CommonLibrarySE.Feeder>(),
            });

            return subs;

            ObservableCollection<CommonLibrarySE.Substation> SubstationCollection = null;
            var serializer = new DataContractSerializer(typeof(ObservableCollection<CommonLibrarySE.Substation>));

            if (File.Exists(pathSubstationsData) && new FileInfo(pathSubstationsData).Length > 0)
            {
                using (var stream = File.OpenRead(pathWorkPlansData))
                {
                    SubstationCollection = (ObservableCollection<CommonLibrarySE.Substation>)serializer.ReadObject(stream);
                }
            }
            else
            {
                SubstationCollection = new ObservableCollection<CommonLibrarySE.Substation>();
            }

            return SubstationCollection;
        }

        public bool SaveSubstation(CommonLibrarySE.Substation substation)
        {
            try
            {
                ObservableCollection<CommonLibrarySE.Substation> SubstationCollection;
                var serializer = new DataContractSerializer(typeof(ObservableCollection<CommonLibrarySE.Substation>));
                var settings = new XmlWriterSettings { Indent = true };

                Debug.WriteLine("Checking if file exists");
                if (File.Exists(pathSubstationsData) && new FileInfo(pathSubstationsData).Length > 0)
                {
                    using (var stream = File.OpenRead(pathSubstationsData))
                    {
                        Debug.WriteLine("Reading xaml file...");
                        SubstationCollection = (ObservableCollection<CommonLibrarySE.Substation>)serializer.ReadObject(stream);
                    }
                }
                else
                {
                    Debug.WriteLine("Creating new list...");
                    SubstationCollection = new ObservableCollection<CommonLibrarySE.Substation>();
                }

                Debug.WriteLine("Adding new work plan before saving...");
                SubstationCollection.Add(substation);
                using (var writer = XmlWriter.Create(pathSubstationsData, settings))
                {
                    Debug.WriteLine("Saving new work plans to xaml...");
                    serializer.WriteObject(writer, SubstationCollection);
                }

                return true;
            }
            catch
            {
                Debug.WriteLine("ERROR SAVING NEW SUBSTATION");
                return false;
            }
        }

        public bool EditSubstation(CommonLibrarySE.Substation substation)
        {
            return false;
        }

        public int GetNewSubstationUniqueID()
        {
            int newID = Interlocked.Increment(ref _lastSubstationID);
            File.WriteAllText(pathSubstationID, _lastSubstationID.ToString());
            return _lastSubstationID;
        }
    }
}
