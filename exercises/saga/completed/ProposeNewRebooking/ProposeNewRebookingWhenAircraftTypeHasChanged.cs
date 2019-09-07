using NServiceBus.Logging;
using FlightPlanning.Events;
using Booking.Commands;
using NServiceBus;
using System.Threading.Tasks;
using Booking.Events;

public class ProposeNewRebookingWhenAircraftTypeHasChanged :
    IHandleMessages<AircraftTypeHasChanged>,
    IHandleMessages<ProposeRebooking>
{
    public async Task Handle(AircraftTypeHasChanged message, IMessageHandlerContext context)
    {
        // Find all the relevant booking references

        // Propose to rebook the flight for each of the bookings found.
        // foreach (affected flight in the list of bookings)
        //{

        // Get the groupings . for each group send a message
        
        var cmd = new ProposeRebooking(bookingReferenceId: "QAZ123",
            customerId: "1",
            reasonForRebooking: $"Aircraft type was changed from {message.OldAircraftTypeId} to {message.NewAircraftTypeId} on flight {message.FlightId}");

        logger.Info("Received AircraftTypeHasChanged event, sending ProposeRebooking command");
        await context.SendLocal(cmd).ConfigureAwait(false);
        //}
    }

    public Task Handle(ProposeRebooking message, IMessageHandlerContext context)
    {
        // call rules engine to prioritize seating arrangement

        // Find a new better seating arrangement and once you find a good itinerary, publish event

        logger.Info("Received ProposeRebooking command, publishing RebookingWasProposed event");
        return context.Publish(
            new RebookingWasProposed(
                message.BookingReferenceId,
                message.ReasonForRebooking));
    }

    static ILog logger = LogManager.GetLogger<ProposeNewRebookingWhenAircraftTypeHasChanged>();
}