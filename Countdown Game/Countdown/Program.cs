using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Encodings.Web;
using System.Threading.Tasks.Sources;
using System.Xml.Serialization;

namespace Countdown
{
    internal class Program
    {
        public static void NumberPlacement(string[,] screen, int number)
        {
            Random random = new Random();
            int x, y;
            for (int i = 0; i < 7; i++)
            {
                do
                {
                    x = random.Next(1, 52);
                    y = random.Next(1, 22);
                }
                while (screen[x, y] != " ");
                screen[x, y] = number.ToString();
                Console.SetCursorPosition(x, y);
                Console.WriteLine(screen[x, y]);
            }
        }
        static void WallPlacement(string[,] screen, int length, int duvarsayisi)
        {
            Random random = new Random();
            int wallx;
            int wally;
            bool squareCheck;
            for (int i = 0; i < duvarsayisi; i++)
            {
                int duvarsekli = random.Next(1, 3);//1 gelirse yatay 2 gelirse dikey yerleştirir
                if (duvarsekli == 1)//Yatay şekilde duvarı yerleştirir.
                {
                    do//Duvarın konumu random olarak alınır.
                    {
                        squareCheck = true;
                        wallx = random.Next(2, 51 - length);//Alanın dışına taşmaması için ve 1 boşluk olması için en son 51 - duvarın uzunluğu olmalıdır.
                        wally = random.Next(2, 21);
                        for (int j = 0; j < length; j++)//Duvar yerleştirilecek her bir koordinatın çevresi kontrol edilir
                        {
                            for (int a = -1; a <= 1; a++)
                            {
                                for (int b = -1; b <= 1; b++)
                                {
                                    if (screen[wallx + a + j, wally + b] != " ")
                                        squareCheck = false;
                                }
                            }
                        }
                    }
                    while (squareCheck == false);
                    for (int j = 0; j < length; j++)
                    {
                        Console.SetCursorPosition(wallx + j, wally);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("#");
                        Console.ResetColor();
                        screen[wallx + j, wally] = "#";
                    }
                }
                else if (duvarsekli == 2)//Dikey şekilde duvarı yerleştirir.
                {
                    do
                    {
                        squareCheck = true;
                        wallx = random.Next(2, 51);
                        wally = random.Next(2, 21 - length);//Alanın dışına taşmaması için en son 21 - duvarın uzunluğu olmalıdır.
                        for (int j = 0; j < length; j++)//Duvar yerleştirilecek her bir koordinatın çevresi kontrol edilir
                        {
                            for (int a = -1; a <= 1; a++)
                            {
                                for (int b = -1; b <= 1; b++)
                                {
                                    if (screen[wallx + a, wally + b + j] != " ")
                                        squareCheck = false;
                                }
                            }
                        }
                    }
                    while (squareCheck == false);
                    for (int j = 0; j < length; j++)
                    {
                        Console.SetCursorPosition(wallx, wally + j);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("#");
                        Console.ResetColor();
                        screen[wallx, wally + j] = "#";
                    }
                }
            }
        }
        public static void ZeroMovement(string[,] screen, ref int i, ref int j, ref int lifeCount, ref int cursorx, ref int cursory)
        {
            Random random = new Random();
            int type = random.Next(1, 5);
            if (type == 1 && (screen[i + 1, j] == " " || screen[i + 1, j] == "P"))
            {
                screen[i, j] = " ";
                Console.SetCursorPosition(i, j);
                Console.WriteLine(screen[i, j]);
                screen[i + 1, j] = "0";
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.SetCursorPosition(i + 1, j);
                Console.WriteLine(screen[i + 1, j]);
                i++;
            }
            else if (type == 2 && (screen[i, j + 1] == " " || screen[i, j + 1] == "P"))
            {
                screen[i, j] = " ";
                Console.SetCursorPosition(i, j);
                Console.WriteLine(screen[i, j]);
                screen[i, j + 1] = "0";
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.SetCursorPosition(i, j + 1);
                Console.WriteLine(screen[i, j + 1]);
                j++;
            }
            else if (type == 3 && (screen[i - 1, j] == " " || screen[i - 1, j] == "P"))
            {
                screen[i, j] = " ";
                Console.SetCursorPosition(i, j);
                Console.WriteLine(screen[i, j]);
                screen[i - 1, j] = "0";
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.SetCursorPosition(i - 1, j);
                Console.WriteLine(screen[i - 1, j]);
                i--;
            }
            else if (type == 4 && (screen[i, j - 1] == " " || screen[i, j - 1] == "P"))
            {
                screen[i, j] = " ";
                Console.SetCursorPosition(i, j);
                Console.WriteLine(screen[i, j]);
                screen[i, j - 1] = "0";
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.SetCursorPosition(i, j - 1);
                Console.WriteLine(screen[i, j - 1]);
                j--;
            }
            else
                ZeroMovement(screen, ref i, ref j, ref lifeCount, ref cursorx, ref cursory);
            /*
            else if (type == 5 && (screen[i + 1, j + 1] == " " || screen[i + 1, j + 1] == "P"))
            {
                screen[i, j] = " ";
                Console.SetCursorPosition(i, j);
                Console.WriteLine(screen[i, j]);
                screen[i + 1, j + 1] = "0";
                Console.SetCursorPosition(i + 1, j + 1);
                Console.WriteLine(screen[i + 1, j + 1]);
            }
            else if (type == 6 && (screen[i - 1, j - 1] == " " || screen[i - 1, j - 1] == "P"))
            {
                screen[i, j] = " ";
                Console.SetCursorPosition(i, j);
                Console.WriteLine(screen[i, j]);
                screen[i - 1, j - 1] = "0";
                Console.SetCursorPosition(i - 1, j - 1);
                Console.WriteLine(screen[i - 1, j - 1]);
            }
            else if (type == 7 && (screen[i - 1, j + 1] == " " || screen[i - 1, j + 1] == "P"))
            {
                screen[i, j] = " ";
                Console.SetCursorPosition(i, j);
                Console.WriteLine(screen[i, j]);
                screen[i - 1, j + 1] = "0";
                Console.SetCursorPosition(i - 1, j + 1);
                Console.WriteLine(screen[i - 1, j + 1]);
            }
            else if (type == 8 && (screen[i + 1, j - 1] == " " || screen[i + 1, j - 1] == "P"))
            {
                screen[i, j] = " ";
                Console.SetCursorPosition(i, j);
                Console.WriteLine(screen[i, j]);
                screen[i + 1, j - 1] = "0";
                Console.SetCursorPosition(i + 1, j - 1);
                Console.WriteLine(screen[i + 1, j - 1]);
            }
            */
            Console.ResetColor();
            if (i == cursorx && j == cursory)
            {
                lifeCount--;
                do
                {
                    cursorx = random.Next(1, 52);
                    cursory = random.Next(1, 22);
                }
                while (screen[cursorx, cursory] != " ");
            }
        }
        static void Main(string[] args)
        {
            int cursorx = 0, cursory = 0;   // position of cursor
            ConsoleKeyInfo cki;               // required for readkey
            string[,] screen = new string[53, 23];


            //int[,] sifirKoordinatlari = new int[7, 2];
            int lifeCount = 5;
            int gameScore = 0;
            Random random = new Random();
            Console.CursorVisible = false;
            for (int i = 0; i < 53; i++)
            {
                for (int j = 0; j < 23; j++)
                {
                    Console.SetCursorPosition(i, j);
                    if (i == 0 || j == 0 || i == 52 || j == 22)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("#");
                        screen[i, j] = "#";
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(" ");
                        screen[i, j] = " ";
                    }
                }
            }


            WallPlacement(screen, 11, 3);
            WallPlacement(screen, 7, 5);
            WallPlacement(screen, 3, 20);

            do
            {
                cursorx = random.Next(1, 52);
                cursory = random.Next(1, 22);
            }
            while (screen[cursorx, cursory] != " ");//Player'ın konumunu belirleme

            for (int i = 0; i <= 9; i++)//Sayıları yerleştirme
                NumberPlacement(screen, i);

            DateTime initialTime = DateTime.Now;
            int time = 0;
            Console.SetCursorPosition(56, 2);
            Console.WriteLine("Time   :" + time);
            Console.SetCursorPosition(56, 4);
            Console.WriteLine("Life   : " + lifeCount);
            Console.SetCursorPosition(56, 6);
            Console.WriteLine("Score  : " + gameScore);
            // --- Main game loop
            while (lifeCount != 0)
            {

                if (Console.KeyAvailable && (DateTime.Now.Millisecond - initialTime.Millisecond + 1000 * (DateTime.Now.Second - initialTime.Second)) % 50 == 0)
                {       // true: there is a key in keyboard buffer
                    cki = Console.ReadKey(true);       // true: do not write character 

                    if (cki.Key == ConsoleKey.RightArrow && cursorx < 51)
                    {   // key and boundary control
                        if (screen[cursorx + 1, cursory] == " ")
                        {
                            Console.SetCursorPosition(cursorx, cursory);           // delete X (old position)
                            Console.WriteLine(" ");
                            cursorx++;
                        }
                        else if (screen[cursorx + 1, cursory] == "0")
                        {
                            Console.SetCursorPosition(cursorx, cursory);           // delete X (old position)
                            Console.WriteLine(" ");
                            lifeCount--;
                            do
                            {
                                cursorx = random.Next(1, 52);
                                cursory = random.Next(1, 22);
                            }
                            while (screen[cursorx, cursory] != " ");
                        }
                        //else if (screen[cursorx + 1, cursory] != "#" && screen[cursorx + 2, cursory] == " ")//Tek bir rakamı itme
                        //{
                        //    if (int.TryParse(screen[cursorx + 1, cursory], out int temp1))
                        //    {
                        //        screen[cursorx + 1, cursory] = " ";
                        //        screen[cursorx + 2, cursory] = temp1.ToString();
                        //        Console.SetCursorPosition(cursorx + 1, cursory);
                        //        Console.WriteLine(screen[cursorx + 1, cursory]);
                        //        Console.SetCursorPosition(cursorx + 2, cursory);
                        //        Console.WriteLine(screen[cursorx + 2, cursory]);

                        //    }
                        //    Console.SetCursorPosition(cursorx, cursory);
                        //    Console.WriteLine(" ");
                        //    cursorx++;
                        //}

                        else//Birden fazla rakam için Pushing ve Smashing işlemi
                        {
                            int numberCount = 0;
                            while (screen[cursorx + 1 + numberCount, cursory] != " " && screen[cursorx + 1 + numberCount, cursory] != "#")
                                numberCount++;//Player'ın önünde kaç sayı olduğu belirlenir.
                            bool nonIncreasingCheck = true;
                            for (int i = 1; i < numberCount; i++)
                            {
                                if (Convert.ToInt32(screen[cursorx + i, cursory]) < Convert.ToInt32(screen[cursorx + 1 + i, cursory]))//Rakamların artıp artmadığı kontrol edilir.
                                    nonIncreasingCheck = false;
                            }

                            if (nonIncreasingCheck == true && numberCount > 1 && screen[cursorx + numberCount + 1, cursory] == "#")//Smash
                            {
                                if (screen[cursorx + numberCount, cursory] == "0")//Smash edilen rakama göre Player'a puan verir.
                                    gameScore += 20;
                                else if (screen[cursorx + numberCount, cursory] == "1" || screen[cursorx + numberCount, cursory] == "2" || screen[cursorx + numberCount, cursory] == "3" || screen[cursorx + numberCount, cursory] == "4")
                                    gameScore += 2;
                                else if (screen[cursorx + numberCount, cursory] == "5" || screen[cursorx + numberCount, cursory] == "6" || screen[cursorx + numberCount, cursory] == "7" || screen[cursorx + numberCount, cursory] == "8" || screen[cursorx + numberCount, cursory] == "9")


                                    screen[cursorx + numberCount, cursory] = " ";
                                int newNumber, x, y;
                                do
                                {
                                    x = random.Next(1, 52);
                                    y = random.Next(1, 22);
                                    newNumber = random.Next(5, 10);
                                }
                                while (screen[x, y] != " ");//Smash edilen rakam için yeni koordinatlar belirlenir ve 5'ten 9'a kadar yeni rakam seçilir.
                                screen[x, y] = newNumber.ToString();
                                Console.SetCursorPosition(x, y);
                                Console.WriteLine(screen[x, y]);

                                for (int i = numberCount - 1; i >= 1; i--)//Rakamları kaydırma işlemi. Smash edilecek rakamın öncekinden kaydırmaya başlayarak Player'a kadar devam eder.
                                {
                                    string temp2 = screen[cursorx + i, cursory];
                                    screen[cursorx + i, cursory] = " ";
                                    screen[cursorx + i + 1, cursory] = temp2;
                                    Console.SetCursorPosition(cursorx + i, cursory);
                                    Console.WriteLine(screen[cursorx + i, cursory]);
                                    Console.SetCursorPosition(cursorx + i + 1, cursory);
                                    Console.WriteLine(screen[cursorx + i + 1, cursory]);
                                }

                                Console.SetCursorPosition(cursorx, cursory);           // delete P (old position)
                                Console.WriteLine(" ");
                                cursorx++;
                            }
                            else if (nonIncreasingCheck == true && screen[cursorx + numberCount + 1, cursory] == " ")//Push 
                            {
                                for (int i = numberCount; i >= 1; i--)//Rakamları kaydırma işlemi. En sondaki rakamı kaydırmaktan başlayarak Player'a kadar devam eder.
                                {
                                    string temp2 = screen[cursorx + i, cursory];
                                    screen[cursorx + i, cursory] = " ";
                                    screen[cursorx + i + 1, cursory] = temp2;
                                    Console.SetCursorPosition(cursorx + i, cursory);
                                    Console.WriteLine(screen[cursorx + i, cursory]);
                                    Console.SetCursorPosition(cursorx + i + 1, cursory);
                                    Console.WriteLine(screen[cursorx + i + 1, cursory]);


                                }
                                Console.SetCursorPosition(cursorx, cursory);           // delete P (old position)
                                Console.WriteLine(" ");
                                cursorx++;
                            }

                        }

                    }
                    if (cki.Key == ConsoleKey.LeftArrow && cursorx > 1)
                    {
                        if (screen[cursorx - 1, cursory] == " ")
                        {
                            Console.SetCursorPosition(cursorx, cursory);           // delete X (old position)
                            Console.WriteLine(" ");
                            cursorx--;
                        }
                        else if (screen[cursorx - 1, cursory] == "0")
                        {
                            Console.SetCursorPosition(cursorx, cursory);           // delete X (old position)
                            Console.WriteLine(" ");
                            lifeCount--;
                            do
                            {
                                cursorx = random.Next(1, 52);
                                cursory = random.Next(1, 22);
                            }
                            while (screen[cursorx, cursory] != " ");
                        }
                        else if (screen[cursorx - 1, cursory] != "#" && screen[cursorx - 2, cursory] == " ")
                        {
                            if (int.TryParse(screen[cursorx - 1, cursory], out int temp1))
                            {
                                screen[cursorx - 1, cursory] = " ";
                                screen[cursorx - 2, cursory] = temp1.ToString();
                                Console.SetCursorPosition(cursorx - 1, cursory);
                                Console.WriteLine(screen[cursorx - 1, cursory]);
                                Console.SetCursorPosition(cursorx - 2, cursory);
                                Console.WriteLine(screen[cursorx - 2, cursory]);

                            }
                            Console.SetCursorPosition(cursorx, cursory);
                            Console.WriteLine(" ");
                            cursorx--;
                        }
                        else//Birden fazla rakam için Pushing ve Smashing işlemi
                        {
                            int numberCount = 0;
                            while (screen[cursorx - 1 - numberCount, cursory] != " " && screen[cursorx - 1 - numberCount, cursory] != "#")
                                numberCount++;//Player'ın önünde kaç sayı olduğu belirlenir.
                            bool nonIncreasingCheck = true;
                            for (int i = 1; i < numberCount; i++)
                            {
                                if (Convert.ToInt32(screen[cursorx - i, cursory]) < Convert.ToInt32(screen[cursorx - 1 - i, cursory]))//Rakamların artıp artmadığı kontrol edilir.
                                    nonIncreasingCheck = false;
                            }
                            if (nonIncreasingCheck == true && numberCount > 1 && screen[cursorx - numberCount - 1, cursory] == "#")//Smash
                            {
                                if (screen[cursorx - numberCount, cursory] == "0")//Smash edilen rakama göre Player'a puan verir.
                                    gameScore += 20;
                                else if (screen[cursorx - numberCount, cursory] == "1" || screen[cursorx - numberCount, cursory] == "2" || screen[cursorx - numberCount, cursory] == "3" || screen[cursorx - numberCount, cursory] == "4")
                                    gameScore += 2;
                                else
                                    gameScore += 1;
                                Console.SetCursorPosition(56, 6);
                                Console.WriteLine("Score  : " + gameScore);

                                screen[cursorx - numberCount, cursory] = " ";
                                int newNumber, x, y;
                                do
                                {
                                    x = random.Next(1, 52);
                                    y = random.Next(1, 22);
                                    newNumber = random.Next(5, 10);
                                }
                                while (screen[x, y] != " ");//Smash edilen rakam için yeni koordinatlar belirlenir ve 5'ten 9'a kadar yeni rakam seçilir.
                                screen[x, y] = newNumber.ToString();
                                Console.SetCursorPosition(x, y);
                                Console.WriteLine(screen[x, y]);

                                for (int i = numberCount - 1; i >= 1; i--)//Rakamları kaydırma işlemi. Smash edilecek rakamın öncekinden kaydırmaya başlayarak Player'a kadar devam eder.
                                {
                                    string temp2 = screen[cursorx - i, cursory];
                                    screen[cursorx - i, cursory] = " ";
                                    screen[cursorx - i - 1, cursory] = temp2;
                                    Console.SetCursorPosition(cursorx - i, cursory);
                                    Console.WriteLine(screen[cursorx - i, cursory]);
                                    Console.SetCursorPosition(cursorx - i - 1, cursory);
                                    Console.WriteLine(screen[cursorx - i - 1, cursory]);

                                }

                                Console.SetCursorPosition(cursorx, cursory);           // delete P (old position)
                                Console.WriteLine(" ");
                                cursorx--;

                            }
                            else if (nonIncreasingCheck == true && screen[cursorx - numberCount - 1, cursory] == " ")//Push
                            {
                                for (int i = numberCount; i >= 1; i--)//Rakamları kaydırma işlemi. En sondaki rakamı kaydırmaktan başlayarak Player'a kadar devam eder.
                                {
                                    string temp2 = screen[cursorx - i, cursory];
                                    screen[cursorx - i, cursory] = " ";
                                    screen[cursorx - i - 1, cursory] = temp2;
                                    Console.SetCursorPosition(cursorx - i, cursory);
                                    Console.WriteLine(screen[cursorx - i, cursory]);
                                    Console.SetCursorPosition(cursorx - i - 1, cursory);
                                    Console.WriteLine(screen[cursorx - i - 1, cursory]);

                                }
                                Console.SetCursorPosition(cursorx, cursory);           // delete P (old position)
                                Console.WriteLine(" ");
                                cursorx--;
                            }

                        }

                    }
                    if (cki.Key == ConsoleKey.UpArrow && cursory > 1)
                    {
                        if (screen[cursorx, cursory - 1] == " ")
                        {
                            Console.SetCursorPosition(cursorx, cursory);           // delete X (old position)
                            Console.WriteLine(" ");
                            cursory--;
                        }
                        else if (screen[cursorx, cursory - 1] == "0")
                        {
                            Console.SetCursorPosition(cursorx, cursory);           // delete X (old position)
                            Console.WriteLine(" ");
                            lifeCount--;
                            do
                            {
                                cursorx = random.Next(1, 52);
                                cursory = random.Next(1, 22);
                            }
                            while (screen[cursorx, cursory] != " ");
                        }
                        else if (screen[cursorx, cursory - 1] != "#" && screen[cursorx, cursory - 2] == " ")
                        {
                            if (int.TryParse(screen[cursorx, cursory - 1], out int temp1))
                            {
                                screen[cursorx, cursory - 1] = " ";
                                screen[cursorx, cursory - 2] = temp1.ToString();
                                Console.SetCursorPosition(cursorx, cursory - 1);
                                Console.WriteLine(screen[cursorx, cursory - 1]);
                                Console.SetCursorPosition(cursorx, cursory - 2);
                                Console.WriteLine(screen[cursorx, cursory - 2]);
                            }
                            Console.SetCursorPosition(cursorx, cursory);
                            Console.WriteLine(" ");
                            cursory--;
                        }
                        else//Birden fazla rakam için Pushing ve Smashing işlemi
                        {
                            int numberCount = 0;
                            while (screen[cursorx, cursory - 1 - numberCount] != " " && screen[cursorx, cursory - 1 - numberCount] != "#")
                                numberCount++;//Player'ın önünde kaç sayı olduğu belirlenir.
                            bool nonIncreasingCheck = true;
                            for (int i = 1; i < numberCount; i++)
                            {
                                if (Convert.ToInt32(screen[cursorx, cursory - i]) < Convert.ToInt32(screen[cursorx, cursory - i - 1]))//Rakamların artıp artmadığı kontrol edilir.
                                    nonIncreasingCheck = false;
                            }
                            if (nonIncreasingCheck == true && numberCount > 1 && screen[cursorx, cursory - numberCount - 1] == "#")//Smash
                            {
                                if (screen[cursorx, cursory - numberCount] == "0")//Smash edilen rakama göre Player'a puan verir.
                                    gameScore += 20;
                                else if (screen[cursorx, cursory - numberCount] == "1" || screen[cursorx, cursory - numberCount] == "2" || screen[cursorx, cursory - numberCount] == "3" || screen[cursorx, cursory - numberCount] == "4")
                                    gameScore += 2;
                                else
                                    gameScore += 1;
                                Console.SetCursorPosition(56, 6);
                                Console.WriteLine("Score  : " + gameScore);

                                screen[cursorx, cursory - numberCount] = " ";
                                int newNumber, x, y;
                                do
                                {
                                    x = random.Next(1, 52);
                                    y = random.Next(1, 22);
                                    newNumber = random.Next(5, 10);
                                }
                                while (screen[x, y] != " ");//Smash edilen rakam için yeni koordinatlar belirlenir ve 5'ten 9'a kadar yeni rakam seçilir.
                                screen[x, y] = newNumber.ToString();
                                Console.SetCursorPosition(x, y);
                                Console.WriteLine(screen[x, y]);

                                for (int i = numberCount - 1; i >= 1; i--)//Rakamları kaydırma işlemi. Smash edilecek rakamın öncekinden kaydırmaya başlayarak Player'a kadar devam eder.
                                {
                                    string temp2 = screen[cursorx, cursory - i];
                                    screen[cursorx, cursory - i] = " ";
                                    screen[cursorx, cursory - i - 1] = temp2;
                                    Console.SetCursorPosition(cursorx, cursory - i);
                                    Console.WriteLine(screen[cursorx, cursory - i]);
                                    Console.SetCursorPosition(cursorx, cursory - i - 1);
                                    Console.WriteLine(screen[cursorx, cursory - i - 1]);

                                }

                                Console.SetCursorPosition(cursorx, cursory);           // delete P (old position)
                                Console.WriteLine(" ");
                                cursory--;
                            }
                            else if (nonIncreasingCheck == true && screen[cursorx, cursory - numberCount - 1] == " ")//Push
                            {
                                for (int i = numberCount; i >= 1; i--)//Rakamları kaydırma işlemi. En sondaki rakamı kaydırmaktan başlayarak Player'a kadar devam eder.
                                {
                                    string temp2 = screen[cursorx, cursory - i];
                                    screen[cursorx, cursory - i] = " ";
                                    screen[cursorx, cursory - i - 1] = temp2;
                                    Console.SetCursorPosition(cursorx, cursory - i);
                                    Console.WriteLine(screen[cursorx, cursory - i]);
                                    Console.SetCursorPosition(cursorx, cursory - i - 1);
                                    Console.WriteLine(screen[cursorx, cursory - i - 1]);

                                }
                                Console.SetCursorPosition(cursorx, cursory);           // delete P (old position)
                                Console.WriteLine(" ");
                                cursory--;
                            }

                        }

                    }
                    if (cki.Key == ConsoleKey.DownArrow && cursory < 21)
                    {
                        if (screen[cursorx, cursory + 1] == " ")
                        {
                            Console.SetCursorPosition(cursorx, cursory);           // delete X (old position)
                            Console.WriteLine(" ");
                            cursory++;
                        }
                        else if (screen[cursorx, cursory + 1] == "0")
                        {
                            Console.SetCursorPosition(cursorx, cursory);           // delete X (old position)
                            Console.WriteLine(" ");
                            lifeCount--;
                            do
                            {
                                cursorx = random.Next(1, 52);
                                cursory = random.Next(1, 22);
                            }
                            while (screen[cursorx, cursory] != " ");
                        }
                        else if (screen[cursorx, cursory + 1] != "#" && screen[cursorx, cursory + 2] == " ")
                        {
                            if (int.TryParse(screen[cursorx, cursory + 1], out int temp1))
                            {
                                screen[cursorx, cursory + 1] = " ";
                                screen[cursorx, cursory + 2] = temp1.ToString();
                                Console.SetCursorPosition(cursorx, cursory + 1);
                                Console.WriteLine(screen[cursorx, cursory + 1]);
                                Console.SetCursorPosition(cursorx, cursory + 2);
                                Console.WriteLine(screen[cursorx, cursory + 2]);

                            }
                            Console.SetCursorPosition(cursorx, cursory);
                            Console.WriteLine(" ");
                            cursory++;
                        }
                        else//Birden fazla rakam için Pushing ve Smashing işlemi
                        {
                            int numberCount = 0;
                            while (screen[cursorx, cursory + 1 + numberCount] != " " && screen[cursorx, cursory + 1 + numberCount] != "#")
                                numberCount++;//Player'ın önünde kaç sayı olduğu belirlenir.
                            bool nonIncreasingCheck = true;
                            for (int i = 1; i < numberCount; i++)
                            {
                                if (Convert.ToInt32(screen[cursorx, cursory + i]) < Convert.ToInt32(screen[cursorx, cursory + i + 1]))//Rakamların artıp artmadığı kontrol edilir.
                                    nonIncreasingCheck = false;
                            }
                            if (nonIncreasingCheck == true && numberCount > 1 && screen[cursorx, cursory + numberCount + 1] == "#")//Smash
                            {
                                if (screen[cursorx, cursory + numberCount] == "0")//Smash edilen rakama göre Player'a puan verir.
                                    gameScore += 20;
                                else if (screen[cursorx, cursory + numberCount] == "1" || screen[cursorx, cursory + numberCount] == "2" || screen[cursorx, cursory + numberCount] == "3" || screen[cursorx, cursory + numberCount] == "4")
                                    gameScore += 2;
                                else
                                    gameScore += 1;

                                screen[cursorx, cursory + numberCount] = " ";
                                int newNumber, x, y;
                                do
                                {
                                    x = random.Next(1, 52);
                                    y = random.Next(1, 22);
                                    newNumber = random.Next(5, 10);
                                }
                                while (screen[x, y] != " ");//Smash edilen rakam için yeni koordinatlar belirlenir ve 5'ten 9'a kadar yeni rakam seçilir.
                                screen[x, y] = newNumber.ToString();
                                Console.SetCursorPosition(x, y);
                                Console.WriteLine(screen[x, y]);

                                for (int i = numberCount - 1; i >= 1; i--)//Rakamları kaydırma işlemi. Smash edilecek rakamın öncekinden kaydırmaya başlayarak Player'a kadar devam eder.
                                {
                                    string temp2 = screen[cursorx, cursory + i];
                                    screen[cursorx, cursory + i] = " ";
                                    screen[cursorx, cursory + i + 1] = temp2;
                                    Console.SetCursorPosition(cursorx, cursory + i);
                                    Console.WriteLine(screen[cursorx, cursory + i]);
                                    Console.SetCursorPosition(cursorx, cursory + i + 1);
                                    Console.WriteLine(screen[cursorx, cursory + i + 1]);

                                }

                                Console.SetCursorPosition(cursorx, cursory);           // delete P (old position)
                                Console.WriteLine(" ");
                                cursory++;
                            }
                            else if (nonIncreasingCheck == true && screen[cursorx, cursory + numberCount + 1] == " ")//Push
                            {
                                for (int i = numberCount; i >= 1; i--)//Rakamları kaydırma işlemi. En sondaki rakamı kaydırmaktan başlayarak Player'a kadar devam eder.
                                {
                                    string temp2 = screen[cursorx, cursory + i];
                                    screen[cursorx, cursory + i] = " ";
                                    screen[cursorx, cursory + i + 1] = temp2;
                                    Console.SetCursorPosition(cursorx, cursory + i);
                                    Console.WriteLine(screen[cursorx, cursory + i]);
                                    Console.SetCursorPosition(cursorx, cursory + i + 1);
                                    Console.WriteLine(screen[cursorx, cursory + i + 1]);

                                }
                                Console.SetCursorPosition(cursorx, cursory);           // delete P (old position)
                                Console.WriteLine(" ");
                                cursory++;
                            }

                        }

                    }
                    if (cki.Key == ConsoleKey.Escape)
                        break;

                    if (Console.KeyAvailable)
                    {
                        Console.ReadKey();
                    }
                }
                Console.SetCursorPosition(cursorx, cursory);    // refresh P (current position)
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("P");
                Console.ResetColor();

                TimeSpan duration = DateTime.Now - initialTime;


                if (((int)duration.TotalMilliseconds) % 1000 == 0)//Sıfırların her saniyede bir hareketi
                {
                    bool[,] zeroMoved = new bool[52, 22];
                    for (int i = 1; i < screen.GetLength(0); i++)
                    {
                        for (int j = 1; j < screen.GetLength(1); j++)
                        {
                            if (screen[i, j] == "0" && zeroMoved[i, j] == false)
                            {

                                ZeroMovement(screen, ref i, ref j, ref lifeCount, ref cursorx, ref cursory);
                                zeroMoved[i, j] = true;
                            }
                        }
                    }
                }
                if (((int)duration.TotalMilliseconds) % 15000 == 0 && ((int)duration.TotalMilliseconds) > 0)//Sayıların 15 saniyede bir 1 azaltılma işlemi
                {

                    for (int i = 0; i < 53; i++)
                    {
                        for (int j = 0; j < 23; j++)
                        {
                            if (int.TryParse(screen[i, j], out int tempNumber))
                            {

                                if (tempNumber == 1)
                                {
                                    int conversionProb = random.Next(1, 101);
                                    if (conversionProb < 11)
                                    {
                                        screen[i, j] = "0";
                                        Console.SetCursorPosition(i, j);
                                        Console.WriteLine(screen[i, j]);
                                    }
                                }
                                else if (tempNumber > 1 && tempNumber <= 9)
                                {
                                    screen[i, j] = (tempNumber - 1).ToString();
                                    Console.SetCursorPosition(i, j);
                                    Console.WriteLine(screen[i, j]);
                                }

                            }
                            else continue;
                        }
                    }
                }

                Console.SetCursorPosition(56, 2);
                Console.WriteLine("Time   :" + ((int)duration.TotalSeconds));
                Console.SetCursorPosition(56, 4);
                Console.WriteLine("Life   : " + lifeCount);
                Console.SetCursorPosition(56, 6);
                Console.WriteLine("Score  : " + gameScore);
            }
            if (lifeCount == 0)
            {
                Console.SetCursorPosition(0, 24);
                Console.WriteLine("Game Over");
            }
            Console.ReadLine();
        }
    }
}