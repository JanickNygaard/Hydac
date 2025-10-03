using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Channels;

namespace KommeGåSystem
{
    internal class DataHandler
    {
        private string dataFileName;

        public string DataFileName { get { return dataFileName; } set { dataFileName = value; } }



        public DataHandler(string dataFileName) 
        { 
            this.dataFileName = dataFileName;
        }

        public void CreateFile()
        {
            // Tjek om filen eksistere ved at åbne og læse fra den.
            // Hvis den ikke eksisterer så laver vi en ny fil med StreamWriter og lukker den
            try
            {
                StreamReader sr = new StreamReader(DataFileName);
                sr.Close();
            }
            catch
            {
                StreamWriter sr = new StreamWriter(DataFileName);
                sr.Close();
            }

        }

        public void SaveRegistrations(List<Registration> registrations)
        {
            StreamWriter sw = new StreamWriter(DataFileName);

            foreach (Registration registration in registrations)
            {
                sw.WriteLine(registration.ToString());
            }
            sw.Close();
        }

        public List<Registration> LoadRegistrations()
        {
                StreamReader sr = new StreamReader(DataFileName);
            
            List<Registration> registrations = new List<Registration>();

            while (!sr.EndOfStream)
            {
                string regString = sr.ReadLine();

                string[] regArr = regString.Split(";");

                Guest guest = new Guest();
                Employee employee;
                Registration registration = new Registration();

                if (regArr[4] != "")
                {

                    guest.Name = regArr[4];
                    guest.Company = regArr[5];
                    guest.ResponsibleEmployee = regArr[6];

                    registration.Arrival = DateTime.Parse(regArr[0]);
                    registration.Departure = DateTime.Parse(regArr[1]);
                    registration.employee = null;
                    registration.guest = guest;


                }
                
                if (regArr[2] != "")
                {
                    employee = new Employee(
                    int.Parse(regArr[3]), regArr[2]);

                    employee.EmployeeNumber = int.Parse(regArr[3]);
                    employee.Name = regArr[2];

                    registration = new Registration(
                    DateTime.Parse(regArr[0]),
                    DateTime.Parse(regArr[1]), employee, null);

                }


                registrations.Add(registration);


            }

            sr.Close();

            return registrations;
        }
    }
}
