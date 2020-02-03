using System;
using System.Threading.Tasks;
using Booking.Commands;
using Booking.Events;
using NServiceBus.Testing;
using NUnit.Framework;

[TestFixture]
public class WhenRebookingWasProposedTests
{
    GracePeriodForAcceptingRebookings saga;
    TestableMessageHandlerContext context;
    RebookingWasProposed rebookingWasProposed;
    const string bookingReferenceId = "XYZ123";

    [SetUp]
    public void Setup()
    {
        saga = new GracePeriodForAcceptingRebookings
        {
            Data = new GracePeriodForAcceptingRebookingsData()
        };
        context = new TestableMessageHandlerContext();

        rebookingWasProposed = new RebookingWasProposed(
            bookingReferenceId,
            "Aircraft type was changed from Boeing 787 to Boeing 777");
    }

    //TODO: Add unit tests
}