namespace StonksAPI.Utility
{
    public class MapIntervalRoute
    {
        private static string _intervalRoute;
        public static string IntervalRoute { 
            get { return _intervalRoute; }
            set
            {
                _intervalRoute = "TIME_SERIES_" + value.ToUpper();
            }
        }
    }
}
