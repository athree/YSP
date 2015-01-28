using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMserver.CommonFuncs
{
    //默认的元素为整型的队列
    public class CircularQueue
    {
        private Int32 count;
        private Int32 length;
        private Int32 font;
        private Int32 rear;

        private Int32[] elements;
        public CircularQueue()
        {
            elements = new Int32[16];
            length = 0;
            font = 0;
            rear = 0;
            count = 16;
        }

        public CircularQueue(Int32 c)
        {
            elements = new Int32[c];
            length = 0;
            font = 0;
            rear = 0;
            count = c;
        }

        public Int32 Lenght
        {
            get
            {
                return length;
            }
        }

        public Boolean EnQueue(Int32 i)
        {
            if (length == count)
                return false;

            if (length == 0)
            {
                elements[font] = i;
                length++;
            }
            else
            {
                rear++;
                length++;
                if (rear == count)
                    rear = 0;

                elements[rear] = i;
            }

            return true;
        }

        public Int32 DeQueue()
        {
            length--;
            Int32 i = elements[rear];
            rear--;
            if (rear == -1)
                rear = count - 1;

            return i;
        }

        public Boolean IsEmpty()
        {
            if (length == 0)
                return true;
            else
                return false;
        }

        public Boolean IsFull()
        {
            if (length == count)
                return true;
            else
                return false;
        }
    }
}
