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
        private List<Registration> registrations = new List<Registration>();
        
        public List<Registration> Registrations { get {  return registrations; } set { registrations = value; } }

        public void ShowMenu()
        {
            // Viser hovedmenu
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
            // Viser medarbejdermenu
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
            // Viser gæstmenu
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
            // Returnere det valgte menu item som int, til at benytte i switch case
            int itemId = new();
            bool itemIdValid = false;

            // Fortsætter indtil itemIdValid er true
            while (!itemIdValid)
            {
                Console.Write("Vælg menupunkt: ");

                // Bruger TryParse hvis man inputter bogstaver kan vi fange exceptionen og håndterer den
                bool itemOK = int.TryParse(Console.ReadLine(), out itemId);

                if (!itemOK)
                {
                    Console.WriteLine("Der er sket en fejl. Du må kun taste heltal ind!");
                    continue;
                }

                // ItemId skal være mellem 0 og det int argument som metoden tager.
                // Hvis det ikke er mellem 0 og int arguemntet melder vi fejl og star while loop forfra.
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

            // instatierer objekter
            Guest g = new Guest();
            Registration r = new Registration();

            // datetime objeckt bliver sat ligemed MinValue
            //DateTime tempDT = DateTime.MinValue;

            //clearer skærmen beder om input
            Console.Clear();
            Console.WriteLine("Du har valgt: Tjek ind.");
            Console.Write("indtast navn: ");
            g.Name =  Console.ReadLine();
            Console.Write("indtast firma: ");
            g.Company = Console.ReadLine();

            // While loop kører så længe bTryAgain er true
            while (bTryAgain)
            {
                // Der spørges om ansvarlig for besøg i et while loop fordi vi tjekker op mod vores
                // medarbejder liste om den ansatte eksisterer
                Console.Write("indtast ansvarlig for besøg: ");
                g.ResponsibleEmployee = Console.ReadLine();


                // tjekker ansvar for besøg op mod medarbejder liste
                foreach (Employee employee in employeelist)
                {
                    if (employee.Name == g.ResponsibleEmployee)
                    {
                        // Hvis alt går godt, tjekkes gæsten ind.
                        r.Arrival = DateTime.Now;
                        r.guest = g;
                        registrations.Add(r);

                        
                        Console.WriteLine($"""

                            Navn: {g.Name}
                            Firma: {g.Company}

                            Er nu tjekket ind.

                            """);

                        Console.WriteLine($"Du kan nu ringe til {g.ResponsibleEmployee} og melde din ankomst.");
                        Console.WriteLine("Husk at tage en sikkersfolder. ");

                        g.SafetyDocs = true;

                        Console.WriteLine();
                        Console.WriteLine("Tryk på vilkårlig tast for at komme tilbage til hovedmenu.");
                        Console.ReadKey();
                        return;
                    }


                }
                // Hvis ansvarlig for besøg ikke blev fundet kan man prøve igen eller hoppe tilbage til hovedmenu.
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
            
            // instanterer objekter
            Employee e = new Employee();
            Registration r = new Registration();

            // datetime objeckt bliver sat ligemed MinValue
            DateTime tempDT = DateTime.MinValue;

            // While loop kører så længe at bTryAgain er true
            while (bTryAgain)
            {
                //clearer skærmen
                Console.Clear();

                // instantierer en message med fejl sætning
                string message = "Medarbejder ikke fundet.";
                bool alreadyCheckedIn = false;

                // spørger om medarbejder nummer, i try-catch fordi kun heltal er tilladt
                // Og her fanges den exception som Visual studio trower til os, som vi håndterer
                try
                {
                    Console.WriteLine("Du har valgt: Tjek ind.");
                    Console.Write("Indtast medarbejdernummer: ");
                    e.EmployeeNumber = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Kun heltal er tilladt. Tryk på vilkårlig tast for at prøve igen.");
                    Console.ReadKey();
                    continue;
                }

                // Tjekker om medabrjdernummer findes i medabrjder liste
                foreach (Employee employee in employeelist)
                {
                    if (employee.EmployeeNumber == e.EmployeeNumber)
                    {
                        // Tjekker om medarbejder allerede er tjekket ind
                        foreach (Registration re in registrations)
                        {
                            // Iregistration holdes der både styr på medarbdjer og gæste objekter.
                            // Hvis der findes et medarbdjer objekt som er null er det en gæst. og vi går videre til næste element i listen

                            if (re.employee == null)
                                continue;
                            else if (re.employee.EmployeeNumber == e.EmployeeNumber && re.Departure == tempDT)
                            {
                                // Når medabrjderen er fundet og allerede er tjekket ind.
                                Console.WriteLine($"""

                                    Navn: {re.employee.Name}
                                    Medarbdjernummer: {re.employee.EmployeeNumber}
                                    """);

                                message = "Er allerede tjekket ind, og kan ikke tjekkes ind igen.";
                                alreadyCheckedIn = true;
                            }
                        }
                        // Når medarbejder er fundet og ikke er tjekket ind, tjekkes medarbedjeren ind.
                        if (!alreadyCheckedIn)
                        {
                            e.Name = employee.Name;
                            r.Arrival = DateTime.Now;
                            r.employee = e;
                            registrations.Add(r);

                            Console.WriteLine($"""

                            Navn: {employee.Name}
                            Medarbdjernummer: {employee.EmployeeNumber}

                            Er nu tjekket ind.

                            """);
                            Console.WriteLine("Tryk på vilkårlig tast for at komme tilbage til hovedmenu.");
                            Console.ReadKey();
                            return;
                        }
                        
                    }
                }
                // Hvis medarbejder ikke blev fundet kan man prøve igen eller hoppe tilbage til hovedmenu.
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

            // datetime objeckt bliver sat ligemed MinValue
            DateTime tempDT = DateTime.MinValue;

            while (bTryAgain)
            {
                Console.Clear();

                // Beder om medarbejdernummer i try-catch så vi kan fange den exception visual studio
                // thrower til os hvis der bliver indtastet "ikke tal"
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

                // tjekker om medarbejder er ligger i registration listen
                foreach (Registration r in registrations)
                {
                    // Hvis medarbdjer objekt er null, er det et gæste objekt og vi køre loopet forfra med ny iteration med "continue"
                    if (r.employee == null)
                        continue;
                    // Hvis marbejder er tjekket ind og ikke tjekket ud, fordi departure time er MinValue som Departure er initialseret med
                    // Så tjekkes de ud.
                    else if (r.employee.EmployeeNumber == tempEmployeeNumber && r.Departure == tempDT)
                    {
                        r.Departure = DateTime.Now;

                        Console.WriteLine($"""

                            Navn: {r.employee.Name}
                            Medarbdjernummer: {r.employee.EmployeeNumber}

                            Er nu tjekket ud.

                            """);
                        Console.WriteLine("Tryk på vilkårlig tast for at komme tilbage til hovedmenu.");
                        Console.ReadKey();
                        return;
                    }
                }
                // Medabrjder kan ikke tjekkes ud, hvis de ikke er tjekket ind eller
                // allerede er tjekket ud.
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

                // tjekker om gæst er ligger i registration listen
                foreach (Registration r in registrations)
                {
                    // Hvis gæst objekt er null, er det et medarbejder objekt og vi køre loopet forfra med ny iteration med "continue"
                    if (r.guest == null)
                        continue;
                    // Hvis gæest er tjekket ind og ikke tjekket ud, fordi departure time er MinValue som Departure er initialseret med
                    // Så tjekkes de ud.
                    else if (r.guest.Name == tempGuestName && r.Departure == tempDT)
                    {
                        r.Departure = DateTime.Now;

                        Console.WriteLine($"""

                            Navn: {r.guest.Name}
                            Firma: {r.guest.Company}

                            Er nu tjekket ud.

                            """);
                        Console.WriteLine("Tryk på vilkårlig tast for at komme tilbage til hovedmenu.");
                        Console.ReadKey();
                        return;
                    }
                }
                // Gæst kan ikke tjekkes ud, hvis de ikke er tjekket ind eller
                // allerede er tjekket ud.
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
            // Viser overblik for dags dato
            Console.Clear();
            Console.WriteLine($"Overblik over registreringer.");
            Console.WriteLine();

            // kigger registration listen igemmen for gæst og medarbejder objekter
            foreach (Registration r in registrations)
            {
                // Hvis gæste obtjekt er fundet så udskrives det med relevante data og tidsstempel
                if (r.guest != null)
                {
                    // Hvis der ikke er sat en departure som IKKE er minvalue så vise der ikke noget. Ellers vises en tid
                    Console.WriteLine($"""
                        #######Gæst######
                        Navn: {r.guest.Name}
                        Firma: {r.guest.Company}
                        Ansvarlig: {r.guest.ResponsibleEmployee}
                        Ankomst: {r.Arrival.ToString("dd/MM/yyyy HH:mm:ss")}
                        Afgang: {(r.Departure == DateTime.MinValue ? "" : r.Departure.ToString("dd/MM/yyyy HH:mm:ss"))}

                        """);
                }
                // Hvis medarbejder obtjekt er fundet så udskrives det med relevante data og tidsstempel
                else if (r.employee != null)
                {
                    // Hvis der ikke er sat en departure som IKKE er minvalue så vise der ikke noget. Ellers vises en tid
                    Console.WriteLine($"""
                        #######Medarbejder######
                        Navn: {r.employee.Name}
                        Medarbejdernummer: {r.employee.EmployeeNumber}
                        Ankomst: {r.Arrival.ToString("dd/MM/yyyy HH:mm:ss")}
                        Afgang: {(r.Departure == DateTime.MinValue ? "" : r.Departure.ToString("dd/MM/yyyy HH:mm:ss"))}

                        """);
                }
            }

            Console.WriteLine("Tryk på vilkårlig tast for at komme tilbage til hovedmenu.");
            Console.ReadKey();
        }














    }
}
