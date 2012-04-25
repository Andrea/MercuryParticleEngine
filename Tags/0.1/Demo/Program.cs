using System;

namespace WindowsGame3
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Demo game = new Demo())
            {
                game.Run();
            }
        }
    }
}

