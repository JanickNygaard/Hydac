namespace KommeGåSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LogBook lb = new LogBook();

            // Employee list
            List<Employee> employees = new List<Employee>{
                new Employee(100, "Jens Jensen"),
                new Employee(101, "Hans Hannsen"),
                new Employee(102, "Bo Bosen"),
                new Employee(103, "Frank Franksen")
                };

            bool runOK = true;
            // Start
            while (runOK)
            {
                bool guestChoice = false;
                bool employeeChoice = false;
                int itemId = 0;

                lb.ShowMenu();
                itemId = lb.SelectMenuItem(2);

                switch (itemId)
                {
                    case 0:
                        runOK = false;
                        break;
                    case 1:
                        lb.ShowEmployeeMenu();
                        employeeChoice = true;
                        itemId = lb.SelectMenuItem(3);
                        break;

                    case 2:
                        lb.ShowGuestMenu();
                        guestChoice = true;
                        itemId = lb.SelectMenuItem(2);
                        break;
                }
                
                if (employeeChoice)
                {
                    switch (itemId)
                    {
                        case 0:
                            break;

                        case 1:
                            lb.EmployeeRegistration(employees);
                            break;
                        case 2:
                            lb.EmployeeCheckout();
                            break;
                        case 3:
                            lb.Overview();
                            break;
                    } 
                }
                else if (guestChoice)
                {
                    switch (itemId)
                    {
                        case 0:
                            break;

                        case 1:
                            lb.GuestRegistration(employees);
                            break;
                        case 2:
                            lb.GuestCheckout();
                            break;
                    }

                    
                }
            }
            
        }
    }
}
