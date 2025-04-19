namespace StonksAPI.Utility
{
    public class MapIntervalRoute
    {
        private static string? _intervalRoute;
        public static string? IntervalRoute { 
            get { return _intervalRoute; }
            set
            {
                if(value == null)
                {
                    throw new ArgumentNullException(nameof(value), "Interval cannot be null");
                }
                _intervalRoute = "TIME_SERIES_" + value.ToUpper();
            }
        }
    }
}
