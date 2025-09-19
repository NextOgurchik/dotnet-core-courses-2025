using System;
using System.Collections.Generic;
using System.Text;

namespace PowerCollections
{
    internal class Stack
    {
        private int[] array;
        private int count;
        private int capacity;
        public Stack(int capacity)
        {
            this.capacity = capacity;
            array = new int[capacity];
        }
        public void Push(int item)
        {
            array[count++] = item;
        }
        public int Pop() 
        {
            count--;
            return array[count];
        }
        public int Top()
        {
            return array[count-1];
        }
        public int[] ToArray()
        {
            int[] array = new int[count];
            int j = 0;
            for (int i = count-1; i >= 0; i--)
            {
                array[i] = this.array[j];
                j++;
            }
            return array;
        }
        public int GetCount()
        {
            return count;
        }
        public int GetCapacity()
        {
            return capacity;
        }
    }
}
