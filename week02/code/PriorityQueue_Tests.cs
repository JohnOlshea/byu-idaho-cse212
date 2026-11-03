using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue items with different priorities, dequeue should return highest priority first and respect FIFO for ties
    // Expected Result: Dequeue returns C (pri 3), then A (pri 2, first), then B (pri 2, second), then D (pri 1)
    // Defect(s) Found: Loop missed last element, >= broke FIFO, item never removed from queue
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 2);
        priorityQueue.Enqueue("B", 2);
        priorityQueue.Enqueue("C", 3);
        priorityQueue.Enqueue("D", 1);

        Assert.AreEqual("C", priorityQueue.Dequeue()); // highest
        Assert.AreEqual("A", priorityQueue.Dequeue()); // FIFO tie
        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("D", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Dequeue from empty queue
    // Expected Result: InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: None, but verifies requirement - previously passed message check
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Should throw");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }

    [TestMethod]
    // Scenario: Enqueue goes to back, verify removal and empty after
    // Expected Result: After enqueue 1 item and dequeue, queue empty and second dequeue throws
    // Defect(s) Found: Original code never removed item, so queue never became empty
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("X", 5);
        Assert.AreEqual("X", priorityQueue.Dequeue());
        Assert.AreEqual("[]", priorityQueue.ToString()); // Should be empty
    }
}