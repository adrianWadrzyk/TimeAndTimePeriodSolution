using System;

namespace TimeAndTimePeriod
{
    public struct Time : IEquatable<Time>, IComparable<Time>
    {
        public byte Hours { get; }
        public byte Minutes { get; }
        public byte Seconds { get; }
        public Time(int hour = 0, int minutes = 0, int seconds = 0)
        {
            this.Hours = (byte)hour;
            this.Minutes = (byte)minutes;
            this.Seconds = (byte)seconds;
            checkParam();
        }
        public Time(string time)
        {
            time = time.Trim();
            string[] tmpStr = time.Split(':');
            if (time == "")
            {
                this.Hours = 0;
                this.Minutes = 0;
                this.Seconds = 0;
            }
            else if (tmpStr.Length == 1)
            {
                try
                {
                    this.Hours = Convert.ToByte(tmpStr[0]);
                    this.Minutes = 0;
                    this.Seconds = 0;
                    checkParam();
                }
                catch
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            else if (tmpStr.Length == 2)
            {
                try
                {
                    this.Hours = Convert.ToByte(tmpStr[0]);
                    this.Minutes = Convert.ToByte(tmpStr[1]);
                    this.Seconds = 0;
                    checkParam();
                }
                catch
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            else if (tmpStr.Length == 3)
            {
                try
                {
                    this.Hours = Convert.ToByte(tmpStr[0]);
                    this.Minutes = Convert.ToByte(tmpStr[1]);
                    this.Seconds = Convert.ToByte(tmpStr[2]);
                    checkParam();
                }
                catch
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }
        private void checkParam()
        {
            if (Hours > 23 || Minutes > 59 || Seconds > 59)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
        public bool Equals(Time other)
        {
            if (Object.ReferenceEquals(this, other)) return true;

            return (Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds);
        }
        public override bool Equals(object obj)
        {
            if (obj is Time)
                return Equals((Time)obj);
            else
                return false;
        }
        public override int GetHashCode() => (Hours, Minutes, Seconds).GetHashCode();
        public int CompareTo(Time other)
        {
            if (other == null) return 1;
            if (Equals(other)) return 0;

            if (Hours != other.Hours)
            {
                return Hours.CompareTo(other.Hours);
            }
            else if (Minutes != other.Minutes)
                return Minutes.CompareTo(other.Minutes);
            else
                return Seconds.CompareTo(other.Seconds);
        }
        public override string ToString()
        {
            return $"{Hours:00}:{Minutes:00}:{Seconds:00}";
        }
        public static bool operator ==(Time operand1, Time operand2) => Equals(operand1, operand2);
        public static bool operator !=(Time operand1, Time operand2) => !(operand1 == operand2);
        public static bool operator >(Time operand1, Time operand2) => operand1.CompareTo(operand2) > 0;
        public static bool operator <(Time operand1, Time operand2) => operand1.CompareTo(operand2) < 0;
        public static bool operator >=(Time operand1, Time operand2) => operand1.CompareTo(operand2) >= 0;
        public static bool operator <=(Time operand1, Time operand2) => operand1.CompareTo(operand2) <= 0;
        public static Time operator +(Time t1, Time t2)
        {
            byte h, m, s;
            s = (byte)((t1.Seconds + t2.Seconds) % 60);
            m = (byte)((t1.Minutes + t2.Minutes) % 60 + (t1.Seconds + t2.Seconds) / 60);
            h = (byte)((t1.Hours + t2.Hours) % 24 + (t1.Minutes + t2.Minutes) / 60);
            Time t = new Time(h, m, s);
            return t;
        }
        public static Time operator -(Time t1, TimePeriod tp)
        {

            TimePeriod oldTime = new TimePeriod(t1.Hours, t1.Minutes, t1.Seconds);
            TimePeriod t2 = oldTime - tp;
            if (oldTime < tp)
            {
                TimePeriod tp2 = new TimePeriod(seconds: Math.Abs(t2.Seconds) % 86400);
                tp2 = new TimePeriod(seconds: Math.Abs(tp2.Seconds));
                TimePeriod t24 = new TimePeriod(24);
                TimePeriod sum = t24 - tp2;
                long Thours = Math.Abs(sum.Seconds / 3600);
                long Tminutes = Math.Abs((sum.Seconds % 3600) / 60);
                long Tseconds = Math.Abs(sum.Seconds % 60);
                return new Time((int)Thours, (int)Tminutes, (int)Tseconds);
            }
            else
            {
                long Thours = Math.Abs(t2.Seconds / 3600);
                long Tminutes = Math.Abs((t2.Seconds % 3600) / 60);
                long Tseconds = Math.Abs(t2.Seconds % 60);
                return new Time((int)Thours, (int)Tminutes, (int)Tseconds);
            }
        }
        public static Time Plus(Time t, TimePeriod tp)
        {
            long Tphours = tp.Seconds / 3600;
            long Tpminutes = tp.Seconds % 3600 / 60;
            long Tpseconds = tp.Seconds % 60;
            byte h, m, s;
            s = (byte)((t.Seconds + Tpseconds) % 60);
            m = (byte)((t.Minutes + Tpminutes) % 60 + (t.Seconds + Tpseconds) / 60);
            h = (byte)((t.Hours + Tphours) % 24 + (t.Minutes + Tpminutes) / 60);
            Time t1 = new Time(h, m, s);
            return t1;
        }
        public Time Plus(TimePeriod tp)
        {
            long Tphours = tp.Seconds / 3600;
            long Tpminutes = tp.Seconds % 3600 / 60;
            long Tpseconds = tp.Seconds % 60;
            byte h, m, s;
            s = (byte)((Seconds + Tpseconds) % 60);
            m = (byte)((Minutes + Tpminutes) % 60 + (Seconds + Tpseconds) / 60);
            h = (byte)((Hours + Tphours) % 24 + (Minutes + Tpminutes) / 60);
            Time t1 = new Time(h, m, s);
            return t1;
        }

        
    }

    public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        public long Seconds { get; set; }

        public TimePeriod(double hours = 0, double minutes = 0, double seconds = 0)
        {
            Seconds = (long)((Math.Floor(hours * 3600)) + (Math.Floor(minutes * 60)) + Math.Floor(seconds));
        }

        public TimePeriod(string time)
        {
            time = time.Trim();
            string[] tmpStr = time.Split(':');
            long hours, minutes, seconds;
            if (time == "")
            {
                hours = 0;
                minutes = 0;
                seconds = 0;
            }
            else if (tmpStr.Length == 1)
            {
                try
                {
                    hours = Convert.ToInt64(tmpStr[0]);
                    minutes = 0;
                    seconds = 0;
                }
                catch
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            else if (tmpStr.Length == 2)
            {
                try
                {
                    hours = Convert.ToInt64(tmpStr[0]);
                    minutes = Convert.ToInt64(tmpStr[1]);
                    seconds = 0;
                }
                catch
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            else if (tmpStr.Length == 3)
            {
                try
                {
                    hours = Convert.ToInt64(tmpStr[0]);
                    minutes = Convert.ToInt64(tmpStr[1]);
                    seconds = Convert.ToInt64(tmpStr[2]);
                }
                catch
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                throw new ArgumentException();
            }

            Seconds = (hours * 3600) + (minutes * 60) + seconds;
        }
        public bool Equals(TimePeriod other)
        {
            if (Object.ReferenceEquals(this, other)) return true;

            return (Seconds == other.Seconds);
        }
        public override bool Equals(object obj)
        {
            if (obj is TimePeriod)
                return Equals((TimePeriod)obj);
            else
                return false;
        }
        public override int GetHashCode() => (Seconds).GetHashCode();

        public int CompareTo(TimePeriod other)
        {
            if (Equals(other)) return 0;
            Console.WriteLine(this.Seconds);
            Console.WriteLine(other.Seconds);
            return Seconds.CompareTo(other.Seconds);
        }

        public override string ToString() // reprezentacja tekstowa TimePeriod
        {
            long hours = Seconds / 3600;
            long minutes = Seconds % 3600 / 60;
            long seconds = Seconds % 60;
            return $"{hours}:{minutes:00}:{seconds:00}";
        }

        public static bool operator ==(TimePeriod operand1, TimePeriod operand2) => Equals(operand1, operand2);
        public static bool operator !=(TimePeriod operand1, TimePeriod operand2) => !(operand1 == operand2);
        public static bool operator >(TimePeriod operand1, TimePeriod operand2) => operand1.CompareTo(operand2) > 0;
        public static bool operator <(TimePeriod operand1, TimePeriod operand2) => operand1.CompareTo(operand2) < 0;
        public static bool operator >=(TimePeriod operand1, TimePeriod operand2) => operand1.CompareTo(operand2) >= 0;
        public static bool operator <=(TimePeriod operand1, TimePeriod operand2) => operand1.CompareTo(operand2) <= 0;
        public static TimePeriod operator +(TimePeriod tp1, TimePeriod tp2) => new TimePeriod(seconds: tp1.Seconds + tp2.Seconds); // dodawanie dwoch TimePeriod do siebie
        public static TimePeriod operator +(double seconds, TimePeriod tp1) => new TimePeriod(seconds: tp1.Seconds + seconds); // dodawanie sekund do TimePeriod
        public static TimePeriod operator +(TimePeriod tp1, double seconds) => new TimePeriod(seconds: tp1.Seconds + seconds);
        public static TimePeriod operator -(TimePeriod tp1, TimePeriod tp2) => new TimePeriod(seconds: tp1.Seconds - tp2.Seconds); // odejmowanie dwoch TimePeriod do siebie
        public static TimePeriod operator -(double seconds, TimePeriod tp1) => new TimePeriod(seconds: tp1.Seconds - seconds); // odejmowanie sekund do TimePeriod
        public static TimePeriod operator -(TimePeriod tp1, double seconds) => new TimePeriod(seconds: tp1.Seconds - seconds);
        public static TimePeriod operator *(TimePeriod tp1, double multiplier) => new TimePeriod(seconds: tp1.Seconds * multiplier);
        public static TimePeriod operator *(double multiplier, TimePeriod tp1) => new TimePeriod(seconds: tp1.Seconds * multiplier);
        public TimePeriod Plus(TimePeriod other) => new TimePeriod(seconds: Seconds + other.Seconds);
        public static TimePeriod Plus(TimePeriod p1, TimePeriod p2) => new TimePeriod(seconds: p1.Seconds + p2.Seconds);
        public static TimePeriod PlusSeconds(TimePeriod p1, double seconds) => new TimePeriod(seconds: p1.Seconds + seconds); // dodawanie sekund
        public static TimePeriod PlusMinutes(TimePeriod p1, double minutes) => new TimePeriod(seconds: p1.Seconds + minutes * 60); // dodawanie minut
        public static TimePeriod PlusHours(TimePeriod p1, double hours) => new TimePeriod(seconds: p1.Seconds + hours * 3600); // dodawanie godzin
        public TimePeriod Minus(TimePeriod other) => new TimePeriod(seconds: Seconds - other.Seconds); // odejmowanie TimePeriod od TimePeriod
        public static TimePeriod Minus(TimePeriod p1, TimePeriod p2) => new TimePeriod(seconds: p1.Seconds - p2.Seconds); // odejmowanie TimePeriod od TimePeriod statyczne
    }
}
