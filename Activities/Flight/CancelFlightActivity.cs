using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace TripBookingSaga.Activities.Flight
{
    public class CancelFlightActivity
    {
        private readonly ILogger _logger;

        public CancelFlightActivity(ILogger logger)
        {
            _logger = logger;
        }

        [Function(nameof(CancelFlight))]
        public async Task<CancelFlightResponse> CancelFlight([ActivityTrigger] CancelFlightRequest cancelFlightRequest, FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger("CancelFlightActivity");
            logger.LogInformation("Trying to cancel Flight booking for FlightBookingId {FlightBookingId}.", cancelFlightRequest.FlightBookingId);

            var response = new CancelFlightResponse();
            
            await Task.Delay(1000);

            return response;
        }
    }
}
