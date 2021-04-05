using System;

namespace PolytechHomeworks
{
    // Спиридонов Дмитрий, 3530903/00001
    public class LinkedList<T>
    {
        private struct Node<P>
        {
            public P Value;
            public int Next;
        }

        private Node<T>[] array;
        private int top;
        private int free;
        private readonly int size;

        public LinkedList(int size)
        {
            this.size = size;
            Init();
        }
        
        private void Init()
        {
            this.array = new Node<T>[size];
            this.top = -1;
            this.free = 0;
            Count = 0;

            for (int i = 0; i < size - 1; i++) 
                array[i].Next = i + 1;
            array[size - 1].Next = -1;
        }
        
        public T this[int index]
        {
            get
            {
                if (index > Count - 1) throw new IndexOutOfRangeException();
                
                int currentIndex = top;
                for (int i = 0; i < index; i++) 
                    currentIndex = array[currentIndex].Next;
                
                return this.array[currentIndex].Value;
            }
            set
            {
                if (index > Count - 1) throw new IndexOutOfRangeException();
                
                int currentIndex = top;
                for (int i = 0; i < index; i++) 
                    currentIndex = array[currentIndex].Next;
                
                this.array[currentIndex].Value = value;
            }
        }

        public bool Add(T item)
        {
            if (free == -1) return false;

            int nextFree = array[free].Next;
            
            array[free] = new Node<T> {
                Value = item,
                Next = -1
            };

            if (top == -1)
                top = free;
            else {
                int lastNodeIndex = top;
                while (array[lastNodeIndex].Next != -1) 
                    lastNodeIndex = array[lastNodeIndex].Next;
                array[lastNodeIndex].Next = free;
            }

            free = nextFree;
            Count++;

            return true;
        }

        public void Clear()
        {
            Init();
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public bool AddAt(int index, T item)
        {
            if (free == -1) return false;
            int currentIndex = top;
            for (int i = 0; i < index - 1; i++) 
                currentIndex = array[currentIndex].Next;
            
            int nextFree = array[free].Next;
            if (index != 0)
            {
                array[free].Next = array[currentIndex].Next;
                array[currentIndex].Next = free;
            }
            else
            {
                array[free].Next = top;
                top = free;
            }
            
            array[free].Value = item;
            free = nextFree;
            Count++;
            
            return true;
        }

        public bool RemoveAt(int index)
        {
            if (index > Count - 1) return false;
            int currentIndex = top;
            for (int i = 0; i < index - 1; i++) 
                currentIndex = array[currentIndex].Next;

            if (index != 0)
            {
                int newFree = array[currentIndex].Next;
                array[currentIndex].Next = array[array[currentIndex].Next].Next;
                array[newFree].Next = free;
                free = newFree;
            }
            else
            {
                top = array[currentIndex].Next;
                array[currentIndex].Next = free;
                free = currentIndex;
            }
            
            Count--;
            
            return true;
        }

        public bool Remove(T item)
        {
            if (Count == 0) return false;
            int currentIndex = top;
            while (array[currentIndex].Next != -1)
            {
                if (array[array[currentIndex].Next].Value.Equals(item))
                    break;
                
                currentIndex = array[currentIndex].Next;
            }

            int index = array[currentIndex].Next;
            if (index != -1)
            {
                array[index].Next = array[index + 1].Next;
                array[index + 1].Next = free;
                free = index + 1;
                Count--;
                return true;
            }
            else return false;
        }

        public int IndexOf(T item)
        {
            if (Count == 0) return -1;
            int currentIndex = top;
            while (array[currentIndex].Next != -1 && !array[currentIndex].Value.Equals(item))
                currentIndex = array[currentIndex].Next;

            return array[currentIndex].Value.Equals(item)
                ? currentIndex
                : -1;
        }

        public int Count { get; private set; }
    }
}