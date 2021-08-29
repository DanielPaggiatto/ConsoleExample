using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace example
{
    class Program
    {
        static List<EletricalSystem> items = new List<EletricalSystem>();

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("*************** Welcome ***************");

            int option = 0;

            while (option != 5)
            {
                DrawMenu();

                if (int.TryParse(Console.ReadLine(), out option))
                {
                    bool optValid = IsOptionValid(option);

                    if (!optValid)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\r\nInvalid option, try again.");
                        Thread.Sleep(2000);
                        continue;
                    }

                    //logic (add/remove/exit)...
                    DoAction(option);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\r\nInvalid number, try again.");
                }

                Thread.Sleep(2000);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Program exiting...");

            Thread.Sleep(5000);
        }

        static void DrawMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("\r\n\r\nSelect desired option:\r\n");
            Console.WriteLine("1 - Add System.");
            Console.WriteLine("2 - Remove System.");
            Console.WriteLine("3 - Make Excel.");
            Console.WriteLine("4 - List.");
            Console.WriteLine("5 - Exit.");
            Console.Write("\r\nOption: ");
        }

        static bool IsOptionValid(int option)
        {
            if (option == 1 ||
                    option == 2 ||
                    option == 3 ||
                    option == 4 ||
                    option == 5)
                return true;

            return false;
        }

        static void DoAction(int option)
        {
            Console.WriteLine();
            Console.WriteLine();

            switch (option)
            {
                case 1:
                    Add();
                    break;
                case 2:
                    Remove();
                    break;
                case 3:
                    Excel();
                    break;
                case 4:
                    List();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Very thanks for using this program...");
                    break;
            }
        }

        static void Add()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;

            bool aux = false;
            string isPj = null;
            string interval = null;
            int quantity = 0;
            decimal totalWats = 0;

            while (!aux)
            {
                Console.Write("Is Pj (Y/N): ");
                isPj = Console.ReadLine();

                if (string.IsNullOrEmpty(isPj))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\r\nPj was not informed.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (!isPj.ToUpper().Equals("Y") && !isPj.ToUpper().Equals("N"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\r\nPj is invalid, try again.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                    aux = true;
            }

            aux = false;

            while (!aux)
            {
                Console.Write("Interval: ");
                interval = Console.ReadLine();

                if (string.IsNullOrEmpty(interval))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\r\nInterval was not informed.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                    aux = true;
            }

            aux = false;

            while (!aux)
            {
                Console.Write("Quantity: ");
                string temp = Console.ReadLine();

                if (!int.TryParse(temp, out quantity))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\r\nInvalid Number.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                    aux = true;
            }

            aux = false;

            while (!aux)
            {
                Console.Write("Total Watts: ");
                string temp = Console.ReadLine();

                if (!decimal.TryParse(temp, out totalWats))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\r\nInvalid Number (Ex.: 10.25).");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                    aux = true;
            }

            EletricalSystem item = new EletricalSystem();
            item.Interval = interval;
            item.IsPj = (isPj == "Y");
            item.Quantity = quantity;
            item.TotalWats = totalWats;

            items.Add(item);
        }

        static void Remove()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;

            int index = 0;

            Console.Write("Input the desired index to remove: ");

            if (int.TryParse(Console.ReadLine(), out index))
            {
                if (index < 1 || index > items.Count)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid item (index).");
                }
                else
                {
                    index--;
                    items.RemoveAt(index);
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid number, try again.");
            }
        }

        static void Excel()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;

            using (StreamWriter stream = new StreamWriter(".\\systems.csv", false, Encoding.Default))
            {
                stream.Write("IsPj;Interval;Quantity;Total Watts (Kw);\r\n");

                foreach (EletricalSystem item in items)
                {
                    string content = null;

                    if (item.IsPj)
                        content = "Sim";
                    else
                        content = "Nao";

                    content += ";";

                    content += "\"" + item.Interval + "\";";
                    content += item.Quantity + ";";
                    content += item.TotalWats + ";";

                    stream.Write(content + "\r\n");
                }
            }

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Data was exported to CSV format.");
        }

        static void List()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            int aux = 1;

            foreach (EletricalSystem item in items)
            {
                Console.WriteLine(
                    "Index: " + aux +
                    " - " +
                    "Interval: " + item.Interval +
                    " - " +
                    "Quantity: " + item.Quantity +
                    " - " +
                    "Total Watts: " + item.TotalWats);

                aux++;
            }
        }
    }
}
