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
        public static int MinutesSinceStart;

        public FakeTime(int h, int m)
        {
            TickInterval = 100;
            Hours = h;
            Minutes = m;

        }

        public string GetFormattedTimeString()
        {
            return Hours.ToString().PadLeft(2, '0') + ":" + Minutes.ToString().PadLeft(2, '0');
        }

        public void StartTime()
        {
            TimeThread = new Thread(Tick);
            TimeThread.Start();
        }

        public void Tick()
        {
             // Detta funkar om vi får klockan att starta när tåget börjar åka..

            Thread.Sleep(TickInterval);
            Minutes++;
            MinutesSinceStart++;
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
            //Console.WriteLine(Hours.ToString().PadLeft(2, '0') + ":" + Minutes.ToString().PadLeft(2, '0'));
            Tick();
        }
    }
}
