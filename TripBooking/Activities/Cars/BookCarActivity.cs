using Microsoft.Azure.Functions.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FunctionApp1.TripBooking.Activities.Cars
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
