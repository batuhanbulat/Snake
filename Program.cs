using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace yilan3
{
    class Program
    {



        static void Main(string[] args)
        {
            Console.SetWindowSize(100,27);
            int sayi=0;
            string deyim1="";

            int sayi1 = 0;

            while (sayi1 < 1 || sayi1 > 4)
            {
                Console.WriteLine(@"
 

                                                                       #               
 ###                                                                   ##              
 ###    ######## ######## ######## ######## #######    ###  ## ###  ## ###  ## ########
 ###                ###      ###                  ##   ###  ## ###  ## #### ##    ###  
 ###     #######    ###      ###    #######  ######    ####### ###  ## #######    ###  
 ###     ###        ###      ###    ###      ##  ##    ###  ## ###  ## ### ###    ###  
 ####### #######    ###      ###    #######  ##   ##   ###  ##  #####  ###  ##    ###  
                                                       ###                   #         

Select your level (1-4)   :");


                string[] deyim = new string[25];
                sayi1 = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                StreamReader f = File.OpenText("c:\\statements.txt");
                for (int st = 0; st <= 24; st++)
                {
                    deyim[st] = Convert.ToString(f.ReadLine());

                }
                Random rnd = new Random();
                if (sayi1 == 1)
                {
                    sayi = 2;
                    deyim1 = deyim[rnd.Next(1, 5)];
                }
                else if (sayi1 == 2)
                {
                    sayi = 4;
                    deyim1 = deyim[rnd.Next(5, 10)];
                }
                else if (sayi1 == 3)
                {
                    sayi = 6;
                    deyim1 = deyim[rnd.Next(10, 15)];
                }
                else
                {
                    sayi = 8;
                    deyim1 = deyim[rnd.Next(1, 5)];
                }
                deyim1 = deyim1.Remove(0, 2);
            }
            string[,] board = new string[27, 62];
            duvarYerleştir(board);

            harfAtama(board);

            duvarAtama(board, sayi);

            tabloYazma(board,deyim1);


            int[] snakex = new int[30];
            int[] snakey = new int[30];
            int[] oldsnakex = new int[30];
            int[] oldsnakey = new int[30];
            int snakedir = 0;
            string[] yılan = { "%", "+" };
            snakex[0] = 10;
            snakey[0] = 10;
            oldsnakex[0] = 9;
            oldsnakey[0] = 9;
            ConsoleKeyInfo cki;
            //ana döngü
            do
            {
                if (snakedir == 0 && board[snakey[0] + 1, snakex[0]] == "#")
                {
                    endGame();
                    break;
                }
                else if (snakedir == 1 && board[snakey[0], snakex[0] - 1] == "#")
                {
                    endGame();
                    break;
                }
                else if (snakedir == 2 && board[snakey[0], snakex[0] + 1] == "#")
                {
                    endGame();
                    break;
                }
                
                else if (snakedir == 3 && board[snakey[0] + 1, snakex[0]] == "#")
                {
                    
                    endGame();
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
                Console.SetCursorPosition(snakex[0], snakey[0]);
                Console.Write(yılan[0]);
                oldsnakex[0] = snakex[0]; oldsnakey[0] = snakey[0];
                if (snakedir == 2 && snakex[0]==58)
                {
                    Thread.Sleep(10);
                    endGame(); 
                    break;
                }
                else if (snakedir == 0 && snakey[0]==23)
                {
                    Thread.Sleep(10);
                    endGame();
                    break;
                }
                else if (snakedir == 3 && snakey[0] == 1)
                {
                    Thread.Sleep(100);
                    endGame();
                    break;
                }
            } while (true);
            Console.ReadLine();
        }

       
           
         

        private static void endGame()
        {

            Console.Clear();
            Console.WriteLine(@"






                            Game Over!!!");
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
        private static void duvarAtama(string[,] board, int sayi)
        {
            for (int zz = 1; zz <= sayi; zz++)
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
                        x = rnd.Next(3, board.GetLength(0) - 3);
                        y = rnd.Next(3, board.GetLength(1) - 13);
                    } while (board[x, y] != " " && board[x, y + 1] != " " && board[x, y + 2] != " " && board[x, y + 3] != " " && board[x, y + 4] != " " && board[x, y + 5] != " " && board[x, y + 6] != " " && board[x, y + 7] != " " && board[x, y + 8] != " " && board[x, y + 9] != " ");
                    for (int i = 0; i <= 9; i++)
                        board[x, y + i] = "#";
                }
                else
                {
                    do
                    {
                        x = rnd.Next(3, board.GetLength(0) - 13);
                        y = rnd.Next(3, board.GetLength(1) - 3);
                    } while (board[x, y] != " " && board[x + 1, y] != " " && board[x + 2, y + 2] != " " && board[x + 3, y] != " " && board[x + 4, y] != " " && board[x + 5, y] != " " && board[x + 6, y] != " " && board[x + 7, y] != " " && board[x + 8, y] != " " && board[x + 9, y] != " ");
                    for (int i = 0; i <= 9; i++)
                        board[x + i, y] = "#";
                }
            }
        }

        public static void tabloYazma(string[,] board,string deyim1)
        {
            for (int i = 1; i < board.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < board.GetLength(1) - 1; j++)
                {
                    Console.Write(board[i, j]);
                }
                Console.WriteLine();
            }
            Console.SetCursorPosition(67, 15);
            Console.Write(deyim1);
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