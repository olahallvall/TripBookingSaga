using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using TripBookingSaga.Activities.Car;

namespace TripBookingSaga.Activities.Flight
{
    public class BookFlightActivity
    {
        private readonly ILogger _logger;

        public BookFlightActivity(ILogger logger)
        {
            _logger = logger;
        }

        [Function(nameof(BookFlight))]
        public async Task<BookFlightResponse> BookFlight([ActivityTrigger] BookFlightRequest bookFlightRequest, FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger("BookFlightActivity");
            logger.LogInformation("Trying to book flight for passportnumber {PassportNumber}.", bookFlightRequest.PassportNumber);

            var response = new BookFlightResponse();
            response.FlightBookingId = 333;

            await Task.Delay(1000);

            return response;
        }
    }
}
