using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KommeGåSystem
{
    internal class Guest
    {
        private string name;
        private string company;
        private string responsibleEmployee;

        public string Name { get { return name; } set { name = value; } }
        public string Company { get { return company; } set { company = value; } }
        public string ResponsibleEmployee { get { return responsibleEmployee; } set { responsibleEmployee = value; }}

        public Guest()
        {
            Name = "";
            Company = "";
            ResponsibleEmployee = "";
        }

        public Guest(string name, string company, string responsibleEmployee)
        {
            Name = name;
            Company = company;
            ResponsibleEmployee = responsibleEmployee;
        }
    }
}
