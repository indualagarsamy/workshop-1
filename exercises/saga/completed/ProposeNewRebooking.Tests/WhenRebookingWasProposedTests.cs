﻿using System;
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

    [Test]
    public async Task ShouldNotifyCustomersWhenRebookingWasProposed()
    {
        await saga.Handle(rebookingWasProposed, context);
        Assert.AreEqual(2, context.SentMessages.Length);
        Assert.AreEqual(1, context.TimeoutMessages.Length);

        var processMessage = (NotifyCustomerAboutProposedRebooking) context.SentMessages[0].Message;

        // also test how long the timeout was supposed to be.
        Assert.AreEqual(TimeSpan.FromSeconds(15), context.TimeoutMessages[0].Within);
        Assert.AreEqual("Aircraft type was changed from Boeing 787 to Boeing 777", processMessage.ReasonForChange);
    }

    [Test]
    public async Task ShouldAcceptTheRebookingAfterGracePeriodHasElapsed()
    {
        await saga.Handle(rebookingWasProposed, context);
        await saga.Timeout(new GracePeriodForAcceptingRebookings.CancellationGracePeriodElapsed(), context);

        Assert.IsTrue(saga.Completed);
        Assert.AreEqual(1, context.PublishedMessages.Length);
        var publishedMessage = (RebookingWasAccepted) context.PublishedMessages[0].Message;
        Assert.AreEqual(bookingReferenceId, publishedMessage.BookingReferenceId);
    }

    [Test]
    public async Task ShouldNotAcceptTheRebookingWhenRebookingProposalWasRejected()
    {
        await saga.Handle(rebookingWasProposed, context);

        var bookingWasCancelled = new RebookingProposalWasRejected(bookingReferenceId);

        await saga.Handle(bookingWasCancelled, context);

        Assert.IsTrue(saga.Completed);
        Assert.AreEqual(0, context.PublishedMessages.Length);
    }
}