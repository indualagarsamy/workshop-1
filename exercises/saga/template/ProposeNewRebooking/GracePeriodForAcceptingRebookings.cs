using NServiceBus.Logging;
using System;
using System.Threading.Tasks;
using Booking.Commands;
using Booking.Events;
using NServiceBus;

public class GracePeriodForAcceptingRebookings  :
Saga<GracePeriodForAcceptingRebookingsData>
// SagaData class has already been defined to store the state. See GracePeriodForAcceptingRebookingsData
// TODO: Implement IAmStartedByMessages<T> for messages that start the saga
// TODO: Implement IHandleTimeouts<T> to implement any timeout messages

{
    // Map the properties of the saga that uniquely identifies with the saga data property
    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<GracePeriodForAcceptingRebookingsData> mapper)
    {
        mapper.ConfigureMapping<RebookingWasProposed>(message => message.BookingReferenceId)
            .ToSaga(data => data.BookingReferenceId);

        mapper.ConfigureMapping<RebookingProposalWasRejected>(message => message.BookingReferenceId)
            .ToSaga(data => data.BookingReferenceId);
    }

    // TODO: Implement RebookingWasProposed handler and start a timeout

    // TODO: Implement RebookingProposalWasRejected event

    // TODO: Define and Implement Timeout message handler

    static ILog logger = LogManager.GetLogger<GracePeriodForAcceptingRebookings>();

}