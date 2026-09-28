using System.Diagnostics;

namespace systemtask1
{
    class Program
    {
        static void Main()
        {
            Thread thread1 = new Thread(ShowProcesses);
            Thread thread2 = new Thread(Command);

            thread1.Start();
            thread2.Start();
        }

        static void ShowProcesses()
        {
            while (true)
            {
                Console.Clear();

                Process[] processes = Process.GetProcesses();

                var last20 = processes
                    .OrderByDescending(p => p.StartTime)
                    .Take(20);

                Console.WriteLine("Son yaranmış 20 prossess:");
                Console.WriteLine("_______");

                foreach (var process in last20)
                {
                    Console.WriteLine(process.Id + " - " + process.ProcessName);
                }

                Thread.Sleep(500);
            }
        }

        static void Command()
        {
            while (true)
            {
                Console.WriteLine();
                Console.Write("kamanda: ");

                string command = Console.ReadLine();

                if (command.StartsWith("start "))
                {
                    string processName = command.Substring(6);

                    Process.Start(processName);
                }
                else if (command.StartsWith("kill "))
                {
                    string processName = command.Substring(5);

                    Process[] processes = Process.GetProcessesByName(
                        Path.GetFileNameWithoutExtension(processName)
                    );

                    foreach (var process in processes)
                    {
                        process.Kill();
                    }
                }
            }
        }
    }
}