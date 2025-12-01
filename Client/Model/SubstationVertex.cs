using GraphX.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Switching_Management_System_Client.Model
{
    public class SubstationVertex : VertexBase
    {
        public int ID { get; set; }
        public string Name {  get; set; }

        public SubstationVertex() : this(string.Empty)
        {
        }

        public SubstationVertex(string name = "Test name")
        {
            Name = name;
        }
    }
}
