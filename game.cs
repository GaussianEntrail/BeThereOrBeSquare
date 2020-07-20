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
            //set check to 1 or -1 if checking for human or computer piece
            //the starting (r,c) isn't checked so this can be used to make the computer pick a space intelligently
            bool square1 = false, square2 = false;
            int s;
            for (s = 1; s < size; s++)
            {
                square1 = check(r + s, c, p) && check(r, c + s, p) && check(r + s, c + s, p);
                square2 = check(r - s, c, p) && check(r, c - s, p) && check(r - s, c - s, p);
                if (square1 || square2) { return true; }
            }
            return false;
        }
        public void draw()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("x║0123456║\n");
            Console.Write("═╬═══════╣\n");
            int r, c, p;
            for (r = 0; r < size; r++)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(r+"║");
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
            Console.Write("═╩═══════╝\n");
            Console.WriteLine("PLAYER: {0} COMPUTER: {1}",PlayerScore,ComputerScore);
            Console.WriteLine(status);
        }
        public int[] ComputerMove()
        {
            int[] i = new int[2] { -1, -1 };
            if (full()) {
                status = "The computer can't make a move";
                return i;
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
                    if (!square(r, c, 2) && rng.Next(100) > 10)
                    {
                        status = String.Format("The computer places a piece at row {0},  column {1}", r, c);
                        set(r, c, 2);
                        break;
                    }
                    if (square(r, c, 2) && rng.Next(100) > 90)
                    {
                        status = String.Format("The computer places a piece at row {0},  column {1}", r, c);
                        set(r, c, 2);
                        break;
                    }
                }
            }
            i[0] = r;
            i[1] = c;
            return i;
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
