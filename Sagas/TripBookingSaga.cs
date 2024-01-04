using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;
using Microsoft.Extensions.Logging;

using TripBookingSaga.Activities.Hotel;
using TripBookingSaga.Activities.Car;
using TripBookingSaga.Activities.Flight;

namespace TripBookingSaga.Sagas
{
    public class TripBookingSaga
    {
        [Function(nameof(TripBookingSaga))]
        public async Task RunOrchestrator([OrchestrationTrigger] TaskOrchestrationContext context)
        {
            ILogger logger = context.CreateReplaySafeLogger(nameof(TripBookingSaga));
            logger.LogInformation("Start booking the trip using a Saga (Orchestration)");

            // Book the hotel using a persons passport number
            var bookHotelRequest = new BookHotelRequest { PassPortNumber = "12212-A2" };
            var bookHotelResponse = await context.CallActivityAsync<BookHotelResponse>(nameof(BookHotelActivity.BookHotel), bookHotelRequest);

            // Book the car using a persons passport number
            var bookCarRequest = new BookCarRequest { PassPortNumber = "12212-A2" };
            var bookCarResponse = await context.CallActivityAsync<BookCarResponse>(nameof(BookHotelActivity.BookHotel), bookHotelRequest);


        }
    }
}
