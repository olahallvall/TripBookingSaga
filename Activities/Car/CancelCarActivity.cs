using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace TripBookingSaga.Activities.Car
{
    public class CancelCarActivity
    {
        private readonly ILogger _logger;

        public CancelCarActivity(ILogger logger)
        {
            _logger = logger;
        }

        [Function(nameof(CancelCar))]
        public async Task<CancelCarResponse> CancelCar([ActivityTrigger] CancelCarRequest cancelCarRequest, FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger("CancelCarActivity");
            logger.LogInformation("Trying to cancel car booking for CarBookingId {CarBookingId}.", cancelCarRequest.CarBookingId);

            var response = new CancelCarResponse();
            
            await Task.Delay(1000);

            // throw new Exception("Can't cancel the car. The vendor returns an exception.");

            return response;
        }
    }
}
