using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TimeAndTimePeriod;

namespace TimeAndTimePeriodTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod, TestCategory("Constructors_Time")]
        public void Constructor_Default_Time()
        {
            Time time = new Time();
            Assert.AreEqual(0, time.Hours);
            Assert.AreEqual(0, time.Minutes);
            Assert.AreEqual(0, time.Seconds);
        }

        [DataTestMethod, TestCategory("Constructors_Time")]
        [DataRow(1, 2, 3, 1, 2, 3)]
        [DataRow(23, 59, 59, 23, 59, 59)]
        [DataRow(20, 0, 12, 20, 0, 12)]
        [DataRow(15, 23, 59, 15, 23, 59)]
        [DataRow(01, 02, 03, 01, 02, 03)]
        public void Constructor_3params_Time(int hour, int minutes, int seconds,
                                                      int expectedHour, int expectedMinutes, int expectedSeconds)
        {
            Time p = new Time(hour, minutes, seconds);
            Assert.AreEqual(p.Hours, expectedHour);
            Assert.AreEqual(p.Minutes, expectedMinutes);
            Assert.AreEqual(p.Seconds, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors_Time")]
        [DataRow(1, 1, 0, 0)]
        [DataRow(15, 15, 0, 0)]
        [DataRow(20, 20, 0, 0)]
        [DataRow(23, 23, 0, 0)]

        public void Constructor_OnlyHours_Time(int hour, int expectedHour, int expectedMinutes, int expectedSeconds)
        {
            Time p = new Time(hour);
            Assert.AreEqual(p.Hours, expectedHour);
            Assert.AreEqual(p.Minutes, expectedMinutes);
            Assert.AreEqual(p.Seconds, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors_Time")]
        [DataRow(1, 0, 1, 0)]
        [DataRow(15, 0, 15, 0)]
        [DataRow(20, 0, 20, 0)]
        [DataRow(23, 0, 23, 0)]

        public void Constructor_OnlyMinutes_Time(int minutes, int expectedHour, int expectedMinutes, int expectedSeconds)
        {
            Time p = new Time(minutes: minutes);
            Assert.AreEqual(p.Hours, expectedHour);
            Assert.AreEqual(p.Minutes, expectedMinutes);
            Assert.AreEqual(p.Seconds, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors_Time")]
        [DataRow(1, 0, 0, 1)]
        [DataRow(15, 0, 0, 15)]
        [DataRow(20, 0, 0, 20)]
        [DataRow(23, 0, 0, 23)]

        public void Constructor_OnlySeconds_Time(int seconds, int expectedHour, int expectedMinutes, int expectedSeconds)
        {
            Time p = new Time(seconds: seconds);
            Assert.AreEqual(p.Hours, expectedHour);
            Assert.AreEqual(p.Minutes, expectedMinutes);
            Assert.AreEqual(p.Seconds, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors_Time")]
        [DataRow("20:20:20", 20, 20, 20)]
        [DataRow("23:15:12", 23, 15, 12)]
        [DataRow("23:59:12", 23, 59, 12)]
        [DataRow("10:0:0", 10, 0, 0)]
        [DataRow("01:01:01", 01, 01, 01)]
        public void Constructor_String_3_Param(string time, int expectedHour, int expectedMinutes, int expectedSeconds)
        {
            Time p = new Time(time);
            Assert.AreEqual(p.Hours, expectedHour);
            Assert.AreEqual(p.Minutes, expectedMinutes);
            Assert.AreEqual(p.Seconds, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors_Time")]
        [DataRow("20:20", 20, 20, 0)]
        [DataRow("23:15", 23, 15, 0)]
        [DataRow("23:59", 23, 59, 0)]
        [DataRow("10:0", 10, 0, 0)]
        [DataRow("01:01", 01, 01, 0)]
        [DataRow("0001:001:0", 01, 01, 0)]
        [DataRow("010:012", 10, 12, 0)]
        public void Constructor_String_2_Param(string time, int expectedHour, int expectedMinutes, int expectedSeconds)
        {
            Time p = new Time(time);
            Assert.AreEqual(p.Hours, expectedHour);
            Assert.AreEqual(p.Minutes, expectedMinutes);
            Assert.AreEqual(p.Seconds, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors_Time")]
        [DataRow("20", 20, 0, 0)]
        [DataRow("23", 23, 0, 0)]
        [DataRow("09", 09, 0, 0)]
        [DataRow("10", 10, 0, 0)]
        [DataRow("01", 01, 0, 0)]
        public void Constructor_String_1_Param(string time, int expectedHour, int expectedMinutes, int expectedSeconds)
        {
            Time p = new Time(time);
            Assert.AreEqual(p.Hours, expectedHour);
            Assert.AreEqual(p.Minutes, expectedMinutes);
            Assert.AreEqual(p.Seconds, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors_Time")]
        [DataRow(-1, 25, 60)]
        [DataRow(24, 23, 1)]
        [DataRow(30, 2, -1)]
        [DataRow(50, -23, -3)]
        [DataRow(51, 33, 61)]
        [DataRow(1000, 0, -1)]
        [DataRow(9999, 0, -1)]
        [DataRow(5000, 0, -1)]
        [DataRow(-1, -2, -2)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_3params_ArgumentOutOfRangeException(int hour, int minutes, int seconds)
        {
            Time p = new Time(hour, minutes, seconds);
        }

        [DataTestMethod, TestCategory("Constructors_Time")]
        [DataRow("123:20:30")]
        [DataRow("0:202:30")]
        [DataRow("12:20:320")]
        [DataRow("13:220:30")]
        [DataRow("-123:20:30")]
        [DataRow("0:-202:30")]
        [DataRow("12:20:-320")]
        [DataRow("-13:220:30")]
        [DataRow("-13:220:30")]
        [DataRow("-13")]
        [DataRow("13:-220")]
        [DataRow("XXSD")]
        [DataRow("ala ma kota")]
        [DataRow("0-: 0 0: 20")]
        [DataRow("00;00;00")]
        [DataRow("0a: 0b: 20")]
        [DataRow(" :0:02")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_String_ArgumentOutOfRangeException(string time)
        {
            Time p = new Time(time);
        }

        [TestMethod, TestCategory("String representation for Time")]
        [DataRow("20:20:20", "20:20:20")]
        [DataRow("0:20:20", "00:20:20")]
        [DataRow("1:2:2", "01:02:02")]
        [DataRow("20", "20:00:00")]
        [DataRow("23:1", "23:01:00")]
        [DataRow("", "00:00:00")]
        [DataRow("        ", "00:00:00")]

        public void ToString_Time(string time, string expectedStringOut)
        {
            var p = new Time(time);
            Assert.AreEqual(expectedStringOut, p.ToString());
        }

        [DataTestMethod, TestCategory("Equals_Time")]
        [DataRow(10, 20, 20, true)]
        [DataRow(10, 20, 20, true)]
        [DataRow(10, 0, 02, true)]
        [DataRow(10, 00, 2, true)]
        [DataRow(01, 59, 00, true)]
        [DataRow(1, 59, 0, true)]
        public void Are_Equals_Time(int hours, int minutes, int seconds, bool equals)
        {
            var t = new Time(hours, minutes, seconds);
            var t1 = new Time(hours, minutes, seconds);
            Assert.AreEqual(t.Equals(t1), equals);
            Assert.AreEqual(t == t1, equals);
        }

        [DataTestMethod, TestCategory("Equals_Time")]
        [DataRow(10, 20, 20, "10:20:20")]
        [DataRow(01, 2, 2, "01:02:02")]
        [DataRow(10, 20, 20, "10:21:20", false)]
        [DataRow(0, 0, 0, "")]
        [DataRow(00, 20, 20, "0:20:20")]
        [DataRow(23, 59, 59, "23:59:59")]
        [DataRow(0, 0, 0, "       ")]
        [DataRow(01, 20, 0, "01:20")]
        [DataRow(10, 20, 0, "10:20")]
        [DataRow(0, 20, 20, " 0:20:20")]
        [DataRow(00, 20, 20, " 01:21:23", false)]
        [DataRow(001, 20, 20, " 01:21:23", false)]
        [DataRow(001, 20, 20, " 01:20:20")]
        [DataRow(001, 00020, 00020, " 01:20:20")]
        [DataRow(0000000, 0000000, 0000000, " 0:0:0")]

        public void Are_Equals_Int_Time_To_String(int hours, int minutes, int seconds, string time, bool equals = true)
        {
            var t = new Time(hours, minutes, seconds);
            var t1 = new Time(time);
            Assert.AreEqual(t.Equals(t1), equals);
            Assert.AreEqual(t == t1, equals);
        }

        [DataTestMethod, TestCategory("Equals_Time")]
        [DataRow("10:20:20", "10:20:20")]
        [DataRow("1:2:2", "01:02:02")]
        [DataRow("21:20:20", "10:21:20", false)]
        [DataRow("   ", "")]
        [DataRow("00:20:20", "0:20:20")]
        [DataRow("23:59:59", "23:59:59")]
        [DataRow("0", "       ")]
        [DataRow("0:0", "00:0:00")]
        [DataRow("02", "02:0:0")]
        public void Are_Equals_String_Time_To_String(string time, string time1, bool equals = true)
        {
            var t = new Time(time);
            var t1 = new Time(time1);
            Assert.AreEqual(t.Equals(t1), equals);
            Assert.AreEqual(t == t1, equals);
        }

        [DataTestMethod, TestCategory("Greter_Time")]
        [DataRow(10, 00, 00, 9, 59, 59)]
        [DataRow(23, 59, 59, 0, 0, 0)]
        [DataRow(01, 02, 2, 0, 59, 59)]
        [DataRow(01, 00, 0, 0, 00, 000000000)]
        [DataRow(12, 22, 22, 12, 22, 22, false)]
        [DataRow(0000023, 00023, 00023, 0001, 002, 0003)]
        public void Is_Greter_Time(int hour, int minutes, int seconds, int otherHour, int otherMinutes, int otherSeconds, bool grater = true)
        {
            var t = new Time(hour, minutes, seconds);
            var t1 = new Time(otherHour, otherMinutes, otherSeconds);
            Assert.AreEqual(t > t1, grater);
        }

        [DataTestMethod, TestCategory("Greter_Time")]
        [DataRow(10, 00, 00, "9")]
        [DataRow(23, 50, 00, "   ")]
        [DataRow(12, 20, 30, "10:30:20")]
        [DataRow(20, 00012, 00556, "000:32:36")]
        [DataRow(0, 00, 00, "0:00:00", false)]


        public void Is_Greter_Time_String(int hour, int minutes, int seconds, string time, bool grater = true)
        {
            var t = new Time(hour, minutes, seconds);
            var t1 = new Time(time);
            Assert.AreEqual(t > t1, grater);
        }

        [DataTestMethod, TestCategory("Smaller_Time")]
        [DataRow(10, 00, 00, "11:00:00")]
        [DataRow(23, 50, 00, "   ", false)]
        [DataRow(12, 20, 30, "23:59:00")]
        public void Is_smaller(int hour, int minutes, int seconds, string time, bool grater = true)
        {
            var t = new Time(hour, minutes, seconds);
            var t1 = new Time(time);
            Assert.AreEqual(t < t1, grater);
        }

        [DataTestMethod, TestCategory("Greter_Or_Equal_Time")]
        [DataRow(10, 00, 00, "11:00:00", false)]
        [DataRow(23, 50, 00, "   ")]
        [DataRow(12, 20, 30, "12:20:30")]
        [DataRow(0, 0, 0, "  ")]
        public void Is_greter_or_Equals(int hour, int minutes, int seconds, string time, bool grater = true)
        {
            var t = new Time(hour, minutes, seconds);
            var t1 = new Time(time);
            Assert.AreEqual(t >= t1, grater);
        }

        [DataTestMethod, TestCategory("Smaller_Or_Equal_Time")]
        [DataRow(10, 00, 00, "11:00:00")]
        [DataRow(23, 50, 00, "   ", false)]
        [DataRow(12, 20, 30, "12:20:30")]
        [DataRow(0, 0, 0, "  ")]
        public void Is_Smaleer_or_Equals(int hour, int minutes, int seconds, string time, bool grater = true)
        {
            var t = new Time(hour, minutes, seconds);
            var t1 = new Time(time);
            Assert.AreEqual(t <= t1, grater);
        }

        [DataTestMethod, TestCategory("Addition_Time")]
        [DataRow(10, 00, 00, 11, 20, 20, "21:20:20")]
        [DataRow(10, 59, 00, 10, 2, 59, "21:1:59")]
        [DataRow(23, 59, 59, 0, 0, 0, "23:59:59")]
        [DataRow(001, 23, 1, 23, 37, 0, "01:00:1")]
        [DataRow(23, 00, 0, 1, 0, 0, "00:00:0")]
        public void Addition_Time_Operator(int hour, int minutes, int seconds, int hourOther, int minutesOther, int secondsOther, string time)
        {
            var t = new Time(hour, minutes, seconds);
            var t1 = new Time(hourOther, minutesOther, secondsOther);
            var t2 = new Time(time);
            Assert.AreEqual(t + t1, t2);
        }

        [DataTestMethod, TestCategory("Addition_Time")]
        [DataRow(10, 00, 00, 11, 20, 20, "21:20:20")]
        [DataRow(10, 59, 00, 10, 2, 59, "21:1:59")]
        [DataRow(23, 59, 59, 0, 0, 0, "23:59:59")]
        [DataRow(001, 23, 1, 23, 37, 0, "01:00:1")]
        [DataRow(23, 00, 0, 1, 0, 0, "00:00:0")]
        [DataRow(23, 00, 0, 48, 0, 0, "23:00:0")]
        [DataRow(23, 00, 0, 96, 0, 0, "23:00:0")]
        public void Addition_Time_Plus(int hour, int minutes, int seconds, int hourOther, int minutesOther, int secondsOther, string time)
        {
            var t = new Time(hour, minutes, seconds);
            var t1 = new TimePeriod(hourOther, minutesOther, secondsOther);
            var t2 = Time.Plus(t, t1);
            var t3 = new Time(time);
            Assert.AreEqual(t2, t3);
        }

        [DataTestMethod, TestCategory("Addition_Time")]
        [DataRow(10, 00, 00, 11, 20, 20, "21:20:20")]
        [DataRow(10, 59, 00, 10, 2, 59, "21:1:59")]
        [DataRow(23, 59, 59, 0, 0, 0, "23:59:59")]
        [DataRow(001, 23, 1, 23, 37, 0, "01:00:1")]
        [DataRow(23, 00, 0, 1, 0, 0, "00:00:0")]
        [DataRow(23, 00, 0, 48, 0, 0, "23:00:0")]
        [DataRow(23, 00, 0, 96, 0, 0, "23:00:0")]
        public void Addition_Time_Plus_TimePeriod(int hour, int minutes, int seconds, int hourOther, int minutesOther, int secondsOther, string time)
        {
            var t = new Time(hour, minutes, seconds);
            var t1 = new TimePeriod(hourOther, minutesOther, secondsOther);
            var t2 = t.Plus(t1);
            var t3 = new Time(time);
            Assert.AreEqual(t2, t3);
        }

        [DataTestMethod, TestCategory("Minus_Time")]
        [DataRow(10, 00, 00, 11, 20, 20, "22:39:40")]
        [DataRow(10, 59, 00, 10, 2, 59, "0:56:01")]
        public void Addition_Time_Minus_TimePeriod(int hour,
                                                   int minutes,
                                                   int seconds,
                                                   int hourOther,
                                                   int minutesOther,
                                                   int secondsOther,
                                                   string time)
        {
            var t = new Time(hour, minutes, seconds);
            var t1 = new TimePeriod(hourOther, minutesOther, secondsOther);
            var t2 = t - t1;
            var t3 = new Time(time);
            Assert.AreEqual(t3, t2);
        }

        [TestMethod, TestCategory("Constructors_Time_Period")]
        public void Constructor_Default_Time_Period()
        {
            TimePeriod time = new TimePeriod();
            TimePeriod time2 = new TimePeriod(" ");
            Assert.AreEqual(0, time.Seconds);
            Assert.AreEqual(0, time2.Seconds);
        }

        [DataTestMethod, TestCategory("Constructors_Time_Period")]
        [DataRow(0, 0, 0)]
        [DataRow(10, 20, 20)]
        [DataRow(0, 20, 20)]
        [DataRow(23, 20, 20)]
        [DataRow(12000, 200, 20)]
        [DataRow(12000, 200, 20)]
        [DataRow(0.1, 0.2, 20)]
        [DataRow(0.05, 000.2, 1)]
        [DataRow(5, 0000, 24)]
        public void Constructor_3params_TimePeriod(double hour, double minutes, double seconds)

        {
            TimePeriod p = new TimePeriod(hour, minutes, seconds);
            Assert.AreEqual(p.Seconds, Math.Floor(hour * 3600 + minutes * 60 + seconds));
        }

        [DataTestMethod, TestCategory("Constructors_Time_Period")]
        [DataRow(0, 0)]
        [DataRow(25, 30)]
        [DataRow(300, 30)]
        [DataRow(2000, 0.1)]
        [DataRow(159, 1299)]
        [DataRow(001, 023)]
        [DataRow(0, 100000)]
        public void Constructor_2params_TimePeriod(double hour, double minutes)

        {
            TimePeriod p = new TimePeriod(hour, minutes);
            Assert.AreEqual(p.Seconds, Math.Floor(hour * 3600 + minutes * 60));
        }

        [DataTestMethod, TestCategory("Constructors_Time_Period")]
        [DataRow(0)]
        [DataRow(25)]
        [DataRow(300)]
        [DataRow(2000)]
        [DataRow(159)]
        [DataRow(001)]
        [DataRow(0)]
        [DataRow(0.1)]
        [DataRow(23.3)]
        [DataRow(101.23)]
        [DataRow(259.233)]
        public void Constructor_1param_Hour_TimePeriod(double hour)

        {
            TimePeriod p = new TimePeriod(hour);
            Assert.AreEqual(p.Seconds, Math.Floor(hour * 3600));
        }

        [DataTestMethod, TestCategory("Constructors_Time_Period")]
        [DataRow(0)]
        [DataRow(25)]
        [DataRow(300)]
        [DataRow(2000)]
        [DataRow(159)]
        [DataRow(001)]
        [DataRow(0)]
        [DataRow(0.1)]
        [DataRow(23.3)]
        [DataRow(101.23)]
        [DataRow(259.233)]
        [DataRow(600000)]
        [DataRow(239.23)]
        [DataRow(3238983.3)]
        public void Constructor_1param_Minutes_TimePeriod(double minutes)

        {
            TimePeriod p = new TimePeriod(minutes: minutes);
            Assert.AreEqual(p.Seconds, Math.Floor(minutes * 60));
        }

        [DataTestMethod, TestCategory("Constructors_Time_Period")]
        [DataRow(0)]
        [DataRow(25)]
        [DataRow(300)]
        [DataRow(2000)]
        [DataRow(159)]
        [DataRow(001)]
        [DataRow(0)]
        [DataRow(0.1)]
        [DataRow(23.3)]
        [DataRow(101.23)]
        [DataRow(259.233)]
        [DataRow(600000)]
        [DataRow(239.23)]
        [DataRow(3238983.3)]
        public void Constructor_1param_Seconds_TimePeriod(double seconds)

        {
            TimePeriod p = new TimePeriod(seconds: seconds);
            Assert.AreEqual(p.Seconds, Math.Floor(seconds));
        }

        [DataTestMethod, TestCategory("Constructors_TimePeriod")]
        [DataRow("20:20:20", 73220)]
        [DataRow("23:15:12", 83712)]
        [DataRow("23:59:12", 86352)]
        [DataRow("10:0:0", 36000)]
        [DataRow("01:01:01", 3661)]
        [DataRow("3500:199:0100", 12612040)]
        public void Constructor_TimePeriod_String_3_Param(string time, long expectedSeconds)
        {
            TimePeriod p = new TimePeriod(time);
            Assert.AreEqual(p.Seconds, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors_TimePeriod")]
        [DataRow("20:20", 73200)]
        [DataRow("23:15", 83700)]
        [DataRow("23:55", 86100)]
        [DataRow("10:0", 36000)]
        [DataRow("01:01", 3660)]
        [DataRow("2938:01", 10576860)]

        public void Constructor_TimePeriod_String_2_Param(string time, long expectedSeconds)
        {
            TimePeriod p = new TimePeriod(time);
            Assert.AreEqual(p.Seconds, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors_TimePeriod")]
        [DataRow("20", 72000)]
        [DataRow("23", 82800)]
        [DataRow("10", 36000)]
        [DataRow("01", 3600)]
        [DataRow("2938", 10576800)]

        public void Constructor_TimePeriod_String_1_Param(string time, long expectedSeconds)
        {
            TimePeriod p = new TimePeriod(time);
            Assert.AreEqual(p.Seconds, expectedSeconds);
        }

        [DataTestMethod, TestCategory("Constructors_TimePeriod")]
        [DataRow("XXSD")]
        [DataRow("ala ma kota")]
        [DataRow("0-: 0 0: 20")]
        [DataRow("00;00;00")]
        [DataRow("0a: 0b: 20")]
        [DataRow(" :0:02")]
        [DataRow("0,.1:0:02")]
        [DataRow("0.1:0:02")]
        [DataRow("0.1")]
        [DataRow("++0.1")]
        [DataRow("!0.1")]
        [DataRow("0.1")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_TimePeriod_String_ArgumentOutOfRangeException(string time)
        {
            TimePeriod p = new TimePeriod(time);
        }

        [TestMethod, TestCategory("String representation for TimePeriod")]
        [DataRow("20:20:20", "20:20:20")]
        [DataRow("0:20:20", "0:20:20")]
        [DataRow("1:2:2", "1:02:02")]
        [DataRow("20", "20:00:00")]
        [DataRow("23:1", "23:01:00")]
        [DataRow("", "0:00:00")]
        [DataRow("        ", "0:00:00")]

        public void ToString_TimePeriod(string time, string expectedStringOut)
        {
            var p = new TimePeriod(time);
            Assert.AreEqual(expectedStringOut, p.ToString());
        }

        [TestMethod, TestCategory("String representation for TimePeriod")]
        [DataRow(20, 20, 20, "20:20:20")]
        [DataRow(000, 20, 20, "0:20:20")]
        [DataRow(1, 2, 2, "1:02:02")]
        [DataRow(20, 0000, 00000, "20:00:00")]
        [DataRow(23, 1, 0, "23:01:00")]
        [DataRow(0, 0, 0, "0:00:00")]
        [DataRow(0.0001, 0.2, 10, "0:00:22")]
        public void ToString_TimePeriod_Numbers(double hour, double minutes, double seconds, string expectedStringOut)
        {
            var p = new TimePeriod(hour, minutes, seconds);
            Assert.AreEqual(expectedStringOut, p.ToString());
        }

        [DataTestMethod, TestCategory("Equals_TimePeriod")]
        [DataRow(10, 20, 20, true)]
        [DataRow(10, 20, 20, true)]
        [DataRow(10, 0, 02, true)]
        [DataRow(10, 00, 2, true)]
        [DataRow(01, 59, 00, true)]
        [DataRow(1, 59, 0, true)]
        [DataRow(100000, 52329, 1, true)]

        public void Are_Equals_TimePeriod(double hours, double minutes, double seconds, bool equals)
        {
            var t = new TimePeriod(hours, minutes, seconds);
            var t1 = new TimePeriod(hours, minutes, seconds);
            Assert.AreEqual(t.Equals(t1), equals);
            Assert.AreEqual(t == t1, equals);
        }

        [DataTestMethod, TestCategory("Equals_TimePeriod")]
        [DataRow(10, 22, 20, true)]
        [DataRow(125, 250, 2230, true)]
        public void Are_Equals_TheSameObj(double hours, double minutes, double seconds, bool equals)
        {
            var t = new TimePeriod(hours, minutes, seconds);
            Assert.AreEqual(t.Equals(t), equals);
        }

        [DataTestMethod, TestCategory("Equals_TimePeriod")]
        [DataRow(10, 20, 20, "10:20:20")]
        [DataRow(01, 2, 2, "01:02:02")]
        [DataRow(10, 20, 20, "10:21:20", false)]
        [DataRow(0, 0, 0, "")]
        [DataRow(00, 20, 20, "0:20:20")]
        [DataRow(23, 59, 59, "23:59:59")]
        [DataRow(0, 0, 0, "       ")]
        [DataRow(01, 20, 0, "01:20")]
        [DataRow(10, 20, 0, "10:20")]
        [DataRow(0, 20, 20, " 0:20:20")]
        [DataRow(00, 20, 20, " 01:21:23", false)]
        [DataRow(001, 20, 20, " 01:21:23", false)]
        [DataRow(001, 20, 20, " 01:20:20")]
        [DataRow(001, 00020, 00020, " 01:20:20")]
        [DataRow(0000000, 0000000, 0000000, " 0:0:0")]

        public void Are_Equals_Int_TimePeriod_To_String(double hours, double minutes, double seconds, string time, bool equals = true)
        {
            var t = new TimePeriod(hours, minutes, seconds);
            var t1 = new TimePeriod(time);
            Assert.AreEqual(t.Equals(t1), equals);
            Assert.AreEqual(t == t1, equals);
        }

        [DataTestMethod, TestCategory("Equals_TimePeriod")]
        [DataRow("10:20:20", "10:20:20")]
        [DataRow("1:2:2", "01:02:02")]
        [DataRow("21:20:20", "10:21:20", false)]
        [DataRow("   ", "")]
        [DataRow("00:20:20", "0:20:20")]
        [DataRow("23:59:59", "23:59:59")]
        [DataRow("0", "       ")]
        [DataRow("0:0", "00:0:00")]
        [DataRow("02", "02:0:0")]
        public void Are_Equals_TimePeriod_String_Time_To_String(string time, string time1, bool equals = true)
        {
            var t = new TimePeriod(time);
            var t1 = new TimePeriod(time1);
            Assert.AreEqual(t.Equals(t1), equals);
            Assert.AreEqual(t == t1, equals);
        }

        [DataTestMethod, TestCategory("Equals_TimePeriod")]
        [DataRow(10, 20, 20, "10:20:20", false)]
        [DataRow(01, 2, 2, "01:02:02", false)]
        [DataRow(10, 20, 20, "10:21:20")]
        [DataRow(0, 0, 0, "", false)]
        [DataRow(00, 20, 20, "0:20:20", false)]
        [DataRow(23, 59, 59, "23:59:59", false)]
        [DataRow(0, 0, 0, "       ", false)]
        public void Are_Not_Equals_Int_TimePeriod_To_String(double hours, double minutes, double seconds, string time, bool equals = true)
        {
            var t = new TimePeriod(hours, minutes, seconds);
            var t1 = new TimePeriod(time);
            Assert.AreEqual(t != t1, equals);
        }

        [DataTestMethod, TestCategory("Greter_TimePeriod")]
        [DataRow(10, 00, 00, 9, 59, 59)]
        [DataRow(23, 59, 59, 0, 0, 0)]
        [DataRow(01, 02, 2, 0, 59, 59)]
        [DataRow(01, 00, 0, 0, 00, 000000000)]
        [DataRow(12, 22, 22, 12, 22, 22, false)]
        [DataRow(0000023, 00023, 00023, 0001, 002, 0003)]
        [DataRow(0.1, 00, 0, 0, 00, 000000000)]
        [DataRow(0.0001, 00, 0, 0, 00, 000000000, false)]
        [DataRow(0.0201, 00, 0, 0, 00, 10100000000, false)]

        public void Is_Greter_TimePeriod(double hour, double minutes, double seconds, double otherHour, double otherMinutes, double otherSeconds, bool grater = true)
        {
            var t = new TimePeriod(hour, minutes, seconds);
            var t1 = new TimePeriod(otherHour, otherMinutes, otherSeconds);
            Assert.AreEqual(t > t1, grater);
        }

        [DataTestMethod, TestCategory("Greter_TimePeriod")]
        [DataRow(10, 00, 00, "9")]
        [DataRow(23, 50, 00, "   ")]
        [DataRow(12, 20, 30, "10:30:20")]
        [DataRow(20, 00012, 00556, "000:32:36")]
        [DataRow(0, 00, 00, "0:00:00", false)]

        public void Is_Greter_TimePeriod_String(double hour, double minutes, double seconds, string time, bool grater = true)
        {
            var t = new TimePeriod(hour, minutes, seconds);
            var t1 = new TimePeriod(time);
            Assert.AreEqual(t > t1, grater);
        }

        [DataTestMethod, TestCategory("Smaller_TimePeriod")]
        [DataRow(10, 00, 00, "11:00:00")]
        [DataRow(23, 50, 00, "   ", false)]
        [DataRow(12, 20, 30, "23:59:00")]
        [DataRow(0.0201, 00, 0, "0:00:10100000000")]
        public void Is_smaller_TimePeriod(double hour, double minutes, double seconds, string time, bool grater = true)
        {
            var t = new TimePeriod(hour, minutes, seconds);
            var t1 = new TimePeriod(time);
            Assert.AreEqual(t < t1, grater);
        }

        [DataTestMethod, TestCategory("Greter_Or_Equal_TimePeriod")]
        [DataRow(10, 00, 00, "11:00:00", false)]
        [DataRow(23, 50, 00, "   ")]
        [DataRow(12, 20, 30, "12:20:30")]
        [DataRow(0, 0, 0, "  ")]
        public void Is_greter_or_EqualsPeriod(double hour, double minutes, double seconds, string time, bool grater = true)
        {
            var t = new TimePeriod(hour, minutes, seconds);
            var t1 = new TimePeriod(time);
            Assert.AreEqual(t >= t1, grater);
        }

        [DataTestMethod, TestCategory("Smaller_Or_Equal_TimePeriod")]
        [DataRow(10, 00, 00, "11:00:00")]
        [DataRow(23, 50, 00, "   ", false)]
        [DataRow(12, 20, 30, "12:20:30")]
        [DataRow(0, 0, 0, "  ")]
        public void Is_Smaleer_or_EqualsPeriod(double hour, double minutes, double seconds, string time, bool grater = true)
        {
            var t = new TimePeriod(hour, minutes, seconds);
            var t1 = new TimePeriod(time);
            Assert.AreEqual(t <= t1, grater);
        }

        [DataTestMethod, TestCategory("TimePeriod_Addition")]
        [DataRow(10, 00, 00, "11:00:00", "21:00:00")]
        [DataRow(23, 50, 00, "   ", "23:50:00")]
        [DataRow(12, 20, 30, "8:49:0", "21:09:30")]
        [DataRow(00, 40, 59, "2:29:2", "3:10:01")]
        public void TimePeriod_Operator_Addition(double hour, double minutes, double seconds, string time, string sum)
        {
            var t = new TimePeriod(hour, minutes, seconds);
            var t1 = new TimePeriod(time);
            var t2 = new TimePeriod(sum);
            Assert.AreEqual(t + t1, t2);
        }

        [DataTestMethod, TestCategory("TimePeriod_Addition_Seconds")]
        [DataRow(10, 00, 00, 10, "10:00:10")]
        [DataRow(201, 00, 00, 60, "201:01:00")]
        [DataRow(00, 00, 00, 0.1, "00:00:00")]
        [DataRow(00, 00, 00, 0.9, "00:00:00")]
        public void TimePeriod_Operator_Addition_Seconds(double hour, double minutes, double seconds, double secondsToAdd, string sum)
        {
            var t = new TimePeriod(hour, minutes, seconds);
            var t2 = new TimePeriod(sum);
            Assert.AreEqual(t + secondsToAdd, t2);
        }

        [DataTestMethod, TestCategory("TimePeriod_Substraction")]
        [DataRow(10, 00, 00, "11:00:00", "-1:00:00")]
        [DataRow(10, 00, 00, "23:00:00", "-13:00:00")]
        [DataRow(0, 00, 00, "11:00:00", "-11:00:00")]
        [DataRow(1000, 00, 00, "11:00:00", "989:00:00")]
        [DataRow(0.3, 00, 00, "1:00:00", "0:-42:00")]
        public void TimePeriod_Operator_Minus(double hour, double minutes, double seconds, string time, string sub)
        {
            var t = new TimePeriod(hour, minutes, seconds);
            var t1 = new TimePeriod(time);
            var t2 = new TimePeriod(sub);
            Assert.AreEqual(t - t1, t2);
        }

        [DataTestMethod, TestCategory("TimePeriod_Substraction")]
        [DataRow(10, 00, 00, 50, "9:59:10")]
        [DataRow(25, 23, 100, 50, "25:23:50")]
        [DataRow(0, 0, 100, 50, "0:0:50")]
        [DataRow(0.3, 0, 0, 50, "0:17:10")]
        public void TimePeriod_Operator_Substraction_Seconds(double hour, double minutes, double seconds, double secondsToSubstraction, string sum)
        {
            var t = new TimePeriod(hour, minutes, seconds);
            var t2 = new TimePeriod(sum);
            Assert.AreEqual(t - secondsToSubstraction, t2);
        }

        [DataTestMethod, TestCategory("TimePeriod_Multipler")]
        [DataRow(10, 00, 00, 3, "30:00:00")]
        [DataRow(25, 23, 100, 2, "50:49:20")]
        [DataRow(0, 0, 100, 1.1, "0:01:50")]
        [DataRow(0.3, 0, 0, 2, "0:36:0")]
        public void TimePeriod_Operator_Multipler(double hour, double minutes, double seconds, double multipler, string sum)
        {
            var t = new TimePeriod(hour, minutes, seconds);
            var t2 = new TimePeriod(sum);
            Assert.AreEqual(t * multipler, t2);
        }

        [DataTestMethod, TestCategory("TimePeriod_Addition")]
        [DataRow(10, 00, 00, "11:00:00")]
        [DataRow(23, 50, 00, "   ")]
        [DataRow(12, 20, 30, "8:49:0")]
        [DataRow(00, 40, 59, "2:29:2")]
        public void TimePeriod_Plus_Operator_Addition(double hour, double minutes, double seconds, string time)
        {
            var t = new TimePeriod(hour, minutes, seconds);
            var t1 = new TimePeriod(time);
            var t2 = t.Plus(t1);
            Assert.AreEqual(t + t1, t2);
        }

        [DataTestMethod, TestCategory("TimePeriod_Addition")]
        [DataRow(10, 00, 00, "11:00:00")]
        [DataRow(23, 50, 00, "   ")]
        [DataRow(12, 20, 30, "8:49:0")]
        [DataRow(00, 40, 59, "2:29:2")]
        public void TimePeriod_Plus_Addition_For_TimePeriod_Params(double hour, double minutes, double seconds, string time)
        {
            var t = new TimePeriod(hour, minutes, seconds);
            var t1 = new TimePeriod(time);
            var t2 = TimePeriod.Plus(t, t1);
            Assert.AreEqual(t + t1, t2);
        }

        [DataTestMethod, TestCategory("TimePeriod_Addition")]
        [DataRow(10, 00, 00, 100)]
        [DataRow(23, 50, 00, 2000)]
        [DataRow(12, 20, 30, 0)]
        [DataRow(00, 40, 59, 56.4)]
        [DataRow(00, 40, 59, 0.001)]
        [DataRow(00, 00010, 59, 0.001)]


        public void TimePeriod_Plus_Addition_For_Seconds_Param(double hour, double minutes, double seconds, double secondsToAdd)
        {
            var t = new TimePeriod(hour, minutes, seconds);
            var t2 = TimePeriod.PlusSeconds(t, secondsToAdd);
            Assert.AreEqual(t + secondsToAdd, t2);
        }

        [DataTestMethod, TestCategory("TimePeriod_Addition")]
        [DataRow(10, 00, 00, 100)]
        [DataRow(23, 50, 00, 2000)]
        [DataRow(12, 20, 30, 0)]
        [DataRow(00, 40, 59, 56.4)]
        [DataRow(00, 40, 59, 0.001)]
        [DataRow(00, 00010, 59, 0.001)]

        public void TimePeriod_Plus_Addition_For_Minutes_Param(double hour, double minutes, double seconds, double minutesToAdd)
        {
            var t = new TimePeriod(hour, minutes, seconds);
            var t2 = TimePeriod.PlusMinutes(t, minutesToAdd);
            Assert.AreEqual(t + minutesToAdd * 60, t2);
        }

        [DataTestMethod, TestCategory("TimePeriod_Addition")]
        [DataRow(10, 00, 00, 100)]
        [DataRow(23, 50, 00, 2000)]
        [DataRow(12, 20, 30, 0)]
        [DataRow(00, 40, 59, 56.4)]
        [DataRow(00, 40, 59, 0.001)]
        [DataRow(00, 00010, -59, 0.001)]
        [DataRow(-123, 00010, 32, -3232)]
        public void TimePeriod_Plus_Addition_For_Hours_Param(double hour, double minutes, double seconds, double hoursToAdd)
        {
            var t = new TimePeriod(hour, minutes, seconds);
            var t2 = TimePeriod.PlusHours(t, hoursToAdd);
            Assert.AreEqual(t + hoursToAdd * 3600, t2);
        }

        [DataTestMethod, TestCategory("TimePeriod_Substraction")]
        [DataRow(10, 00, 00, "11:00:00")]
        [DataRow(23, 50, 00, "   ")]
        [DataRow(12, 20, 30, "8:49:0")]
        [DataRow(00, 40, 59, "2:29:2")]
        [DataRow(00, 40, 59, "0:50:100")]
        [DataRow(00, -40, -59, "0:50:100")]
        public void TimePeriod_Minus(double hour, double minutes, double seconds, string time)
        {
            var t = new TimePeriod(hour, minutes, seconds);
            var t1 = new TimePeriod(time);
            var t2 = t.Minus(t1);
            Assert.AreEqual(t - t1, t2);
        }
    }
}
