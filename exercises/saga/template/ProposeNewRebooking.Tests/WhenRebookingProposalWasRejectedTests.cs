using System.Threading.Tasks;
using Booking.Events;
using NServiceBus.Testing;
using NUnit.Framework;

[TestFixture]
public class WhenRebookingProposalWasRejectedTests
{
    GracePeriodForAcceptingRebookings saga;
    TestableMessageHandlerContext context;
    RebookingProposalWasRejected proposedRebookingWasRejected;

    const string bookingReferenceId = "XYZ123";

    [SetUp]
    public void Setup()
    {
        saga = new GracePeriodForAcceptingRebookings
        {
            Data = new GracePeriodForAcceptingRebookingsData()
        };
        context = new TestableMessageHandlerContext();
        proposedRebookingWasRejected = new RebookingProposalWasRejected(bookingReferenceId);
    }

    // TODO: Add unit tests
}