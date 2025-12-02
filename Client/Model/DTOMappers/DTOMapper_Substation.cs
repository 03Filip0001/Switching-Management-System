using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Switching_Management_System_Client.Model.DTOMappers
{
    public static partial class DTOMapper
    {
        public static class Substation
        {
            public static List<CommonLibrarySE.Substation> ToSubstation(ObservableCollection<ServerReference.Substation> sub)
            {
                List<CommonLibrarySE.Substation> substations = new List<CommonLibrarySE.Substation>();
                foreach(var subs in sub)
                {
                    Debug.Print("Substation: " + subs.Name);
                    CommonLibrarySE.Substation s = new CommonLibrarySE.Substation
                    {
                        ID = subs.ID,
                        Name = subs.Name,
                        Feeders = new ObservableCollection<CommonLibrarySE.Feeder>(),
                    };

                    foreach(var feed in subs.Feeders)
                    {
                        Debug.Print("Feeder: " + feed.ID.ToString());
                        CommonLibrarySE.Feeder f = new CommonLibrarySE.Feeder
                        {
                            ID = feed.ID,
                            Name = feed.Name,
                            Switches = new ObservableCollection<CommonLibrarySE.Switch>(),
                        };

                        foreach(var sw in feed.Switches)
                        {
                            Debug.Print("Switch: " + sw.Switch_ID.ToString());
                            CommonLibrarySE.Switch ss = new CommonLibrarySE.Switch
                            {
                                ID = sw.Switch_ID,
                                State = sw.State
                            };
                            f.Switches.Add(ss);
                        }
                        s.Feeders.Add(f);
                    }
                    substations.Add(s);
                }
                
                return substations;
            }
        }
    }
}
