using MGA.CrossCutting.Provider.Interface;
using System;

namespace MGA.CrossCutting.Provider
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetNow()
        {
            return DateTime.Now;
        }

        public DateTime GetUtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
