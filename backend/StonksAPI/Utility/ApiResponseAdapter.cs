namespace StonksAPI.Utility
{
    public class ApiResponseAdapter
    {
        private ApiResponse _apiResponse;
        public ApiResponseAdapter(ApiResponse apiResponse) { 
            _apiResponse = apiResponse;
        }

        
        public ApiSeries extractSeries()
        {
            /*
             * Adapting the ApiResponse format to internal ApiSeries JSON format
             */
            ApiSeries apiSeries = new ApiSeries();

            if(_apiResponse.TimeSeriesDaily != null)
            {
                apiSeries.TimeSeries = _apiResponse.TimeSeriesDaily;
            }else if(_apiResponse.TimeSeriesWeekly != null)
            {
                apiSeries.TimeSeries = _apiResponse.TimeSeriesWeekly;
            }

            return apiSeries;
        }

    }
}
