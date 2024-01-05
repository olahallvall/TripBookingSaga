using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using TripBookingSaga.Activities.Hotel;

namespace TripBookingSaga.Activities.Car
{
    public class BookCarActivity
    {
        private readonly ILogger _logger;

        public BookCarActivity(ILogger logger)
        {
            _logger = logger;
        }

        [Function(nameof(BookCar))]
        public async Task<BookCarResponse> BookCar([ActivityTrigger] BookCarRequest bookCarRequest, FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger("BookCarActivity");
            logger.LogInformation("Trying to book car for passportnumber {PassportNumber}.", bookCarRequest.PassportNumber);

            var response = new BookCarResponse();
            response.CarBookingId = 222;

            await Task.Delay(1000);

            //throw new Exception("No cars available"); 

            return response;
        }
    }
}
