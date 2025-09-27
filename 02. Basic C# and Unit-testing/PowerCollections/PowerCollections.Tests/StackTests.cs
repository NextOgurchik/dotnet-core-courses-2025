using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerCollections;

namespace Wintellect.PowerCollections.Tests
{
    [TestClass]
    public class StackTests
    {
        [TestMethod]
        public void CreateAndGetCapacity()
        {
            Stack<int> stack1 = new(4);
            Assert.AreEqual(4, stack1.Capacity);
        }
        [TestMethod]
        public void CountOfElement()
        {
            Stack<int> stack1 = new(7);
            for (int i = 0; i < 5; i++)
            {
                stack1.Push(i);
            }
            Assert.AreEqual(5, stack1.Count);
        }
        [TestMethod]
        public void PushPopAndTop()
        {
            Stack<int> stack1 = new(8);
            stack1.Push(1);
            stack1.Push(2);
            stack1.Push(3);
            stack1.Pop();
            Assert.AreEqual(2, stack1.Top());
        }
        [TestMethod]
        public void LessThenZero()
        {
            Stack<int> stack1 = new(4);
            stack1.Push(0);
            stack1.Pop();
            Assert.ThrowsException<InvalidOperationException>(() => stack1.Pop());
        }
        [TestMethod]
        public void MoreThenCapacity()
        {
            Stack<int> stack1 = new(4);
            for (int i = 0; i < 4; i++)
            {
                stack1.Push(0);
            }
            Assert.ThrowsException<InvalidOperationException>(() => stack1.Push(0));
        }
        [TestMethod]
        public void EmptyTop()
        {
            Stack<int> stack1 = new(4);
            Assert.ThrowsException<InvalidOperationException>(() => stack1.Top());
        }
        [TestMethod]
        public void ConvertToArray()
        {
            Stack<int> stack1 = new(4);
            for (int i = 0; i < 4; i++)
            {
                stack1.Push(i + 0);
            }
            int[] array = { 3, 2, 1, 0 };
            CollectionAssert.AreEqual(array, stack1.ToArray());
        }
        [TestMethod]
        public void Find()
        {
            Stack<int> stack1 = new(6);
            for (int i = 0; i < 6; i++)
            {
                stack1.Push(i);
            }
            var arrayMoreThanTwo = stack1.Find((int x) => x > 2);
            CollectionAssert.AreEqual(new int[3] { 5, 4, 3 }, arrayMoreThanTwo);
        }
    }
}
