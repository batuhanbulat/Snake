using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApplication25
{
    class Program
    {
        static void Main(string[] args)
        {
            string[,] board = new string[27, 62];
            int snakex = 1, snakey = 1, osnakex = 0, osnakey = 0,snakedir=0;
            ConsoleKeyInfo cki;
            do
            { Thread.Sleep(80);
                
                if (Console.KeyAvailable)
                {
                    cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.LeftArrow)
                        snakedir = 1;
                    if ( cki.Key == ConsoleKey.RightArrow)
                        snakedir = 2;
                    if (cki.Key == ConsoleKey.UpArrow)
                        snakedir = 3;
                    if (cki.Key == ConsoleKey.DownArrow)
                        snakedir = 4;
                   
                }
                if (snakex > 0 && snakedir == 1)
                    snakex--;
                if (snakex < 60 && snakedir == 2)
                    snakex++;
                if (snakey > 0 && snakedir == 3)
                    snakey--;
                if (snakey < 25 && snakedir == 4)
                    snakey++;
                Console.SetCursorPosition(osnakex, osnakey);
                Console.Write(" ");
                Console.SetCursorPosition(snakex, snakey);
                Console.Write("%");
                osnakex = snakex; osnakey = snakey;
                
            } while (true);

        }
    }
}
