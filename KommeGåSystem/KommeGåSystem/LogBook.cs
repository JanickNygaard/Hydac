using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace KommeGåSystem
{
    internal class LogBook
    {
        public List<Registration> registrations = new List<Registration>();

        public void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("""
                Velkommen til min virksomhed.
                Vælg en af følgende muligheder:
                1. Medarbedjer
                2. Gæst


                0. Luk system

                """);
        }

        public void ShowEmployeeMenu()
        {
            Console.Clear();
            Console.WriteLine("""
                Du har valgt Medarbejder.
                Vælg en af følgende muligheder
                1. Tjek ind
                2. Tjek ud
                3. Overblik


                0. Tilbage til hoved menu

                """);
        }

        public void ShowGuestMenu()
        {
            Console.Clear();
            Console.WriteLine("""
                Du har valgt Gæst.
                Vælg en af følgende muligheder
                1. Tjek ind
                2. Tjek ud
                

                0. Tilbage til hoved menu

                """);
        }

        public int SelectMenuItem(int menuSize)
        {
            int itemId = new();

            bool itemIdValid = false;

            while (!itemIdValid)
            {
                Console.Write("Vælg menupunkt: ");

                bool itemOK = int.TryParse(Console.ReadLine(), out itemId);

                if (!itemOK)
                {
                    Console.WriteLine("Der er sket en fejl. Du må kun taste heltal ind!");
                    continue;
                }

                if (itemId >= 0 && itemId <= menuSize)
                {
                    itemIdValid = true;
                }
                else
                {
                    Console.WriteLine($"{itemId}: Er ikke et menupunkt. Prøv igen");
                    continue;
                }
            }
            return itemId;
        }

        public void GuestRegistration(List<Employee> employeelist)
        {
            bool bTryAgain = true;

            
            Guest g = new Guest();
            Registration r = new Registration();
            Console.Clear();
            Console.WriteLine("Du har valgt: Tjek ind.");
            Console.Write("indtast navn: ");
            g.name =  Console.ReadLine();
            Console.Write("indtast firma: ");
            g.company = Console.ReadLine();

            while (bTryAgain)
            {
                Console.Write("indtast ansvarlig for besøg: ");
                g.responsibleEmployee = Console.ReadLine();



                foreach (Employee employee in employeelist)
                {
                    if (employee.name == g.responsibleEmployee)
                    {
                        r.Arrival = DateTime.Now;
                        r.guest = g;
                        registrations.Add(r);

                        Console.WriteLine($"""

                            Navn: {g.name}
                            Firma: {g.company}

                            Er nu tjekket ind.

                            """);

                        Console.WriteLine($"Du kan nu ringe til {g.responsibleEmployee} og melde din ankomst.");
                        Console.WriteLine("Husk at tage en sikkersfolder. ");

                        g.safetyDocs = true;

                        Console.WriteLine();
                        Console.WriteLine("Tryk på vilkårlig tast for at komme tilbage til hovedmenu.");
                        Console.ReadKey();
                        return;
                    }


                }
                Console.Write("Medarbejder ikke fundet. Prøv igen? [ja/nej]: ");

                string tryAgain = Console.ReadLine();

                switch (tryAgain)
                {
                    case "ja":
                        continue;

                    default:
                        bTryAgain = false;
                        break;
                }
            }
        }

        public void EmployeeRegistration(List<Employee> employeelist)
        {
            bool bTryAgain = true;

            Employee e = new Employee();
            Registration r = new Registration();
            while (bTryAgain)
            {
                Console.Clear();

                string message = "Medarbejder ikke fundet.";
                bool alreadyCheckedIn = false;

                try
                {
                    Console.WriteLine("Du har valgt: Tjek ind.");
                    Console.Write("Indtast medarbejdernummer: ");
                    e.employeeNumber = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Kun heltal er tilladt. Tryk på vilkårlig tast for at prøve igen.");
                    Console.ReadKey();
                    continue;
                }

                foreach (Employee employee in employeelist)
                {
                    if (employee.employeeNumber == e.employeeNumber)
                    {

                        foreach (Registration re in registrations)
                        {
                            if (re.employee == null)
                                continue;
                            else if (re.employee.employeeNumber == e.employeeNumber)
                            {

                                Console.WriteLine($"""

                                    Navn: {re.employee.name}
                                    Medarbdjernummer: {re.employee.employeeNumber}
                                    """);

                                message = "Er allerede tjekket ind, og kan ikke tjekkes ind igen.";
                                alreadyCheckedIn = true;
                            }
                        }
                        if (!alreadyCheckedIn)
                        {
                            e.name = employee.name;
                            r.Arrival = DateTime.Now;
                            r.employee = e;
                            registrations.Add(r);

                            Console.WriteLine($"""

                            Navn: {employee.name}
                            Medarbdjernummer: {employee.employeeNumber}

                            Er nu tjekket ind.

                            """);
                            Console.WriteLine("Tryk på vilkårlig tast for at komme tilbage til hovedmenu.");
                            Console.ReadKey();
                            return;
                        }
                        
                    }
                }

                Console.Write(message + " Prøv igen? [ja/nej]: ");

                string tryAgain = Console.ReadLine();

                switch (tryAgain)
                {
                    case "ja":
                        continue;

                    default:
                        bTryAgain = false;
                        break;
                }


            }
        }

        public void EmployeeCheckout()
        {
            bool bTryAgain = true;
            int tempEmployeeNumber = 0;
            DateTime tempDT = DateTime.MinValue;

            while (bTryAgain)
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("Du har valgt: Tjek ud.");
                    Console.Write("Indtast medarbejdernummer: ");
                    tempEmployeeNumber = int.Parse(Console.ReadLine());

                }
                catch
                {
                    Console.WriteLine("Kun heltal er tilladt. Tryk på vilkårlig tast for at prøve igen.");
                    Console.ReadKey();
                    continue;
                }

                foreach (Registration r in registrations)
                {
                    if (r.employee == null)
                        continue;
                    else if (r.employee.employeeNumber == tempEmployeeNumber && r.Departure == tempDT)
                    {
                        r.Departure = DateTime.Now;

                        Console.WriteLine($"""

                            Navn: {r.employee.name}
                            Medarbdjernummer: {r.employee.employeeNumber}

                            Er nu tjekket ud.

                            """);
                        Console.WriteLine("Tryk på vilkårlig tast for at komme tilbage til hovedmenu.");
                        Console.ReadKey();
                        return;
                    }
                }

                Console.Write("""

                    Kan ikke tjekke en medarbejder ud, 
                    som ikke har være tjekket ind eller allerede er tjekket ud.
                    Prøv igen? [ja/nej]: 
                    """);

                string tryAgain = Console.ReadLine();

                switch (tryAgain)
                {
                    case "ja":
                        continue;

                    default:
                        bTryAgain = false;
                        break;
                }
            }
        }

        public void GuestCheckout()
        {
            bool bTryAgain = true;
            DateTime tempDT = DateTime.MinValue;

            while (bTryAgain)
            {
                Console.Clear();
                Console.WriteLine("Du har valgt: Tjek ud.");
                Console.Write("Indtast navn: ");
                string tempGuestName = Console.ReadLine();

                foreach (Registration r in registrations)
                {
                    if (r.guest == null)
                        continue;
                    else if (r.guest.name == tempGuestName && r.Departure == tempDT)
                    {
                        r.Departure = DateTime.Now;

                        Console.WriteLine($"""

                            Navn: {r.guest.name}
                            Firma: {r.guest.company}

                            Er nu tjekket ud.

                            """);
                        Console.WriteLine("Tryk på vilkårlig tast for at komme tilbage til hovedmenu.");
                        Console.ReadKey();
                        return;
                    }
                }

                Console.Write("""

                    Kan ikke tjekke en gæst ud, 
                    som ikke har være tjekket ind eller allerede er tjekket ud.
                    Prøv igen? [ja/nej]: 
                    """);

                string tryAgain = Console.ReadLine();

                switch (tryAgain)
                {
                    case "ja":
                        continue;

                    default:
                        bTryAgain = false;
                        break;
                }
            }
        }


        public void Overview()
        {
            Console.Clear();
            Console.WriteLine($"Overblik over folk i virksomheden idag [{DateTime.Today.ToString("dd/MM/yyyy")}].");
            Console.WriteLine();

            foreach (Registration r in registrations)
            {
                if (r.guest != null)
                {
                    Console.WriteLine($"""
                        #######Gæst######
                        Navn: {r.guest.name}
                        Firma: {r.guest.company}
                        Ansvarlig: {r.guest.responsibleEmployee}
                        Ankomst: {r.Arrival.ToString("HH:mm:ss")}
                        Afgang: {(r.Departure == DateTime.MinValue ? "" : r.Departure.ToString("HH:mm:ss"))}

                        """);
                }
                else if (r.employee != null)
                {
                    Console.WriteLine($"""
                        #######Medarbejder######
                        Navn: {r.employee.name}
                        Medarbejdernummer: {r.employee.employeeNumber}
                        Ankomst: {r.Arrival.ToString("HH:mm:ss")}
                        Afgang: {(r.Departure == DateTime.MinValue ? "" : r.Departure.ToString("HH:mm:ss"))}

                        """);
                }
            }

            Console.WriteLine("Tryk på vilkårlig tast for at komme tilbage til hovedmenu.");
            Console.ReadKey();
        }














    }
}
