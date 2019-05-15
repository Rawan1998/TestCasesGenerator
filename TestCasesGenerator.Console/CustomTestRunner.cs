using System;
using System.IO;
using System.Runtime.InteropServices;

namespace TestCasesGenerator.Console
{
    public class CustomTestRunner
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("Kernel32")]
        private static extern void AllocConsole();

        [DllImport("Kernel32", SetLastError = true)]
        private static extern void FreeConsole();
        
        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;

        private static CustomTestRunner _instance;
        public static CustomTestRunner Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CustomTestRunner();
                }
                return _instance;
            }
        }

        private CustomTestRunner()
        {
            AllocConsole();
        }

        public void RunCustomTest(string filePath)
        {
            ShowWindow(GetConsoleWindow(), 5);

            System.Console.Clear();
            string[] parameters = { filePath };
            Program.Main(parameters);

            ShowWindow(GetConsoleWindow(), 0);
        }
    }
}
