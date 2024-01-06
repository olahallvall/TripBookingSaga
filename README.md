# Trip booking Saga Example

The Saga pattern describes how to solve distributed (business) transactions without two-phase-commit as this does not scale in distributed systems. The basic idea is to break the overall transaction into multiple steps or activities. Only the steps internally can be performed in atomic transactions but the overall consistency is taken care of by the Saga. The Saga has the responsibility to either get the overall business transaction completed or to leave the system in a known termination state. So in case of errors a business rollback procedure is applied which occurs by calling compensation steps or activities in reverse order.

In the example hotel, car and flight booking might be done by different remote services. So there is not technical transaction, but a business transaction. When the flight booking cannot be carried out succesfully you need to cancel hotel and car. 

![Saga example](Docs/example-use-case.png)

# Get started

You need

* Visual Studio 2022 or later
* Azure Functions Core Tools (Azure Functions Core Tools lets you develop and test your functions on your local computer)
