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

        public DateTime Arrival
        {
            get { return arrival; }
            set { arrival = value; }
        }

        public DateTime Departure
        {
            get { return departure; }
            set { departure = value; }
        }

        //public Employee? employee = new Employee();
        //public Guest? guest = new Guest();
        public Employee? employee;
        public Guest? guest;

        /*public void Arrive()
        {
            arrival = DateTime.Now;
        }*/

        /*public void Depart()
        {
            departure = DateTime.Now;
        }*/
    }
}
