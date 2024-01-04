using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;

using FunctionApp1.TripBooking.Activities.Hotel;
using TripBookingApp.TripBooking.Activities.Hotel;

namespace FunctionApp1.TripBooking.Sagas
{
    public class TripBookingSaga
    {
        [Function(nameof(TripBookingSaga))]
        public async Task RunOrchestrator([OrchestrationTrigger] TaskOrchestrationContext context)
        {
            ILogger logger = context.CreateReplaySafeLogger(nameof(TripBookingSaga));
            logger.LogInformation("Start booking the trip");

            // Book the hotel using a person passport number
            var bookHotelRequest = new BookHotelRequest { PassPortNumber = "12212-A2" };
            var bookHotelResponse = await context.CallActivityAsync<BookHotelResponse>(nameof(BookHotelActivity.BookHotel), bookHotelRequest);
         


        }
    }
}
