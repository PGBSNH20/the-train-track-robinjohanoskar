using Newtonsoft.Json;
using System.Threading;

namespace TrainEngine
{
    public class FakeTime
    {
        public int TickInterval { get; set; }
        public int Hours { get; set; }
        public int Minutes { get; set; }
        public Thread TimeThread { get; set; }
        [JsonIgnore]
        public static int MinutesSinceStart;

        public FakeTime(int hours, int minutes)
        {
            TickInterval = 100;
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
            TimeThread.Start();
        }

        public void Tick()
        {
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

            // todo print time in top left corner?
            //Console.WriteLine(Hours.ToString().PadLeft(2, '0') + ":" + Minutes.ToString().PadLeft(2, '0'));
            Tick();
        }
    }
}
