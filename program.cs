using System;
using System.Threading;

namespace PlinkoDemo
{
    class Program
    {
        private static int totalGems = 100;
        private const int minGemRequired = 1;
        

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Plinko Game Demo!");

            while (true)
            {
                Console.WriteLine($"You have {totalGems} gems.");
                if (totalGems >= minGemRequired)
                {
                    Console.WriteLine($"Each play costs minimum {minGemRequired} gem(s). Would you like to play? (yes/no)");
                    string userInput = Console.ReadLine().ToLower();

                    if (userInput == "yes")
                    {
                        int betAmount = determineBet();
                        totalGems -= betAmount;
                        int slot = PlayPlinko();
                        int reward = GetReward(slot);
                        int payout = reward * betAmount;
                        totalGems += payout;
                        Console.WriteLine($"You won {payout} gems!");
                    }
                    else if (userInput == "no")
                    {
                        Console.WriteLine("Thank you for playing! Goodbye!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please type 'yes' or 'no'.");
                    }
                }
                else
                {
                    Console.WriteLine("You do not have enough gems to play. Goodbye!");
                    break;
                }
            }
        }

        static int determineBet()
        {
            Console.WriteLine("Enter bet amount");
            int bet;
            int bAmount = 0; // initialize to zero
            while (!int.TryParse(Console.ReadLine(), out bet) || bet > totalGems || bet < 0)            {
                Console.WriteLine("Invalid input. Please enter a number less than your totalGems.");
            }
            if (bet <= totalGems)
            {
                bAmount = minGemRequired * bet;
            }
            return bAmount;
        }

        static int PlayPlinko()
        {
            Console.WriteLine("Choose a column to drop the ball (1-7):");
            int column;
            while (!int.TryParse(Console.ReadLine(), out column) || column < 1 || column > 7)
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 7.");
            }

            int currentRow = 0;
            int currentColumn = column * 2 - 1;
            int numRows = 7;
            int numCols = 13;

            Random rand = new Random();
            while (currentRow < numRows)
            {
                DrawBoard(currentRow, currentColumn, numRows, numCols);
                currentRow++;

                if (currentColumn > 1 && currentColumn < numCols - 1)
                {
                    int move = rand.Next(0, 2);
                    if (move == 1)
                    {
                        currentColumn += 2;
                    }
                    else
                    {
                        currentColumn -= 2;
                    }
                }

                Thread.Sleep(500);
            }

            int slot = (currentColumn + 1) / 2;
            Console.WriteLine($"The ball landed in slot {slot}.");
            return slot;
        }

        static void DrawBoard(int currentRow, int currentColumn, int numRows, int numCols)
        {
            Console.Clear();
            for (int row = 0; row < numRows; row++)
            {
                for (int col = 0; col < numCols; col++)
                {
                    if (col % 2 == 0 && row % 2 == 0)
                    {
                        Console.Write("O");
                    }
                    else if (row == currentRow && col == currentColumn)
                    {
                        Console.Write("*");
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }

        static int GetReward(int slot)
        {
            int[] rewards = { 8, 4, 2, 2, 3, 8 };
            return rewards[slot - 1];
        }
    }
}
