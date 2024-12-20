using System;

class MonteCarloSimulation
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Вхідні дані
        Console.Write("Введіть верхню межу інтервалу (n): ");
        int n = int.Parse(Console.ReadLine());

        Console.Write("Введіть кількість проміжків (m): ");
        int numIntervals = int.Parse(Console.ReadLine());

        Console.Write("Введіть кількість випробувань: ");
        int trials = int.Parse(Console.ReadLine());

        Console.WriteLine("Виберіть метод генерації чисел:");
        Console.WriteLine("1. Стандартний генератор");
        Console.WriteLine("2. Метод середини квадрату");
        Console.WriteLine("3. Лінійний конгруентний метод");
        int methodChoice = int.Parse(Console.ReadLine());

        // Запит на введення seed тільки після вибору 2 або 3
        int seed = 0;
        if (methodChoice == 2 || methodChoice == 3)
        {
            Console.Write("Введіть початкове значення для методу середини квадрату або лінійного конгруентного методу (seed): ");
            seed = int.Parse(Console.ReadLine());
        }

        // Генерація випадкових чисел та підрахунок попадань
        int[] intervals = new int[numIntervals];
        Random random = new Random();

        for (int i = 0; i < trials; i++)
        {
            double randomValue = 0;

            // Вибір методу генерації
            switch (methodChoice)
            {
                case 1:
                    randomValue = random.NextDouble() * n; // Стандартний генератор
                    break;
                case 2:
                    // Метод середини квадрату
                    int seedLength = seed.ToString().Length;
                    long square = (long)seed * seed;
                    string squareStr = square.ToString().PadLeft(seedLength * 2, '0');
                    int midStart = (squareStr.Length - seedLength) / 2;
                    seed = int.Parse(squareStr.Substring(midStart, seedLength));
                    randomValue = (double)seed / Math.Pow(10, seedLength) * n;
                    break;
                case 3:
                    // Лінійний конгруентний метод
                    int a = 1664525;
                    int c = 1013904223;
                    int m = (int)Math.Pow(2, 32);  // Тут m є константою для методу
                    seed = (a * seed + c) % m;
                    randomValue = (double)seed / m * n;
                    break;
                default:
                    Console.WriteLine("Невірний вибір методу.");
                    return;
            }

            // Визначаємо, в який проміжок число потрапило
            int index = (int)(randomValue / (n / numIntervals));

            // Перевіряємо, чи не виходить індекс за межі масиву
            if (index >= numIntervals)
                index = numIntervals - 1;
            else if (index < 0)
                index = 0;

            intervals[index]++;
        }

        // Вивід результатів
        Console.WriteLine("\nРезультати:");
        double intervalSize = (double)n / numIntervals;
        for (int i = 0; i < numIntervals; i++)
        {
            double lowerBound = i * intervalSize;
            double upperBound = (i + 1) * intervalSize;
            Console.WriteLine($"[{lowerBound:F2}, {upperBound:F2}): {intervals[i]} попадань");
        }

        Console.WriteLine("\nРобота завершена!");
    }
}
