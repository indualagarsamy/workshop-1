using NServiceBus.Logging;
using System;
using System.Threading.Tasks;
using Booking.Commands;
using Booking.Events;
using NServiceBus;

public class GracePeriodForAcceptingRebookings  :
Saga<GracePeriodForAcceptingRebookingsData>
// Define what messages start the saga and what timeout messages are being received
// Implement SagaData class to store the state
// Implement IAmStartedByMessages<T> for messages that start the saga
// Implement IHandleTimeouts<T> for to implement timeout 

{
    // Map the properties of the saga that uniquely identifies with the saga data property
    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<GracePeriodForAcceptingRebookingsData> mapper)
    {
        mapper.ConfigureMapping<RebookingWasProposed>(message => message.BookingReferenceId)
            .ToSaga(data => data.BookingReferenceId);

        mapper.ConfigureMapping<RebookingProposalWasRejected>(message => message.BookingReferenceId)
            .ToSaga(data => data.BookingReferenceId);
    }

    // Implement RebookingWasProposed handler and start a timeout

    // Implement RebookingProposalWasRejected event

    // Define and Implement Timeout message handler

    static ILog logger = LogManager.GetLogger<GracePeriodForAcceptingRebookings>();
    
}