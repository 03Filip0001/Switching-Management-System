using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Runtime.Serialization;
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

        private ObservableCollection<CommonLibrarySE.WorkPlan> LoadWorkPlans()
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

        private bool SaveWorkPlans(ObservableCollection<CommonLibrarySE.WorkPlan> wp)
        {
            bool res = false;

            var serializer = new DataContractSerializer(typeof(ObservableCollection<CommonLibrarySE.WorkPlan>));
            var settings = new XmlWriterSettings { Indent = true };

            using (var writer = XmlWriter.Create(pathWorkPlansData, settings))
            {
                Debug.WriteLine("Saving new work plans to xaml...");
                serializer.WriteObject(writer, wp);
                Debug.Write("saved?");
                res = true;
            }

            return res;
        }

        public ObservableCollection<CommonLibrarySE.WorkPlan> GetWorkPlans()
        {
            return LoadWorkPlans();
        }

        public bool SaveWorkPlan(CommonLibrarySE.WorkPlan workPlan)
        {
            try
            {
                var workPlanCollection = LoadWorkPlans();

                var foundPlan = workPlanCollection.FirstOrDefault(wp => wp.ID == workPlan.ID);
                Debug.WriteLine("Adding new work plan before saving...");
                if (foundPlan != null)
                {
                    Debug.Write("Removing old work plan");
                    bool res = workPlanCollection.Remove(foundPlan);
                    if (res)
                    {
                        Debug.Write("Removed.");
                    }
                    else
                    {
                        Debug.Write("Could not remove...");
                    }
                }

                workPlanCollection.Add(workPlan);
                
                return SaveWorkPlans(workPlanCollection);
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

        private bool DeleteWorkPlan(int ID)
        {
            bool res = false;

            try
            {
                var workPlanCollection = LoadWorkPlans();

                var foundPlan = workPlanCollection.FirstOrDefault(wp => wp.ID == ID);
                if (foundPlan != null)
                {
                    res = workPlanCollection.Remove(foundPlan);
                    if (res)
                    {
                        Debug.Write("Removed.");
                    }
                    else
                    {
                        Debug.Write("Could not remove...");
                    }
                }

                if(res)
                    res = SaveWorkPlans(workPlanCollection);
            }
            catch
            {
                Debug.WriteLine("ERROR SAVING NEW WORK PLAN");
                return false;
            }

            return res;
        }

        private bool CheckWorkPlan(int ID)
        {
            bool res = false;

            var workPlanCollection = LoadWorkPlans();
            var substationCollection = LoadSubstations();

            Debug.WriteLine("Checking if work plan with ID" +  ID + " exists...");
            var foundPlan = workPlanCollection.FirstOrDefault(wp => wp.ID == ID);

            if (foundPlan != null)
            {
                Debug.WriteLine("Found work plan.");
                foreach(var instr in foundPlan.Instructions)
                {
                    foreach(var sw in instr.Switches)
                    {
                        bool found = false;
                        foreach (var sub in substationCollection)
                        {
                            foreach (var feed in sub.Feeders)
                            {
                                found = feed.Switches.Any(fs => fs.ID == sw.ID);
                                if (found)
                                {
                                    Debug.WriteLine("Found switch with ID: " +  sw.ID);
                                    break;
                                }
                            }
                            if(found) break;
                        }
                        if (!found) return false;
                    }
                }

                foundPlan.State = CommonLibrarySE.WorkPlanStates.Approved;
                res = SaveWorkPlan(foundPlan);
            }
            else
            {
                Debug.WriteLine("Not found work plan");
            }

                return res;
        }

        private bool ExecuteWorkPlan(int ID)
        {
            bool res = false;

            var workPlanCollection = LoadWorkPlans();
            var foundPlan = workPlanCollection.FirstOrDefault(wp => wp.ID == ID);

            List<CommonLibrarySE.Switch> updateSwitches = new List<CommonLibrarySE.Switch>();

            if(foundPlan != null)
            {
                foreach( var instr in foundPlan.Instructions)
                {
                    foreach( var sw in instr.Switches)
                    {
                        var update = updateSwitches.FirstOrDefault(wp => wp.ID == sw.ID);
                        if(update != null)
                        {
                            update.State = sw.State;
                        }
                        else
                        {
                            var newSwitch = new CommonLibrarySE.Switch { ID = sw.ID, State=sw.State};
                            updateSwitches.Add(newSwitch);
                        }
                    }
                }

                var updateMap = updateSwitches.ToDictionary(s => s.ID, s => s.State);
                var substations = LoadSubstations();
                foreach (var sub in substations)
                {
                    foreach(var feed in sub.Feeders)
                    {
                        foreach (var switchInFeeder in feed.Switches)
                        {
                            if (updateMap.TryGetValue(switchInFeeder.ID, out var newState))
                            {
                                switchInFeeder.State = newState;
                            }
                        }
                    }
                }


                res = SaveSubstations(substations);
                if (res)
                {
                    res = DeleteWorkPlan(ID);
                    foundPlan.State = CommonLibrarySE.WorkPlanStates.Executed;
                    if (res)
                    {
                        res = SaveWorkPlan(foundPlan);
                    }
                }
            }

            return res;
        }

        public bool WorkPlanAction(int ID, WorkPlanActions action)
        {
            bool res = false;
            switch(action){
                case WorkPlanActions.Delete:
                    res = DeleteWorkPlan(ID);
                    break;
                case WorkPlanActions.Check:
                    res = CheckWorkPlan(ID);
                    break;
                case WorkPlanActions.Execute: 
                    res = ExecuteWorkPlan(ID);
                    break;

                default:
                    break;
            }
            return res;
        }

        private ObservableCollection<CommonLibrarySE.Substation> LoadSubstations()
        {
            ObservableCollection<CommonLibrarySE.Substation> SubstationCollection = null;
            var serializer = new DataContractSerializer(typeof(ObservableCollection<CommonLibrarySE.Substation>));

            if (File.Exists(pathSubstationsData) && new FileInfo(pathSubstationsData).Length > 0)
            {
                using (var stream = File.OpenRead(pathSubstationsData))
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

        private bool SaveSubstations(ObservableCollection<CommonLibrarySE.Substation> subs)
        {
            var serializer = new DataContractSerializer(typeof(ObservableCollection<CommonLibrarySE.Substation>));
            var settings = new XmlWriterSettings { Indent = true };
            using (var writer = XmlWriter.Create(pathSubstationsData, settings))
            {
                Debug.WriteLine("Saving new work plans to xaml...");
                serializer.WriteObject(writer, subs);
            }

            return true;
        }

        public ObservableCollection<CommonLibrarySE.Substation> GetSubstations()
        {
            return LoadSubstations();
        }

        public bool SaveSubstation(CommonLibrarySE.Substation substation)
        {
            try
            {
                ObservableCollection<CommonLibrarySE.Substation> SubstationCollection = LoadSubstations();

                Debug.WriteLine("Adding new work plan before saving...");
                SubstationCollection.Add(substation);

                return SaveSubstations(SubstationCollection);
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
