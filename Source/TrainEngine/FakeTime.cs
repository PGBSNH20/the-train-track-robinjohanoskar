using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TrainEngine
{
    public class FakeTime
    {
        public int TickInterval { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public Thread TimeThread { get; set; }

        public FakeTime(int h, int m)
        {
            TickInterval = 100;
            Hours = h;
            Minutes = m;
        }

        public void StartTime()
        {
            TimeThread = new Thread(Tick);
            TimeThread.Start();
        }

        public void Tick()
        {
            Thread.Sleep(TickInterval);
            Minutes++;
            if (Minutes == 60)
            {
                Hours++;
                Minutes = 0;
            }
            if (Hours == 24)
            {
                Hours = 0;
                Minutes = 0;
            }
            Console.WriteLine(Hours.ToString().PadLeft(2, '0') + ":" + Minutes.ToString().PadLeft(2, '0'));
            Tick();
        }
    }
}
