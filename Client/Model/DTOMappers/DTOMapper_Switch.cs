using Mini_Switching_Management_System_Client.Model.Binding;

namespace Mini_Switching_Management_System_Client.Model.DTOMappers
{
    public static partial class DTOMapper
    {
        public static class Switch
        {
            public static SwitchDTO ToSwitchBinding(CommonLibrarySE.Switch sw)
            {
                return new SwitchDTO { ID = sw.ID, State = sw.State };
            }

            public static CommonLibrarySE.Switch ToSwitch(SwitchDTO sw)
            {
                return new CommonLibrarySE.Switch { ID = sw.ID, State = sw.State };
            }
        }
    }
}
