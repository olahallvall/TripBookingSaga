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
            var activityCompensationsList = new List<ActivityCompensation>();

            try 
            {
                // Book the hotel using a persons passport number
                var bookHotelRequest = new BookHotelRequest { PassportNumber = passportNumber };
                var bookHotelResponse = await context.CallActivityAsync<BookHotelResponse>(nameof(BookHotelActivity.BookHotel), bookHotelRequest);
                activityCompensationsList.Add(new ActivityCompensation { ActivityToCompensate = nameof(BookHotelActivity.BookHotel), CompensationKey = bookHotelResponse.HotelBookingId });
                logger.LogInformation("Booked a hotel with HotelBookingId {HotelBookingId}.", bookHotelResponse.HotelBookingId);
             

                // Book the car using a persons passport number
                var bookCarRequest = new BookCarRequest { PassportNumber = passportNumber };
                var bookCarResponse = await context.CallActivityAsync<BookCarResponse>(nameof(BookCarActivity.BookCar), bookCarRequest);
                activityCompensationsList.Add(new ActivityCompensation { ActivityToCompensate = nameof(BookCarActivity.BookCar), CompensationKey = bookCarResponse.CarBookingId });
                logger.LogInformation("Booked a car with CarBookingId {CarBookingId}.", bookCarResponse.CarBookingId);


                // Book the flight using a persons passport number - this activity will throw an exception.
                var bookFlightRequest = new BookFlightRequest { PassportNumber = passportNumber };
                var bookFlightResponse = await context.CallActivityAsync<BookFlightResponse>(nameof(BookFlightActivity.BookFlight), bookFlightRequest);
                activityCompensationsList.Add(new ActivityCompensation { ActivityToCompensate = nameof(BookFlightActivity.BookFlight), CompensationKey = bookFlightResponse.FlightBookingId });
                logger.LogInformation("Booked a flight with FlightBookingId {FlightBookingId}.", bookFlightResponse.FlightBookingId);

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                // Something has gone wrong booking the trip. To restore the system in an known termination state we now will try to compensate.
                // Here we can use Caitie McAffery defintion - Semantic rollback. The rollback will not works exactly the same way as an two-phase commt rollback where changed data are restored in tables. 
                //
                // The code below is very explicit and configurable. You can absolutely put more engineering skills in it 

                // Something can go wrong compensating!!! We need to handle this in a Try-Catch manner.
                // The same here - the code below is very explicit and configurable. You can absolutely put more engineering skills in it
                try
                {
                    foreach (ActivityCompensation activityCompensation in activityCompensationsList)
                    {
                        switch (activityCompensation.ActivityToCompensate)
                        {
                            case nameof(BookHotelActivity.BookHotel):
                                {
                                    // Cancel the hotel using the HotelBookingId 
                                    var cancelHotelRequest = new CancelHotelRequest { HotelBookingId = activityCompensation.CompensationKey };
                                    var cancelHotelResponse = await context.CallActivityAsync<CancelHotelResponse>(nameof(CancelHotelActivity.CancelHotel), cancelHotelRequest);
                                }
                                break;

                            case nameof(BookCarActivity.BookCar):
                                {
                                    // Cancel the car using the CarBookingId 
                                    var cancelCarRequest = new CancelCarRequest { CarBookingId = activityCompensation.CompensationKey };
                                    var cancelCarResponse = await context.CallActivityAsync<CancelCarResponse>(nameof(CancelCarActivity.CancelCar), cancelCarRequest);
                                }
                                break;

                            case nameof(BookFlightActivity.BookFlight):
                                {
                                    // Cancel the hotel using the FlightBookingId 
                                    var cancelFlightRequest = new CancelFlightRequest { FlightBookingId = activityCompensation.CompensationKey };
                                    var cancelFlightResponse = await context.CallActivityAsync<CancelFlightResponse>(nameof(CancelFlightActivity.CancelFlight), cancelFlightRequest);
                                }
                                break;
                        }
                    }
                } 
                catch (Exception exceptionWhenCompensating)
                {
                    logger.LogError(exceptionWhenCompensating.Message);
                    // What to do here??? Probably we have to manage this manually. Should we send an email perhaps???
                    // In which Status should we set this orchestrations instance?
                    // Rethrowing the exception or set the this orchestrations status to Complete?
                    // 
                    // This questions you have to answer based on your specific use case and organization.
                }
            }
        }
    }
}
