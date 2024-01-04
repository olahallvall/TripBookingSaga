using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace TripBookingSaga.Activities.Hotel
{
    public class BookHotelActivity
    {
        private readonly ILogger _logger;

        public BookHotelActivity(ILogger logger)
        {
            _logger = logger;
        }

        [Function(nameof(BookHotel))]
        public async Task<BookHotelResponse> BookHotel([ActivityTrigger] BookHotelRequest bookHotelRequest, FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger("BookHotelActivity");
            logger.LogInformation("Trying to book hotel for passportnumber {PassportNumber}.", bookHotelRequest.PassportNumber);

            var response = new BookHotelResponse();
            response.HotelBookingId = 111;

            await Task.Delay(1000);

            return response;
        }
    }
}
