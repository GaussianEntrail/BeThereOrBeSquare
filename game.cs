using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvoidMakingSquares
{
    class game
    {
        public int PlayerScore;
        public int ComputerScore;
        public int size; //size of a square board
        int[,] board; //0 for an empty space, 1 for a human piece, 2 for a computer piece
        string status;
        Random rng;
        public game()
        {
            PlayerScore = 0;
            ComputerScore = 0;
            size = 7;
            board = new int[size, size];
            rng = new Random();
            status = "";
            clear();

        }
        public void clear()
        {
            int i, j;
            for (j = 0; j < size; j++)
            {
                for (i = 0; i < size; i++)
                {
                    board[j, i] = 0;
                }
            }
        }
        public int get(int r, int c)
        {
            int k = 0;
            if (r >= 0 && r < size && c >= 0 && c < size ) { k = board[r, c]; }
            return k;
        }
        public bool empty(int r, int c)
        {
            if (r >= 0 && r < size && c >= 0 && c < size) { return (board[r, c] == 0); }
            return false;
        }
        public bool full()
        {
            //returns if the board is full
            //returns false if it finds an empty space
            int r, c;
            for (r = 0; r < size; r++)
            {
                for (c = 0; c < size; c++)
                {
                    if (empty(r,c)) { return false; }
                }
            }

            return true;
        }
        public void set(int r, int c, int p) { if (r >= 0 && r < size && c >= 0 && c < size) { board[r, c] = p; }}
        public bool check(int r, int c, int p) { return (get(r, c) == p); }
        public bool square(int r, int c, int p)
        {
            /* 
             *                       | (R,C-2S)|
             *                       
             *           | (R-S,C-S) | (R,C-S) | (R+S,C-S) |
             * 
             *  (R-2S,C) | (R-S,C)   | (R,C)   | (R+S, C)  | (R+2S,C)
             * 
             *           | (R-S,C+S) | (R,C+S) | (R+S,C+S) |
             *                       
             *                       | (R,C+2S)|
             */
            //set check to 1 or 2 if checking for human or computer piece respectively
            //the starting (r,c) isn't checked so this can be used to make the computer pick a space intelligently
            int s;
            //note to self... remove these bools, and instead return immediately the moment a square is detected
            for (s = 1; s < size; s++)
            {
                //check straight squares
                if (check(r, c - s, p) && check(r - s, c - s, p) && check(r - s, c, p)) { return true; } //NW
                if (check(r, c - s, p) && check(r + s, c - s, p) && check(r + s, c, p)) { return true; } //NE
                if (check(r - s, c, p) && check(r - s, c + s, p) && check(r, c + s, p)) { return true; } //SW
                if (check(r, c + s, p) && check(r + s, c + s, p) && check(r + s, c, p)) { return true; } //SE
                //check diagonal squares
                if (check(r - s, c - s, p) && check(r + s, c - s, p) && check(r, c - (2 * s), p)) { return true; } //N
                if (check(r - s, c - s, p) && check(r - (2 * s), c - s, p) && check(r - s, c + s, p)) { return true; } //S
                if (check(r - s, c + s, p) && check(r, c + (2 * s), p) && check(r + s, c + s, p)) { return true; } //E
                if (check(r + s, c + s, p) && check(r + (2 * s), c, p) && check(r + s, c - s, p)) { return true; } //W
            }
            return false;
        }
        public void draw()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("╔═╦═══════╗\n");
            Console.Write("║x║0123456║\n");
            Console.Write("╠═╬═══════╣\n");
            int r, c, p;
            for (r = 0; r < size; r++)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("║{0}║",r);
                for (c = 0; c < size; c++)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    if ((r + c) % 2 == 0) { Console.BackgroundColor = ConsoleColor.Gray; }

                    //if (square(r, c, 1) || square(r, c, 2)) { Console.BackgroundColor = ConsoleColor.Yellow; } //cheating lol

                    p = get(r, c);
                    switch (p)
                    {
                        default:
                        case 0:
                            Console.Write(" ");
                            break;
                        case 1:
                            //player
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("●");
                            break;
                        case 2:
                            //computer
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write("●");
                            break;
                    }
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("║\n");
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("╚═╩═══════╝\n");
            Console.WriteLine("PLAYER: {0} COMPUTER: {1}",PlayerScore,ComputerScore);
            Console.WriteLine(status);
        }
        public int[] ComputerMove()
        {
            if (full()) {
                status = "The computer can't make a move";
                return new int[] { -1, -1 };
            }
            //plays semi perfectly for now
            int r, c;
            while (true)
            {
                //pick a row and column
                r = rng.Next(size); c = rng.Next(size);
                //check if it's empty and if it won't form a square
                if (empty(r, c))
                {
                    if (!square(r, c, 2))
                    {
                        if (rng.Next(100) > 30)
                        {
                            status = String.Format("The computer places a piece at row {0},  column {1}", r, c);
                            set(r, c, 2);
                            break;
                        }
                    } else {
                        if (rng.Next(100) > 70)
                        {
                            status = String.Format("The computer places a piece at row {0},  column {1}", r, c);
                            set(r, c, 2);
                            break;
                        }
                    }
                }
            }
            return new int[] { r, c };
        }
        public void PlayerMove(int r, int c)
        {
            //a move made by the player
            if (full())
            {
                status = String.Format("You can't make any moves.");
                return;
            }
            if (empty(r, c))
            {
                status = String.Format("You place a piece at row {0},  column {1}", r, c);
                set(r, c, 1);
            }
        }
    }
}
