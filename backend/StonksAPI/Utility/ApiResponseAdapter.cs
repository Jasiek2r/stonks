namespace StonksAPI.Utility
{
    public class ApiResponseAdapter
    {
        private ApiResponse _apiResponse;
        private ApiSeries _apiSeries;
        public ApiResponseAdapter(ApiResponse apiResponse) { 
            _apiResponse = apiResponse;
            _apiSeries = new ApiSeries();
        }

        private void tryLoadTimeSeries(Dictionary<string, Quote> fieldContent)
        {
            if (fieldContent != null)
            {
                _apiSeries.TimeSeries = fieldContent;
            }
        }


        public ApiSeries extractSeries()
        {
            /*
             * Adapting the ApiResponse format to internal ApiSeries JSON format
             */
            tryLoadTimeSeries(_apiResponse.TimeSeriesWeekly);
            tryLoadTimeSeries(_apiResponse.TimeSeriesDaily);
            tryLoadTimeSeries(_apiResponse.TimeSeriesHourly);
            tryLoadTimeSeries(_apiResponse.TimeSeries30min);
            tryLoadTimeSeries(_apiResponse.TimeSeries15min);
            tryLoadTimeSeries(_apiResponse.TimeSeries5min);

            return _apiSeries;
        }

    }
}
