using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvoidMakingSquares
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("In this game, you and the computer take turns placing pieces on a board");
            Console.WriteLine("Try and avoid making your pieces form a square!");
            Console.WriteLine("press any key to begin");
            Console.ReadKey();

            game g = new game();
            bool programRun = true;
            bool gameEnd = false;
            bool playerWin = false;
            bool CPUWin = false;
            int r, c;
            int[] rc;
            string playerInput;

            while (programRun)
            {
                Console.Clear();
                //main game loop
                while (!gameEnd)
                {
                    g.draw();
                    //player move
                    while (true)
                    {
                        Console.WriteLine("please enter the row or column you wish to place a piece on");
                        playerInput = Console.ReadLine();
                        rc = getRowColumnFromUser(playerInput);
                        r = rc[0];
                        c = rc[1];
                        if (r >= 0 && r < g.size && c >= 0 && c < g.size)
                        {
                            if (!g.empty(r, c)) { Console.WriteLine("Please pick an empty space"); }
                            else
                            {
                                g.PlayerMove(r, c);
                                if (g.square(r, c, 1)) { gameEnd = true; CPUWin = true; break; }
                                break;
                            }
                        }
                        else { Console.WriteLine("That doesn't look like a valid move to me..."); }
                    }
                    //show the player move
                    g.draw();
                    Console.ReadKey();
                    //computer move
                    rc = g.ComputerMove();
                    if (g.square(rc[0], rc[1], 2)) { gameEnd = true; playerWin = true; break; }
                    if (g.full()) { gameEnd = true; break; }
                    if (playerWin || CPUWin) { gameEnd = true; break;}
                }
                g.clear();
                if (!playerWin && !CPUWin) { Console.WriteLine("It's a tie!"); }
                else if (playerWin) { Console.WriteLine("YOU'RE WINNER"); g.PlayerScore++; }
                else { Console.WriteLine("LOSER!!"); g.ComputerScore++; }

                while (true)
                {
                    Console.WriteLine("You wanna play again? Y/N");
                    playerInput = Console.ReadLine();

                    if (playerInput == "Y" || playerInput == "y") { playerWin = false; CPUWin = false; gameEnd = false; break; }
                    if (playerInput == "N" || playerInput == "n") { programRun = false; break; }
                }
            }
            Console.Clear();
            Console.WriteLine("See you later space cowboy...");
            Console.ReadKey();
        }

        static int[] getRowColumnFromUser(string input)
        {
            int[] i = new int[2] { -1, -1 };
            if (input.Length < 2) { return i; } 
            i[0] = int.Parse(""+input[0]);
            i[1] = int.Parse(""+input[1]);
            return i;
        }
    }
}
