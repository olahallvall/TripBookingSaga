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
            logger.LogInformation("Booked hotel for passportnumber {PassPortNumber}.", bookHotelRequest.PassPortNumber);

            var response = new BookHotelResponse();
            response.BookingNumber = 222;

            await Task.Delay(1000);

            return response;
        }
    }
}
