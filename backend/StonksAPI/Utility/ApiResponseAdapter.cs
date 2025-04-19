namespace StonksAPI.Utility
{
    public class ApiResponseAdapter
    {
        private readonly ApiResponse _apiResponse;
        private readonly ApiSeries _apiSeries;
        public ApiResponseAdapter(ApiResponse apiResponse) { 
            _apiResponse = apiResponse;
            _apiSeries = new ApiSeries();
        }

        private void TryLoadTimeSeries(Dictionary<string, Quote>? fieldContent)
        {
            if (fieldContent != null)
            {
                _apiSeries.TimeSeries = fieldContent;
            }
        }

        public ApiSeries ExtractSeries()
        {
            /*
             * Adapting the ApiResponse format to internal ApiSeries JSON format
             */
            TryLoadTimeSeries(_apiResponse.TimeSeriesWeekly);
            TryLoadTimeSeries(_apiResponse.TimeSeriesDaily);
            TryLoadTimeSeries(_apiResponse.TimeSeriesHourly);
            TryLoadTimeSeries(_apiResponse.TimeSeries30min);
            TryLoadTimeSeries(_apiResponse.TimeSeries15min);
            TryLoadTimeSeries(_apiResponse.TimeSeries5min);

            return _apiSeries;
        }

    }
}
