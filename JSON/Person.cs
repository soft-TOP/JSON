using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;


namespace JSON
{
    [DataContract]
    public class Person
    {
        [DataMember]
        public String Name { get; set; }

        [DataMember]
        public Byte Alter { get; set; } // älter als 255 Jahre ist unwahrscheinlich
    }

}
