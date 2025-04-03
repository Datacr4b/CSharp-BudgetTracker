using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Budget_Tracker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IDictionary<string,decimal> expenses = new Dictionary<string,decimal>();
            IDictionary<string,decimal> income = new Dictionary<string,decimal>();
            string entry;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Income (i) Expenses (e) Summary (s) or Quit (q): ");
                entry = Console.ReadLine();
                if (entry.ToLower() == "i")
                    AddEntry(income, entry);
                else if (entry.ToLower() == "e")
                    AddEntry(expenses, entry);
                else if (entry.ToLower() == "s")
                    DisplayResult(income, expenses);
                else if (entry.ToLower() == "q")
                    break;
                else
                    Console.WriteLine("Invalid Input");
            }
            DisplayResult(income, expenses);
        }

        static void AddEntry(IDictionary<string, decimal> dict, string entry)
        {
            string source;
            string backInput;
            decimal amount;
            string sourceText = entry.ToLower() == "i" ? "Income" : "Expenses";

            while (true) // Main Loop
            {
                Console.WriteLine("Please enter the source of " + sourceText + " (press b to go back): ");
                source = Console.ReadLine();
                if (source.ToLower() == "b")
                    break;
                else if (source.Trim() != "")
                {
                    while(true)
                    {
                        Console.WriteLine("Please enter the amount (press b to go back): ");
                        backInput = Console.ReadLine();
                        if (backInput.ToLower() == "b")
                            break;
                        bool success = Decimal.TryParse(backInput, out amount);
                        if (success)
                        {
                            if (amount < 0)
                                Console.WriteLine("Negative Numbers aren't valid");
                            else
                            {
                                if (!dict.ContainsKey(source.ToLower()))
                                {
                                    dict.Add(source.ToLower(), amount);
                                    break;
                                }
                                else
                                {
                                    dict[source.ToLower()] += amount;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (backInput == "")
                                Console.WriteLine("Amount cannot be empty");
                            else
                                Console.WriteLine("Invalid amount input");
                        }
                    }
                }
                else
                    Console.WriteLine("Invalid Source of " + sourceText);
            }
        }

        static void DisplayResult(IDictionary<string, decimal> income, IDictionary<string, decimal> expenses)
        {
            decimal calculation;
            calculation = income.Values.Sum() - expenses.Values.Sum();
            Console.Clear();
            Console.WriteLine("Your total balance is: {0:C}", calculation);
            Console.WriteLine("------");
            Console.WriteLine("Expenses: ");
            foreach (KeyValuePair<string, decimal> kvpExpenses in expenses)
                Console.WriteLine("Source: {0} Amount: {1:C} ", kvpExpenses.Key, kvpExpenses.Value);
            Console.WriteLine("------");
            Console.WriteLine("Income: ");
            foreach (KeyValuePair<string, decimal> kvpIncome in income)
                Console.WriteLine("Source: {0} Amount: {1:C} ", kvpIncome.Key, kvpIncome.Value);
            Console.ReadLine();
        }
    }
}
