using Newtonsoft.Json;
using System;
using System.Threading;

namespace TrainEngine
{
    public class FakeTime
    {
        private bool _threadRunning = true;

        public int TickInterval { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public Thread TimeThread { get; set; }
        [JsonIgnore]
        public static int MinutesSinceStart;

        public FakeTime(int hours, int minutes)
        {
            TickInterval = 50;
            Hours = hours;
            Minutes = minutes;
        }

        public override string ToString()
        {
            return Hours.ToString().PadLeft(2, '0') + ":" + Minutes.ToString().PadLeft(2, '0');
        }

        public void StartTime()
        {
            TimeThread = new Thread(Tick);
            TimeThread.IsBackground = true;
            TimeThread.Start();
        }

        public void Tick()
        {
            if (!_threadRunning)
            {
                Console.CursorVisible = true;
                return;
            }

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
                _threadRunning = false;
            }

            (int y, int x) = (Console.CursorTop, Console.CursorLeft);
            Console.SetCursorPosition(Console.BufferWidth - 5, 0);
            Console.WriteLine(ToString());
            Console.SetCursorPosition(x, y);
            Tick();
        }
    }
}
