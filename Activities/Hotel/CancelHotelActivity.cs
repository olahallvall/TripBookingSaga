using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace TripBookingSaga.Activities.Hotel
{
    public class CancelHotelActivity
    {
        private readonly ILogger _logger;

        public CancelHotelActivity(ILogger logger)
        {
            _logger = logger;
        }

        [Function(nameof(CancelHotel))]
        public async Task<CancelHotelResponse> CancelHotel([ActivityTrigger] CancelHotelRequest cancelHotelRequest, FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger("CancelHotelActivity");
            logger.LogInformation("Trying to cancel hotel booking for HotelBookingId {HotelBookingId}.", cancelHotelRequest.HotelBookingId);

            var response = new CancelHotelResponse();
            
            await Task.Delay(1000);

            return response;
        }
    }
}
