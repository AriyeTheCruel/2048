using System;
using System.Collections.Generic;

namespace _2048
{
    public class point
    {
        public int x {get; set; }
        public int y { get; set; }
    }
    public class Board
    {
        static int size = 4;
        int[,] board = new int[size, size];
        List<point> zeros_coordinates = new List<point>();
        public bool finished  { get; set; }
        private char direction = 'l';
        public int this[int i, int j]
        {
            get {
                permute(ref i, ref j);    
                return board[i,j]; 
            }
            set
            {
                permute(ref i, ref j);
                board[i, j] = value;
            }
        }

        public void init()
        {
            int[] a = new int[size];

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    board[i, j] = 0;
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            finished = false;
        }
        public void print()
        {
            //i is vertical axix, j ia horizontal axis (i = 0 is first row, j = 0  is first column)
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                    Console.Write("{0} ", board[i, j]);
                Console.WriteLine("\n");        
            }
        }
        private void test_func(ref int a, ref int b)
        {
            int c = a;
            a = b;
            b = c;
        }
        private int count_non_zeros()
        {
            int counter = 0;
            zeros_coordinates.Clear();
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    if (board[i, j] == 0)
                    {
                        counter++;
                        zeros_coordinates.Add(new point { x = i, y = j });
                    }
            return counter;
        }
        public void Update_board()
        {
            Random random = new Random();
            count_non_zeros();
            int randomNumber = random.Next(0, zeros_coordinates.Count);
            if (zeros_coordinates.Count == 0)
                return;
            board[zeros_coordinates[randomNumber].x, zeros_coordinates[randomNumber].y] =  random.Next(1,11) < 10 ? 2 : 4; ;
        }

        private void permute(ref int i, ref int j)
        {
            int temp;
            switch (direction)
            { 
                case 'l':
                    break;
                case 'r':
                    j = size - 1 - j;
                    break;
                case 'u':
                    temp = i;
                    i = j;
                    j = temp;
                    break;
                case 'd':
                    temp = i;
                    i = size - j - 1;
                    j = temp;
                    break;
            }
        }

        public bool Act(char c)
        {
            if (c != 'u' && c!= 'd' && c!='r' && c!= 'l')
            {
                Console.WriteLine("invalid action");

            }

            direction = c;
            //first implement for only left
            for (int i = 0; i < size; i++) //go row row
            {
                for (int j = 0; j < size; j++)
                {
                    if (this[i, j] == 0) // nothing to do
                        continue;
                    //if we are here it's not zero
                    for (int k = j + 1; k < size; k++) // so if we are in the last block we don't enter
                        if (this[i, k] != 0) // look for next block in the row
                        {
                            if (this[i, k] == this[i, j]) //additional block has same value
                            {
                                this[i, j] *= 2;
                                this[i, k] = 0;
                            }
                            break; // we don't need to combine any more blocks
                        }
                    //move block (combined or not) to the furtherst vacant block
                    for (int k = j - 1; k >= 0; k--) // if we are in zero it will never happen anyway :)
                    {
                        if (this[i, k] != 0)
                            break;
                        if (this[i, k] == 0 && (k == 0 || this[i, k - 1] != 0))
                        {
                            this[i, k] = this[i, j];
                            this[i, j] = 0;
                            break;
                        }
                    }

                }
            }
            return true;
        }

        public Board()
        {
            Console.WriteLine("initializing board");
            init();
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            System.ConsoleKeyInfo c;
            Board board = new Board();
            while (!board.finished)
            {
                board.Update_board();
                board.print();
                Console.WriteLine("enter an arrow key or wasd");
                c = Console.ReadKey();
                Console.WriteLine();
                if (c.Key == ConsoleKey.UpArrow || c.Key == ConsoleKey.W)
                {
                    Console.WriteLine("up!");
                    board.Act('u');
                }
                else if (c.Key == ConsoleKey.LeftArrow || c.Key == ConsoleKey.A)
                {
                    Console.WriteLine("left!");
                    board.Act('l');
                }
                else if (c.Key == ConsoleKey.DownArrow || c.Key == ConsoleKey.S)
                {
                    Console.WriteLine("down!");
                    board.Act('d');
                }
                else if (c.Key == ConsoleKey.RightArrow || c.Key == ConsoleKey.D)
                {
                    Console.WriteLine("right!");
                    board.Act('r');
                }
                else if (c.Key == ConsoleKey.Q || c.Key == ConsoleKey.Escape)
                    board.init();
                else
                    Console.WriteLine("invalid key -> nothing happended!");

            }
            //Console.WriteLine("Hello World!");
            Console.ReadLine();
                  
        }
    }
}
