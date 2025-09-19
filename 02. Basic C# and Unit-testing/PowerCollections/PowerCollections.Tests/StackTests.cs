using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
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
            Stack stack1 = new Stack(4);
            Assert.AreEqual(4, stack1.GetCapacity());
        }
        [TestMethod]
        public void CountOfElement()
        {
            Stack stack1 = new Stack(7);
            for (int i = 0; i < 7; i++)
            {
                stack1.Push(i);
            }
            Assert.AreEqual(7, stack1.GetCount());
        }
        [TestMethod]
        public void PushPopAndTop()
        {
            Stack stack1 = new Stack(8);
            stack1.Push(1);
            stack1.Push(2);
            stack1.Push(3);
            stack1.Pop();
            Assert.AreEqual(2, stack1.Top());
        }
        [TestMethod]
        public void ThrowOutOfRangeException()
        {
            Stack stack1 = new Stack(4);
            for (int i = 0; i < 4; i++)
            {
                stack1.Push(1);
            }
            Assert.ThrowsException<IndexOutOfRangeException>(() => stack1.Push(1));
        }
        [TestMethod]
        public void ConvertToArray()
        {
            Stack stack1 = new Stack(4);
            for (int i = 0; i < 4; i++)
            {
                stack1.Push(i);
            }
            int[] array = { 3, 2, 1, 0 };
            CollectionAssert.AreEqual(array, stack1.ToArray());
        }
    }
}
