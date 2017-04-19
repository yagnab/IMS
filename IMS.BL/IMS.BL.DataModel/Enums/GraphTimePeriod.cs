using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.BL.DataModel
{
    public enum GraphTimePeriod
    {
        PastHour,
        PastDay,
        PastWeek,
        PastYear
    }

    public static class GraphTimePeriodExtension
    {
        public static string StringRepresentation(this GraphTimePeriod timePeriod)
        {
            switch(timePeriod)
            {
                case GraphTimePeriod.PastDay:
                    return "Past Day";
                case GraphTimePeriod.PastHour:
                    return "Past Hour";
                case GraphTimePeriod.PastWeek:
                    return "Past Week";
                case GraphTimePeriod.PastYear:
                    return "Past Year";
                //some error occurs
                default:
                    return null;
            }
        }
    }
}
