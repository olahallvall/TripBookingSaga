using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

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
        public static string BookCar([ActivityTrigger] string name, FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger("BookCarActivity");
            logger.LogInformation("Booked hotel {name}.", name);
            return $"Booked hotel {name}!";
        }
    }
}
