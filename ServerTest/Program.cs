using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace ServerTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server.Service1Client client = new Server.Service1Client();

            ObservableCollection<Server.Substation> substations = client.GetSubstations();

            if (substations.Count == 0)
            {
                substations.Add(new Server.Substation());
            }

            substations[0].Feeders = new ObservableCollection<Server.Feeder>();

            Server.Switch s1 = new Server.Switch { Switch_ID = 1, State=true};
            Server.Switch s2 = new Server.Switch { Switch_ID = 2, State = false};
            Server.Switch s3 = new Server.Switch { Switch_ID = 3, State=false};

            Server.Switch s4 = new Server.Switch { Switch_ID = 4, State = true };
            Server.Switch s5 = new Server.Switch { Switch_ID = 5, State = true };

            Server.Feeder f1 = new Server.Feeder { ID = 0, Name = "Petrovaradin", Switches = new ObservableCollection<Server.Switch>()};
            Server.Feeder f2 = new Server.Feeder { ID = 1, Name = "Novi Sad", Switches = new ObservableCollection<Server.Switch>() };

            f1.Switches.Add(s1);
            f1.Switches.Add(s2);
            f1.Switches.Add(s3);

            f2.Switches.Add(s4);
            f2.Switches.Add(s5);

            substations[0].Feeders.Add(f1);
            substations[0].Feeders.Add(f2);

            bool res = client.SaveSubstation(substations[0]);
            client.Close();

            if (res == true)
            {
                Debug.Print("Successfully saved new Substation");
            }
            else
            {
                Debug.Print("COULD NOT SAVE SUBSTATION");
            }
        }
    }
}
