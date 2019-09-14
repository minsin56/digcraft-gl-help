using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigCraft
{
    class Program
    {
        static void Main(string[] args)
        {
            IO.Config.Load();
            using(Digcraft DigCraft = new Digcraft())
            {
                DigCraft.Run(60.0);
            }
        }
    }
}
