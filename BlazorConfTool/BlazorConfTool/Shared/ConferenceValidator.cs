using System;

namespace BlazorConfTool.Shared
{
    public class ConferenceValidator
    {
        public bool IsValidPeriod(DateTime start, DateTime end)
        {
            return DateTime.Compare(end, start) >= 0;
        }
    }
}
