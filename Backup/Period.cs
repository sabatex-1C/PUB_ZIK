using System;
using System.Collections.Generic;
using System.Text;

namespace NTICS
{
    public struct Period
    {
        DateTime Date;
        public Period(DateTime Period)
        {
            Date = Period;
        }
        public DateTime ToDateTime()
        {
            return Date;
        }
        public static explicit operator DateTime(Period value)
        {
            return value.ToDateTime();
        }
        public DateTime Begin
        {
            get { return new DateTime(Date.Year, Date.Month, 1); }
        }
        public DateTime End
        {
            get { return new DateTime(Date.Year, Date.Month, DateTime.DaysInMonth(Date.Year, Date.Month)); }
        }


    }
}
