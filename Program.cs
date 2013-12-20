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
            Console.SetWindowSize(100,27);            //...................................... oyunu sığması için console u büyütme
            int sayi=0;
            string deyim1="";

            int sayi1 = 0;

            while (sayi1 < 1 || sayi1 > 4)           //......................................menu
            {
                writeLetterHunt();     //..................................................ekrana Letter HUNT yazma
                string[] deyim = new string[25];
                sayi1 = Convert.ToInt32(Console.ReadLine());           //......................................kullanıcıdan level alma
                Console.Clear();
                StreamReader f = File.OpenText("c:\\statements.txt");           //......................................text yolunu belirleme
                for (int st = 0; st <= 24; st++)
                {
                    deyim[st] = Convert.ToString(f.ReadLine());           //......................................textden deyimleri alma

                }
                Random rnd = new Random();
                if (sayi1 == 1)           //......................................levele göre rasgele deyim alma
                {
                    sayi = 2;
                    deyim1 = deyim[rnd.Next(1, 5)];
                }
                else if (sayi1 == 2)           //......................................levele göre rasgele deyim alma
                {
                    sayi = 4;
                    deyim1 = deyim[rnd.Next(5, 10)];
                }
                else if (sayi1 == 3)           //......................................levele göre rasgele deyim alma
                {
                    sayi = 6;
                    deyim1 = deyim[rnd.Next(10, 15)];
                }
                else           //......................................levele göre rasgele deyim alma
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
                      //......................................ana döngü
            do
            {
                if (snakedir == 0 && board[snakey[0] + 1, snakex[0]] == "#")           //......................................duvara çarpınca oyunu bitirme
                {
                    endGame();
                    break;
                }
                else if (snakedir == 1 && board[snakey[0], snakex[0] - 1] == "#")           //......................................duvara çarpınca oyunu bitirm
                {
                    endGame();
                    break;
                }
                else if (snakedir == 2 && board[snakey[0], snakex[0] + 1] == "#")           //......................................duvara çarpınca oyunu bitirm
                {
                    endGame();
                    break;
                }

                else if (snakedir == 3 && board[snakey[0] + 1, snakex[0]] == "#")           //......................................duvara çarpınca oyunu bitirm
                {
                    
                    endGame();
                    break;
                }


                Thread.Sleep(80);            //......................................yılan hareketinin görünmesi için döngüyü bekletme
                if (Console.KeyAvailable)
                {           //......................................tuş okutma
                    cki = Console.ReadKey(true);
                    if (cki.Key == ConsoleKey.LeftArrow)
                    {
                        if (snakedir != 2)           //......................................yılanın kendi üstünden geçmesini engelleme
                        {
                            snakedir = 1;           //...................................... yılanın hareket sürekliliğini sağlama
                        }
                    }
                    else if (cki.Key == ConsoleKey.RightArrow)
                    {
                        if (snakedir != 1)           //......................................yılanın kendi üstünden geçmesini engelleme
                        {
                            snakedir = 2;           //...................................... yılanın hareket sürekliliğini sağlama
                        }
                    }
                    else if (cki.Key == ConsoleKey.UpArrow)
                    {
                        if (snakedir != 0)           //......................................yılanın kendi üstünden geçmesini engelleme
                        {
                            snakedir = 3;           //...................................... yılanın hareket sürekliliğini sağlama
                        }
                    }
                    else if (cki.Key == ConsoleKey.DownArrow)
                    {
                        if (snakedir != 3)           //......................................yılanın kendi üstünden geçmesini engelleme
                        {
                            snakedir = 0;           //...................................... yılanın hareket sürekliliğini sağlama
                        }
                    }
                }
                if (snakex[0] > 1 && snakedir == 1)
                {
                    snakex[0]--;           //...................................... yılanı ilerletme
                }
                else if (snakex[0] < 58 && snakedir == 2)
                {
                    snakex[0]++;           //...................................... yılanı ilerletme
                }
                else if (snakey[0] < 23 && snakedir == 0)
                {
                    snakey[0]++;           //...................................... yılanı ilerletme
                }
                else if (snakey[0] > 1 && snakedir == 3)
                {
                    snakey[0]--;           //...................................... yılanı ilerletme
                }
                Console.SetCursorPosition(oldsnakex[0], oldsnakey[0]);            //......................................yılanın eski yerini silme
                Console.Write(" ");
                Console.SetCursorPosition(snakex[0], snakey[0]);           //...................................... yılanı yeni yerine götürme
                Console.Write(yılan[0]);
                oldsnakex[0] = snakex[0]; oldsnakey[0] = snakey[0];
                if (snakedir == 2 && snakex[0]==58)           //......................................oyun bitirme sağ duvarda
                {
                    Thread.Sleep(10);
                    endGame(); 
                    break;
                }
                else if (snakedir == 0 && snakey[0]==23)           //...................................... alt duvarda oyun bitirme
                {
                    Thread.Sleep(10);
                    endGame();
                    break;
                }
                else if (snakedir == 3 && snakey[0] == 1)           //......................................üst duvarda oyun bitirme
                {
                    Thread.Sleep(100);
                    endGame();
                    break;
                }
            } while (true);
            Console.ReadLine();
        }

        private static void writeLetterHunt()
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
        }

       
           
         

        private static void endGame()           //......................................oyunu bitirme fonksiyonu
        {

            Console.Clear();           //...................................... ekranı temizleme
            Console.WriteLine(@"






                            Game Over!!!");
        }
        static void duvarYerleştir(string[,] board)           //......................................çerçeveyi atama
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
        private static void duvarAtama(string[,] board, int sayi)           //......................................random duvar atama
        {
            for (int zz = 1; zz <= sayi; zz++)
            {


                Random rnd = new Random();
                int vertical;
                int x;
                int y;
                vertical = rnd.Next(0, 2);                                  //......................................yatay yada düşey için random
                if (vertical == 0)
                {
                    do
                    {                    //............................................................................pozisyon belirleme
                        x = rnd.Next(3, board.GetLength(0) - 3);
                        y = rnd.Next(3, board.GetLength(1) - 13);
                    } while (board[x, y] != " " && board[x, y + 1] != " " && board[x, y + 2] != " " && board[x, y + 3] != " " && board[x, y + 4] != " " && board[x, y + 5] != " " && board[x, y + 6] != " " && board[x, y + 7] != " " && board[x, y + 8] != " " && board[x, y + 9] != " ");
                    for (int i = 0; i <= 9; i++)                            //......................................duvarı atama
                        board[x, y + i] = "#"; 
                }
                else
                {
                    do
                    {                     //............................................................................pozisyon belirleme
                        x = rnd.Next(3, board.GetLength(0) - 13);
                        y = rnd.Next(3, board.GetLength(1) - 3);
                    } while (board[x, y] != " " && board[x + 1, y] != " " && board[x + 2, y + 2] != " " && board[x + 3, y] != " " && board[x + 4, y] != " " && board[x + 5, y] != " " && board[x + 6, y] != " " && board[x + 7, y] != " " && board[x + 8, y] != " " && board[x + 9, y] != " ");
                    for (int i = 0; i <= 9; i++)                             //......................................duvarı atama
                        board[x + i, y] = "#";
                }
            }
        }

        public static void tabloYazma(string[,] board,string deyim1)           //......................................boardu ekrana yazma
        {
            for (int i = 1; i < board.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < board.GetLength(1) - 1; j++)
                {
                    Console.Write(board[i, j]);
                }
                Console.WriteLine();
            }
            Console.SetCursorPosition(67, 15);                                 //......................................deyimi yazma
            Console.Write(deyim1);
        }

        public static void harfAtama(string[,] board)                          //......................................alfabayi atama
        {
            string alfabe = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ";
            Random rnd = new Random();
            int x;
            int y;
            for (int o = 0; o < 29; o++)
            {
                do
                {                           //............................................................................ harflere rasgele yer verme
                    x = rnd.Next(2, board.GetLength(0) - 1); 
                    y = rnd.Next(2, board.GetLength(1) - 1);
                } while (board[x, y] != " ");

                board[x, y] = Convert.ToString(alfabe[o]);
            }
        }
    
    }
}