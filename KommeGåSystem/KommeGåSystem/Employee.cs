using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KommeGåSystem
{
    public class Employee
    {
        private int employeeNumber;
        private string name;

        public int EmployeeNumber { get { return employeeNumber; } set { employeeNumber = value; } }
        public string Name { get { return name; } set { name = value; } }

        public Employee()
        {
            employeeNumber = 0;
            name = "";

        }

        public Employee(int employeeNumber, string name)
        {
            EmployeeNumber = employeeNumber;
            Name = name;
        }
    }
}
