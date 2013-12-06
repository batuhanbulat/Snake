using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace mayın_deneme
{
    class Program
    {

        static void Main(string[] args)
        {
            //Arka plan rengi ve normal yazo rengi için
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;

            string command;
            do
            {
                Console.Clear();
                Console.WriteLine("N :New game");
                Console.WriteLine("L :Load game");
                Console.Write("Choose a Command :");
                command = Console.ReadLine().ToUpper();
                Console.Clear();
            } while (command != "N" & command != "L");
            //oyun bitince çıkmak için bir bool
            bool game = true;

            //Oyundaki verileri kaydetmek için mesela duvar,mayın...
            string[,] gameb = new string[7, 7];
            //arraydeki alınacak string değerleri: M:Mayın , W:Duvar , F:Bayrak-Mayınlı , L:Bayrak-Mayınsız , O:Açılmış , E:Açılmamış

            //Mayınlara göre sayıları kaydetmek için...
            int[,] numbers = new int[7, 7];

            //Basılan tuşu algılamak için bir değişken
            ConsoleKeyInfo cki;

            //gezerken seçilen yerin değişkenleri
            int r = 0, c = 0;

            //
            int point = 0, hint = 3, truef = 0, numberf = 0;

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    gameb[i, j] = "E";
                }
            }


            //gamebyu yazdırma
            Console.WriteLine("  1234567");
            Console.WriteLine(" -=======¬");
            Console.WriteLine("1¦.......¦");
            Console.WriteLine("2¦.......¦");
            Console.WriteLine("3¦.......¦");
            Console.WriteLine("4¦.......¦");
            Console.WriteLine("5¦.......¦");
            Console.WriteLine("6¦.......¦");
            Console.WriteLine("7¦.......¦");
            Console.WriteLine(" L=======-");
            Console.WriteLine();
            Console.WriteLine("Command List");
            Console.WriteLine("============");
            Console.WriteLine("Open   :O");
            Console.WriteLine("Flag   :F");
            Console.WriteLine("Unflag :U");
            Console.WriteLine("Hint   :H");
            Console.WriteLine("Save   :S");
            Console.WriteLine("Exit   :E");
            //rastegele işlemleri yapmak ve sayac...
            int r1, r2;
            Random rnd = new Random();
            int counter = 0;

            if (command == "N")
            {
                //duvar atama işlemi
                do
                {
                    r1 = rnd.Next(0, 7);
                    r2 = rnd.Next(0, 7);
                    if (gameb[r1, r2] != "W")
                    {
                        //atanmış duvarları seçmemek için
                        gameb[r1, r2] = "W";
                        counter++;
                        //r1+2 ve r2+2, console koordinatları için
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.SetCursorPosition(r2 + 2, r1 + 2);
                        Console.Write(Convert.ToString(Convert.ToChar(4)));
                    }
                } while (counter != 10);


                counter = 0;

                //mayınları atamak için
                do
                {
                    r1 = rnd.Next(0, 7);
                    r2 = rnd.Next(0, 7);

                    //mayın veya duvarı seçmemek için
                    if (gameb[r1, r2] != "W" && gameb[r1, r2] != "M")
                    {
                        gameb[r1, r2] = "M";
                        counter++;
                    }
                } while (counter != 8);

                //mayınlara göre sayı atama işlemi
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        //Seçilen yerin mayın olduğunda sayı işlemleri yapılacak...
                        if (gameb[i, j] == "M")
                        {
                            //Seçtiğim bölgenin etrafını taratmak için
                            for (int k = i - 1; k < i + 2; k++)
                            {
                                for (int l = j - 1; l < j + 2; l++)
                                {
                                    //k'nın ve l'nin değerinin -1 veya 7 olmaması için
                                    if (k > -1 && l > -1 && k < 7 && l < 7)
                                    {
                                        //buradaki değeri 1 artırıcam
                                        numbers[k, l]++;
                                    }
                                }
                            }
                        }
                    }
                }

            }
            else
            {
                //load kısmı....
                StreamReader load = File.OpenText("mfs.txt");

                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        gameb[i, j] = Convert.ToString(Convert.ToChar(load.Read()));
                    }
                    load.ReadLine();
                }

                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        numbers[i, j] = Convert.ToInt16(Convert.ToString(Convert.ToChar(load.Read())));
                    }
                    load.ReadLine();
                }

                point = Convert.ToInt16(load.ReadLine());
                hint = Convert.ToInt16(load.ReadLine());
                truef = Convert.ToInt16(load.ReadLine());
                numberf = Convert.ToInt16(load.ReadLine());

                load.Close();
                //dosyadaki değişkenleri yükledikten sonra ekrana bunları yazdırma
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        if (gameb[i, j] == "W")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.SetCursorPosition(j + 2, i + 2);
                            Console.Write(Convert.ToString(Convert.ToChar(4)));
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        else if (gameb[i, j] == "F" | gameb[i, j] == "L")
                        {
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.SetCursorPosition(j + 2, i + 2);
                            Console.Write(Convert.ToString(Convert.ToChar(165)));
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        else if (gameb[i, j] == "O")
                        {
                            Console.SetCursorPosition(j + 2, i + 2);
                            Console.Write(numbers[i, j]);
                        }
                    }
                }
            }
            //-------------------------------------------------
            Console.ForegroundColor = ConsoleColor.Black;
            //oyunun döngüsü
            while (game)
            {
                //seçilen yeri göstermek için cursorı oraya gönderion
                Console.SetCursorPosition(c + 2, r + 2);

                cki = Console.ReadKey(true);

                //Yön tuşlarının haraketi
                if (cki.Key == ConsoleKey.UpArrow)
                {
                    if (r != 0)
                    {
                        r--;
                    }
                }
                else if (cki.Key == ConsoleKey.DownArrow)
                {
                    if (r != 6)
                    {
                        r++;
                    }
                }
                else if (cki.Key == ConsoleKey.LeftArrow)
                {
                    if (c != 0)
                    {
                        c--;
                    }
                }
                else if (cki.Key == ConsoleKey.RightArrow)
                {
                    if (c != 6)
                    {
                        c++;
                    }
                }

                //Eğer açma tuşuna basarsa 
                if (cki.Key == ConsoleKey.O)
                {
                    //Eğer seçilen yerde mayın varsa
                    if (gameb[r, c] == "M")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(c + 2, r + 2);
                        Console.Write(Convert.ToString(Convert.ToChar(164)));
                        Console.SetCursorPosition(0, 20);
                        Console.WriteLine("Mine Exploded ! Game Over . Score :{0}", point + 50 * truef - (numberf - truef) * 30);
                        Console.WriteLine("Press any key to exit...");
                        Console.ReadKey(true);
                        game = false;
                    }
                    //Eğer seçilen yer boşsa oraya say değerini yazdırıyroum ve ordaki sayının 10 katını puana ekliyorum
                    else if (gameb[r, c] == "E")
                    {
                        gameb[r, c] = "O";
                        Console.SetCursorPosition(c + 2, r + 2);
                        Console.Write(numbers[r, c]);
                        point += numbers[r, c] * 10;
                    }
                }
                //Eğer bayraklamayı seçerse
                else if (cki.Key == ConsoleKey.F)
                {
                    //Boş yerse oraya mayınsız bayrak ataımve sadece bayrak sayısını artırırım
                    if (gameb[r, c] == "E")
                    {
                        gameb[r, c] = "L";
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.SetCursorPosition(c + 2, r + 2);
                        Console.Write(Convert.ToString(Convert.ToChar(165)));
                        Console.ForegroundColor = ConsoleColor.Black;
                        numberf++;
                    }
                    //Eğer orada mayın varsa mayınlı bayrak ata , bayrak sayısını ve doğru bayrak sayısını bir artır ve oraya bayrak yazdır
                    else if (gameb[r, c] == "M")
                    {
                        gameb[r, c] = "F";
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.SetCursorPosition(c + 2, r + 2);
                        Console.Write(Convert.ToString(Convert.ToChar(165)));
                        Console.ForegroundColor = ConsoleColor.Black;
                        numberf++;
                        truef++;
                    }
                }
                //Eğer bayrak kaldırmayı seçerse
                else if (cki.Key == ConsoleKey.U)
                {
                    //seçilen yer mayınlı bayraksa orayı mayına çevir doğru bayrak ve bayrak sayısını bi azalt ve ordaki yazıyı eski haline getir
                    if (gameb[r, c] == "F")
                    {
                        gameb[r, c] = "M";
                        Console.SetCursorPosition(c + 2, r + 2);
                        Console.Write(".");
                        Console.ForegroundColor = ConsoleColor.Black;
                        numberf--;
                        truef--;
                    }
                    else if (gameb[r, c] == "L")
                    {
                        gameb[r, c] = "E";
                        Console.SetCursorPosition(c + 2, r + 2);
                        Console.Write(".");
                        Console.ForegroundColor = ConsoleColor.Black;
                        numberf--;
                    }
                }
                else if (cki.Key == ConsoleKey.H)
                {
                    if (hint > 0)
                    {
                        do
                        {
                            r1 = rnd.Next(0, 6);
                            r2 = rnd.Next(0, 6);
                        } while (gameb[r1, r2] != "E");
                        gameb[r1, r2] = "O";
                        point -= 20;
                        hint--;
                        Console.SetCursorPosition(r2 + 2, r1 + 2);
                        Console.Write(numbers[r1, r2]);
                    }
                }
                else if (cki.Key == ConsoleKey.S)
                {
                    StreamWriter save = File.CreateText("mfs.txt");

                    for (int i = 0; i < 7; i++)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            save.Write(gameb[i, j]);
                        }
                        save.WriteLine();
                    }

                    for (int i = 0; i < 7; i++)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            save.Write(numbers[i, j]);
                        }
                        save.WriteLine();
                    }

                    save.WriteLine(point);
                    save.WriteLine(hint);
                    save.WriteLine(truef);
                    save.WriteLine(numberf);

                    save.Close();

                    Console.SetCursorPosition(0, 20);
                    Console.WriteLine("Game saved. Press any key to exit...");
                    Console.ReadKey(true);
                    game = false;
                }
                else if (cki.Key == ConsoleKey.E)
                {
                    Console.SetCursorPosition(0, 20);
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey(true);
                    game = false;
                }

                //Puan ve hintin olduğu yeri temizleme
                Console.SetCursorPosition(15, 3);
                Console.Write("                ");
                Console.SetCursorPosition(15, 5);
                Console.Write("                ", hint);
                //Puan ve hintin yazımı
                Console.SetCursorPosition(15, 3);
                Console.Write("Point :{0}", point);
                Console.SetCursorPosition(15, 5);
                Console.Write("Hint left :{0}", hint);

                //Oyunun bitme olayı-------------------------------------------
                //Eğer 8 mayın da bayrakmışsa
                if (truef == 8)
                {
                    //her bayrağın altında mayın varsa başka bişi yoksa
                    if (numberf - truef == 0)
                    {
                        //heryer girilmiş mi diye kontrol ederim eğer girilmiş bi yer varsa oyun eğer yoksa oyun bitmiştir score yazdırım ve bitiririm
                        game = false;
                        for (int i = 0; i < 7; i++)
                        {
                            for (int j = 0; j < 7; j++)
                            {
                                if (gameb[i, j] == "E")
                                {
                                    game = true;
                                }
                            }
                        }
                        //Eğer döngünün sonuna kadar bir karakter bile gamei true yapamamışsa her yer dolmuş demektir score yazarım.
                        if (!game)
                        {
                            Console.SetCursorPosition(0, 20);
                            Console.WriteLine("Congrats . Score :{0}", point + 400);
                            Console.WriteLine("Press any key to exit...");
                            Console.ReadKey(true);
                        }
                    }
                }


            }

        }
    }
}
