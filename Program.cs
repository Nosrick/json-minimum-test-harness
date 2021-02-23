using System;

namespace JSON_Minimum_Test_Harness
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureHandler cultureHandler = new CultureHandler();

            float memoryUsedMB = (System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / 1024f) / 1024f;
            Console.WriteLine("Memory usage at process start " + memoryUsedMB + "MB");
            for (int i = 0; i < 3; i++)
            {
                cultureHandler.Load();
            }
            
            memoryUsedMB = (System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / 1024f) / 1024f;
            Console.WriteLine("After 3 Loads() " + memoryUsedMB + "MB");
            
            for (int i = 0; i < 3; i++)
            {
                cultureHandler.Load();
            }
            
            memoryUsedMB = (System.Diagnostics.Process.GetCurrentProcess().WorkingSet64 / 1024f) / 1024f;
            Console.WriteLine("After 6 Loads() " + memoryUsedMB + "MB");

            Console.ReadLine();
        }
    }
}