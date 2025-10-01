using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KommeGåSystem
{
    internal class Registration
    {
        private DateTime arrival = DateTime.MinValue;
        private DateTime departure = DateTime.MinValue;

        public Employee? employee;
        public Guest? guest;

        public DateTime Arrival { get { return arrival; } set { arrival = value; } }
        public DateTime Departure { get { return departure; } set { departure = value; } }

        public Registration()
        {
        }

        public Registration(DateTime arrival, DateTime departure, Employee? employee, Guest? guest)
        {
            Arrival = arrival;
            Departure = departure;
            this.employee = employee;
            this.guest = guest;
        }

        public override string ToString()
        {
            //0 = Arrival
            //1 = Departure
            // 2 = Employee name
            // 3 = Employee number
            // 4 = Guest name
            // 5 = company
            // 6 = responsible employee
            // 7 = safetydocs

            return $"{Arrival};" +
                $"{Departure};" +
                $"{(employee == null ? "" : employee.Name)};" +
                $"{(employee == null ? "" : employee.EmployeeNumber)};" +
                $"{(guest == null ? "" : guest.Name)};" +
                $"{(guest == null ? "" : guest.Company)};" +
                $"{(guest == null ? "" : guest.ResponsibleEmployee)};" +
                $"{(guest == null ? "" : guest.SafetyDocs)}";
        }


    }
}
