using Mini_Switching_Management_System_Client.Model.Binding;

namespace Mini_Switching_Management_System_Client.Model.DTOMappers
{
    public static partial class DTOMapper
    {
        public static class Switch
        {
            public static SwitchBinding ToSwitchBinding(CommonLibrarySE.Switch sw)
            {
                return new SwitchBinding { ID = sw.ID, State = sw.State };
            }

            public static CommonLibrarySE.Switch ToSwitch(SwitchBinding sw)
            {
                return new CommonLibrarySE.Switch { ID = sw.ID, State = sw.State };
            }
        }
    }
}
