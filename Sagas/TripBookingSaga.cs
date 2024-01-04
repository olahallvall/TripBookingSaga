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

            var passportNumber = "12212-A2";

            // Book the hotel using a persons passport number
            var bookHotelRequest = new BookHotelRequest { PassportNumber = passportNumber };
            var bookHotelResponse = await context.CallActivityAsync<BookHotelResponse>(nameof(BookHotelActivity.BookHotel), bookHotelRequest);
            logger.LogInformation("Booked a hotel with HotelBookingId {HotelBookingId}.", bookHotelResponse.HotelBookingId);


            // Book the car using a persons passport number
            var bookCarRequest = new BookCarRequest { PassportNumber = passportNumber };
            var bookCarResponse = await context.CallActivityAsync<BookCarResponse>(nameof(BookCarActivity.BookCar), bookCarRequest);
            logger.LogInformation("Booked a car with CarBookingId {CarBookingId}.", bookCarResponse.CarBookingId);


            // Book the flight using a persons passport number
            var bookFlightRequest = new BookFlightRequest { PassportNumber = passportNumber };
            var bookFlightResponse = await context.CallActivityAsync<BookFlightResponse>(nameof(BookFlightActivity.BookFlight), bookFlightRequest);
            logger.LogInformation("Booked a flight with FlightBookingId {FlightBookingId}.", bookFlightResponse.FlightBookingId);
        }
    }
}
