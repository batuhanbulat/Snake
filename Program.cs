using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace yilan2
{
    class Program
    {

        


        static void Main(string[] args)
        {
            int sayi;
            sayi = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            string[,] board = new string[27, 62];
            duvarYerleştir(board);

            harfAtama(board);
            for (int q = 0; q <= sayi; q++)
             duvarAtama(board);
            
            
            tabloYazma(board);


            int [] snakex = new int[30];
            int [] snakey = new int[30];
            int [] oldsnakex = new int[30];
            int [] oldsnakey = new int[30]; 
            int snakedir = 0;
            string [] yılan ={"%","+"};
            snakex[0] = 10;
            snakey[0] = 10;
            oldsnakex[0] = 9;
            oldsnakey[0] = 9;
            ConsoleKeyInfo cki;
            //ana döngü
            do
            {
                if ((snakedir == 1 && board[snakex[0], snakey[0] - 1] == "#") || (snakedir == 2 && board[snakex[0], snakey[0] + 1] == "#") || (snakedir == 3 && board[snakex[0] - 1, snakey[0]] == "#") || (snakedir == 4 && board[snakex[0] + 1, snakey[0] - 1] == "#"))
                {
                    Console.Clear();
                    Console.WriteLine(@"





                                                                                    Game Over!!!");
                    break;
                }
                
                    Thread.Sleep(80);
                if (Console.KeyAvailable)
                {
                    cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.LeftArrow)
                    {
                        if (snakedir != 2)
                        {
                            snakedir = 1;
                        }
                    }
                    else if (cki.Key == ConsoleKey.RightArrow)
                    {
                        if (snakedir != 1)
                        {
                            snakedir = 2;
                        }
                    }
                    else if (cki.Key == ConsoleKey.UpArrow)
                    {
                        if (snakedir != 0)
                        {
                            snakedir = 3;
                        }
                    }
                    else if (cki.Key == ConsoleKey.DownArrow)
                    {
                        if (snakedir != 3)
                        {
                            snakedir = 0;
                        }
                    }
                }
                if (snakex[0] > 1 && snakedir == 1)
                {
                    snakex[0]--;
                }
                else if (snakex[0] < 58 && snakedir == 2)
                {
                    snakex[0]++;
                }
                else if (snakey[0] < 23 && snakedir == 0)
                {
                    snakey[0]++;
                }
                else if (snakey[0] > 1 && snakedir == 3)
                {
                    snakey[0]--;
                }
                Console.SetCursorPosition(oldsnakex[0], oldsnakey[0]);
                Console.Write(" ");
                oldsnakex[0] = snakex[0];
                oldsnakey[0] = snakey[0];
                Console.SetCursorPosition(snakex[0], snakey[0]);
                Console.Write(yılan[0]);
                Console.SetCursorPosition(oldsnakex[0], oldsnakey[0]);
                Console.Write(yılan[0]);
            } while (true);
            Console.ReadLine();
        }
        static void duvarYerleştir(string[,] board)
        {

            for (int i = 1; i < board.GetLength(0); i++)
            {
                for (int j = 1; j < board.GetLength(1); j++)
                {
                    if (i == 1)
                    {
                        board[i, j] = "#";
                    }
                    else if (j == 1)
                    {
                        board[i, j] = "#";
                    }
                    else if (i == 25)
                    {
                        board[i, j] = "#";
                    }
                    else if (j == 60)
                    {
                        board[i, j] = "#";
                    }
                    else
                        board[i, j] = " ";
                }
            }

        }
        private static void duvarAtama(string[,] board)
        {
            Random rnd = new Random();
            int vertical;
            int x;
            int y;
            vertical = rnd.Next(0, 2);
            if (vertical == 0)
            {
                do
                {
                    x = rnd.Next(2, board.GetLength(0) - 1);
                    y = rnd.Next(2, board.GetLength(1) - 1);
                } while (board[x, y] != " " && board[x, y + 1] != " " && board[x, y + 2] != " " && board[x, y + 3] != " " && board[x, y + 4] != " " && board[x, y + 5] != " " && board[x, y + 6] != " " && board[x, y + 7] != " " && board[x, y + 8] != " " && board[x, y + 9] != " ");
                for (int i = 0; i < 11; i++)
                    board[x, y + i + 1] = "#";
            }
            else
            {
                do
                {
                    x = rnd.Next(2, board.GetLength(0) - 1);
                    y = rnd.Next(2, board.GetLength(1) - 1);
                } while (board[x, y] != " " && board[x + 1, y] != " " && board[x + 2, y + 2] != " " && board[x + 3, y] != " " && board[x + 4, y] != " " && board[x + 5, y] != " " && board[x + 6, y] != " " && board[x + 7, y] != " " && board[x + 8, y] != " " && board[x + 9, y] != " ");
                for (int i = 0; i < 11; i++)
                    board[x + i + 1, y] = "#";
            }
        }

        public static void tabloYazma(string[,] board)
        {
            for (int i = 1; i < board.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < board.GetLength(1) - 1; j++)
                {
                    Console.Write(board[i, j]);
                }
                Console.WriteLine();
            }
        }

        public static void harfAtama(string[,] board)
        {
            string alfabe = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ";
            Random rnd = new Random();
            int x;
            int y;
            for (int o = 0; o < 29; o++)
            {
                do
                {
                    x = rnd.Next(2, board.GetLength(0) - 1);
                    y = rnd.Next(2, board.GetLength(1) - 1);
                } while (board[x, y] != " ");

                board[x, y] = Convert.ToString(alfabe[o]);
            }
        }
    }
}