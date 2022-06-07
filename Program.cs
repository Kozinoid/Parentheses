using System;

namespace Parentheses
{
    // 
    // Open bracket count = closed bracket count
    // (!) in any place of bracket sequence 'PRINTED open bracket count' >= 'PRINTED' close bracket count
    // So, if
    // Open bracket '(' = 1,
    // Closed bracket ')' = -1 
    // then in any place of bracket sequence sum of 1 and -1 >= 0

    // Example for 3 open (and closed) brackets. Here is a tree, where 'o' - nodes. 
    //                                                      
    //--------------------------- sum=3 -------o            We can move from each node only 
    //----------------------------------------(-)           right-up (print '(' and sum = sum + 1) 
    //--------------------------- sum=2 -----o---o          or right-down (print ')' and sum = sum - 1)
    //--------------------------------------(-)-(-)         Each move meets the conditions:
    //--------------------------- sum=1 ---o---o---o        sum >= 0, sum <= open(closed) bracket count,
    //------------------------------------(-)-(-)-(-)       printed bracket count <= initial bracket count (3 open and 3 closed in this example)
    //--------------------------- sum=0 -o---o---o---o      

    class Program
    {
        static int count = 0;           // Global static variable to count the number of patterns (for output)
        static bool showResults = true; // if number of open/closed brackets more then 15, the process can take a long time, so it's good idea to turn off result patterns output

        static void Main(string[] args)
        {
            bool exit = false;
            do
            {
                Console.Write("Enter the number of open (or closed) brackets (0 - exit): ");
                string stringNumber = Console.ReadLine();
                if (int.TryParse(stringNumber, out int numberOfBrackets))
                {
                    if (numberOfBrackets > 0)
                    {
                        count = 0;          // Clear result
                        int sum = 0;        // 'Sum' of brackets
                        string res = "";    // Result pattern string
                        Console.WriteLine("----------------------------------");

                        // Call recursive function
                        if (numberOfBrackets >= 15)
                        {
                            showResults = false;    // if open/closed bracket count >= 15, turn off patterns output
                            Console.WriteLine("Please, wait...");
                        }
                        else
                        {
                            showResults = true;
                        }
                        // Call recursive algorithm
                        TestNode(numberOfBrackets, numberOfBrackets, sum, res, "");

                        Console.WriteLine("Total: {0}", count);
                        Console.WriteLine("----------------------------------");
                        Console.WriteLine();
                    }
                    else
                    {
                        exit = true;
                    }
                }
                else
                {
                    Console.WriteLine("Enter digits only, please!");
                }
            } while (!exit);
        }

        // Recursive function. open - number of open brackets left, closed - number of closed brackets left, sum - the 'sum of brackets' (1 or -1), res and add - for print pattern
        static void TestNode(int open, int close, int sum, string res, string add)
        {
            // ...add a bracket to result pattern string
            res += add;
            // Exit recursion condition
            if ((close == 0) && (open == 0))
            {
                count++;    // One more pattern was finished
                if (showResults) Console.WriteLine("{0, 16}: {1}", count, res);
                return;
            }

            if ((open - 1) >= 0)    // try to move 'right-down' to the next node with printing ')'
            {
                TestNode(open - 1, close, sum + 1, res, "(");
            }

            if (((close - 1) >= 0) && ((sum - 1) >= 0)) // try to move 'right-up' to the next node with printing '('
            {
                TestNode(open, close - 1, sum - 1, res, ")");
            }
        }
    }
}
