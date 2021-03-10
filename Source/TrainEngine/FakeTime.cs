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
        public int Hours { get; set; }
        public int Minutes { get; set; }

        public Thread TimeThread { get; set; }
        public FakeTime(int h, int m)
        {
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
            Thread.Sleep(1000);
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
            Console.WriteLine(Hours + ":" + Minutes);
            Tick();
        }

    }
}
