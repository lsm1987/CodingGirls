using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VSTestProject
{
    class ForeachTest
    {
        private const int SIZE = 16 * 1024 * 1024;
        private int[] array = new int[SIZE];
        private List<int> list = new List<int>(SIZE);

        public void Initialize()
        {
            for (int i = 0; i < SIZE; i++)
            {
                array[i] = 1;
            }

            for (int i = 0; i < SIZE; i++)
            {
                list.Add(1);
            }
        }

        public void DoForArray()
        {
            int x = 0;
            for (int i = 0; i < SIZE; i++)
            {
                x += array[i];
            }
        }

        public void DoForeachArray()
        {
            int x = 0;
            foreach (int val in array)
            {
                x += val;
            }
        }

        public void DoForList()
        {
            int x = 0;
            for (int i = 0; i < SIZE; i++)
            {
                x += list[i];
            }
        }

        public void DoForeachList()
        {
            int x = 0;
            foreach (int val in list)
            {
                x += val;
            }
        }
    }
}
