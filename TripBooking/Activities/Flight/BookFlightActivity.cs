using Microsoft.Azure.Functions.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace FunctionApp1.TripBooking.Activities.Flight
{
    public class BookFlightActivity
    {
        private readonly ILogger _logger;

        public BookFlightActivity(ILogger logger)
        {
            _logger = logger;
        }

        [Function(nameof(BookFlight))]
        public static string BookFlight([ActivityTrigger] string name, FunctionContext executionContext)
        {
            ILogger logger = executionContext.GetLogger("BookFlightActivity");
            logger.LogInformation("Booked hotel {name}.", name);
            return $"Booked hotel {name}!";
        }
    }
}
