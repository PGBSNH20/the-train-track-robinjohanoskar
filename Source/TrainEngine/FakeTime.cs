using Newtonsoft.Json;
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
            TickInterval = 10;
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

            // todo print time in top left corner?
            //Console.WriteLine(Hours.ToString().PadLeft(2, '0') + ":" + Minutes.ToString().PadLeft(2, '0'));
            Tick();
        }
    }
}
