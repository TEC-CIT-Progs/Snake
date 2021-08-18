using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Snakes
{
    class Program
    {
      /// <summary>
      /// Kalder metoder fra snake.cs
      /// overordnet styring af program.
      /// </summary>
      /// <param name="args"></param>
        static void Main(string[] args)
        {
            
            Snake snake = new Snake();
            snake.ShowBorder();
            snake.MainLoop();
        }
    }
}
