using System;

namespace TestTask
{
    public class Timestamp
    {
        private string strValue; // Дата
        private Int32 unix; // Дата в unix

        public string StrValue
        {
            get { return strValue; }
            set { strValue = value; }
        }

        public Int32 Unix
        {
            get { return unix; }
            set { unix = value; }
        }

        public void SetUnix()
        {
            unix = (Int32)(DateTime.Now.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public void SetUnix(string timestamp)
        {
            unix = (Int32)(DateTime.Parse(timestamp).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public void SetTime()
        {
            strValue =  DateTime.Now.ToString("yyyy-MM-dd");
        }

    }
}
