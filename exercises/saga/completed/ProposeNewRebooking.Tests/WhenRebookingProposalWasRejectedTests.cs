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

    [Test]
    public async Task ShouldNotNotifyCustomers()
    {
        await saga.Handle(proposedRebookingWasRejected, context);

        var rebookingWasProposed = new RebookingWasProposed(
            bookingReferenceId,
            "Aircraft type was changed from Boeing 787 to Boeing 777");

        await saga.Handle(rebookingWasProposed, context);

        Assert.IsTrue(saga.Completed);
        Assert.AreEqual(0, context.SentMessages.Length);
        Assert.AreEqual(0, context.PublishedMessages.Length);
    }
}