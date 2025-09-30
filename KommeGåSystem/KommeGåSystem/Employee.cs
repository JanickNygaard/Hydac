using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KommeGåSystem
{
    internal class Employee
    {
        public int employeeNumber;
        public string name;

        public Employee()
        {
            employeeNumber = 0;
            name = "";

        }


        public Employee(int employeeNumber, string name)
        {
            this.employeeNumber = employeeNumber;
            this.name = name;
        }
    }
}
