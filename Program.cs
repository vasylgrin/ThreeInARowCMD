namespace ThreeInARow
{
    internal class Program
    {
        static int[,] MainArray;
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            int counterOfOperation = 0;
            
            Console.WriteLine("Заповнена матриця: ");
            FullMatrixFilling();
            PrintMatrix();
            Console.WriteLine("Щоб побачити дубльованні кольори натисни Enter.");
            Console.ReadLine();

            while (true)
            {
                CheckForHorizontal();
                CheckForVertical();

                Console.WriteLine($"(counter: {counterOfOperation}). Позначили місця де було дублювання.");
                PrintMatrix();

                if (isExistChangeNum(counterOfOperation))
                {
                    Console.WriteLine("Щоб продовжити натисни Enter.");
                    Console.ReadLine();

                    ReplaceEmpty();

                    Console.WriteLine("\n\n\n\n");
                    Console.WriteLine("Після заміни: ");
                    PrintMatrix();

                    Console.WriteLine("Щоб продовдити натисни Enter.");
                    Console.ReadLine();

                    counterOfOperation++;
                }
                else
                    break;
            }

            Console.WriteLine("Дублювань немає.");
            Console.WriteLine("Програма завершена.");
        }      

        /// <summary>
        /// Змінює значення -1, на число яке по вертикалі на 1 вище, якщо вище числа немає, то число генерується.
        /// </summary>
        private static void ReplaceEmpty()
        {
            for (int height = 0; height < 9; height++)
            {
                for (int widht = 0; widht < 9; widht++)
                {
                    if (height == 0)
                    {
                        if (MainArray[height, widht] == -1)
                            MainArray[height, widht] = rnd.Next(0, 4);
                    }
                    else
                    {
                        if(Swop(height, widht))
                        {
                            widht--;
                            height--;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Зміна двух значень в матриці по вертикалі.
        /// </summary>
        /// <param name="height">Висота матриці, або перше значення.</param>
        /// <param name="widht">Ширина матриці, або друге значення.</param>
        /// <returns>true - якщо зміна занчення відбулась, false - якщо зміни не було.</returns>
        private static bool Swop(int height, int widht)
        {
            if (MainArray[height, widht] == -1)
            {
                MainArray[height, widht] = MainArray[height - 1, widht];
                MainArray[height - 1, widht] = -1;
                return true;
            }
            return false;
        }      

        /// <summary>
        /// Перевіряє чи є дублювання по вертикалі.
        /// </summary>
        private static void CheckForVertical()
        {
            int value = 0;

            for (int widht = 0; widht < 9; widht++)
            {
                for (int height = 0; height < 9; height++)
                {
                    value = MainArray[height, widht];

                    // якщо 6 і 7 числа не однакові, то 8 немає сенсу перевіряти.
                    if (height == 8 || height == 7 || height >= 6 && value != MainArray[height + 1, height])
                        break;

                    // порівнюються три числа підряд, якщо олнакові то йдемо далі, а вони вже встановлюються -1 для подальшої зміни.
                    if (value == MainArray[height + 1, widht] && value == MainArray[height + 2, widht])
                    {
                        // встановлює для троьох числе -1.
                        MainArray[height, widht] = -1;
                        MainArray[height + 1, widht] = -1;
                        MainArray[height + 2, widht] = -1;

                        // якщо widht <= 5 то, перевіяэ чи схоже четверте число.
                        if (height <= 5 && value == MainArray[height + 3, widht])
                        {
                            MainArray[height + 3, widht] = -1;

                            // аналогічно.
                            if (height <= 4 && value == MainArray[height + 4, widht])
                            {
                                MainArray[height + 4, widht] = -1;
                                height++;
                            }
                            height++;
                        }
                        height += 2;
                    }
                }
            }
        }

        /// <summary>
        /// Перевіряє чи є дублювання по горизонталі.
        /// </summary>
        private static void CheckForHorizontal()
        {
            int value = 0;

            for (int height = 0; height < 9; height++)
            {
                for (int widht = 0; widht < 9; widht++)
                {
                    value = MainArray[height, widht];

                    // якщо 6 і 7 числа не однакові, то 8 немає сенсу перевіряти.
                    if (widht == 8 || widht == 7 || widht >= 6 && value != MainArray[height, widht + 1])
                        break;

                    // порівнюються три числа підряд, якщо олнакові то йдемо далі, а вони вже встановлюються -1 для подальшої зміни.
                    if (value == MainArray[height, widht + 1] && value == MainArray[height, widht + 2])
                    {
                        // встановлює для троьох чисeл -1.
                        MainArray[height, widht] = -1;
                        MainArray[height, widht + 1] = -1;
                        MainArray[height, widht + 2] = -1;

                        // якщо widht <= 5 то, перевіяє чи схоже четверте число.
                        if (widht <= 5 && value == MainArray[height, widht + 3])
                        {
                            MainArray[height, widht + 3] = -1;

                            // якщо widht <= 4 то, перевіяє чи схоже четверте число.
                            if (widht <= 4 && value == MainArray[height, widht + 4])
                            {
                                MainArray[height, widht + 4] = -1;
                                // пропускає числа які вже перевіренні
                                widht++;
                            }

                            MainArray[height, widht + 3] = -1;
                            widht++;
                        }
                        widht += 2;
                    }
                }
            }
        }

        /// <summary>
        /// Перевіряє чи є хоча б один елемент для зміни.
        /// </summary>
        /// <param name="counterOfOperation">Кількість змін.</param>
        /// <returns>true - якщо є хоча б один елемент зі значенням -1, false - якщо жодного елмента зі значенням -1 не знайдено.</returns>
        private static bool isExistChangeNum(int counterOfOperation)
        {
            if (counterOfOperation == 0)
                return true;
            foreach (var item in MainArray)
            {
                if (item == -1)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Заповнення матриці будь якими числами.
        /// </summary>
        private static void FullMatrixFilling()
        {
            MainArray = new int[9, 9];           
            for (int height = 0; height < 9; height++)
            {
                for (int widht = 0; widht < 9; widht++)
                {
                    MainArray[height, widht] = rnd.Next(0, 4);
                }
            }
        }

        /// <summary>
        /// Вивід матриці.
        /// </summary>
        private static void PrintMatrix()
        {
            for (int height = 0; height < 9; height++)
            {
                for (int widht = 0; widht < 9; widht++)
                {
                    if (MainArray[height, widht] == -1)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write($"[{MainArray[height, widht]}]\t");
                    }
                    else if (MainArray[height, widht] == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write($"[{MainArray[height, widht]}]\t");
                    }
                    else if (MainArray[height, widht] == 1)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write($"[{MainArray[height, widht]}]\t");
                    }
                    else if (MainArray[height, widht] == 2)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write($"[{MainArray[height, widht]}]\t");
                    }
                    else if (MainArray[height, widht] == 3)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write($"[{MainArray[height, widht]}]\t");
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Magenta;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write($"[{MainArray[height, widht]}]\t");
                    }
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.Write("\n\n\n");
            }
        }
    }
}