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

            substations[0].Feeders = new ObservableCollection<Server.Feeder>();

            Server.Switch s1 = new Server.Switch { Switch_ID = 1};
            Server.Switch s2 = new Server.Switch { Switch_ID = 2};
            Server.Feeder f1 = new Server.Feeder { ID = 0, Name = "test", Switches = new ObservableCollection<Server.Switch>()};
            f1.Switches.Add(s1);
            f1.Switches.Add(s2);

            substations[0].Feeders.Add(f1);

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
