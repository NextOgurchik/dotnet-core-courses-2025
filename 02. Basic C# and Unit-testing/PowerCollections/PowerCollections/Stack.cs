using System;
using System.Collections.Generic;

namespace PowerCollections
{
    internal interface IFindable<T>
    {
        public T[] Find(Func<T, bool> predicate);
    }
    internal class Stack<T>: IFindable<T>
    {
        private readonly T[] array;
        public int Count { get; private set; }
        public int Capacity { get; private set; }
        public Stack(int capacity)
        {
            Capacity = capacity;
            array = new T[capacity];
        }
        public void Push(T item)
        {
            if (Count > Capacity-1)
            {
                throw new InvalidOperationException($"Stack is full. Cannot push '{item}'. Current capacity is {Capacity}.");
            }
            array[Count++] = item;
        }
        public T Pop()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Cannot perform Pop operation on an empty stack.");
            }
            Count--;
            return array[Count];
        }
        public T Top()
        {
            if(Count == 0)
            {
                throw new InvalidOperationException("Cannot perform Top operation on an empty stack.");
            }
            return array[Count - 1];
        }
        public T[] ToArray()
        {
            T[] array = new T[Count];
            int j = 0;
            for (int i = Count - 1; i >= 0; i--)
            {
                array[i] = this.array[j];
                j++;
            }
            return array;
        }
        public T[] Find(Func<T,bool> predicate)
        {
            var foundItems = new List<T>();
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");
            }
            for (int i = Count - 1; i >= 0; i--)
            {
                if (predicate(array[i]))
                {
                    foundItems.Add(array[i]);
                }
            }
            return foundItems.ToArray();
        }
    }
}
