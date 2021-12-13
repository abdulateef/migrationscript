using System;
using System.Collections.Generic;
using System.Text;

namespace OctopusVariableScript
{
    public class  Appsetting
    {
        public Appsetting()
        {
            Variables = new List<variable>();
        }
        public List<variable> Variables { get; set; }
    }

    public class variable
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }

}
