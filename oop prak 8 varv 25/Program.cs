using System;
using System.Threading;

class Program
{
    static int[] array = new int[20];
    static double arithmeticMean;
    static double geometricMean;

    static void ThreadT0()
    {
        double sum = 0;
        foreach (int x in array)
            sum += x;

        arithmeticMean = sum / array.Length;
        Console.WriteLine($"[T0] Середнє арифметичне = {arithmeticMean:F4}");
    }

    static void ThreadT1()
    {
        double logSum = 0;
        foreach (int x in array)
            logSum += Math.Log(x);

        geometricMean = Math.Exp(logSum / array.Length);
        Console.WriteLine($"[T1] Середнє геометричне = {geometricMean:F4}");
    }

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Random rnd = new Random();
        for (int i = 0; i < array.Length; i++)
            array[i] = rnd.Next(1, 51);

        Console.WriteLine("Масив: " + string.Join(", ", array));
        Console.WriteLine();

        Thread t0 = new Thread(ThreadT0);
        Thread t1 = new Thread(ThreadT1);                

        t0.Start();
        t1.Start();

        t0.Join();
        t1.Join();

        Console.WriteLine();
        Console.WriteLine("Обидва потоки завершили роботу.");
        Console.WriteLine($"Перевірка: AM = {arithmeticMean:F4}, GM = {geometricMean:F4} (AM ≥ GM)");
    }
}