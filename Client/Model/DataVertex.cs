using GraphX.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Switching_Management_System_Client.Model
{
    public class DataVertex : VertexBase
    {
        public int ID { get; set; }
        public string Name {  get; set; }

        public DataVertex() : this(string.Empty)
        {
        }

        public DataVertex(string name = "Test name")
        {
            Name = name;
        }
    }
}
