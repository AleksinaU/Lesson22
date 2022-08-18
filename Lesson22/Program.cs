using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson22
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите размерность массива");
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Action<Task<int[]>> action1 = new Action<Task<int[]>>(GetSum);
            Task task2 = task1.ContinueWith(action1);

            Action<Task<int[]>> action2 = new Action<Task<int[]>>(GetMax);
            Task task3 = task1.ContinueWith(action2);

            task1.Start();
            Console.ReadKey();
        }
        static int[] GetArray(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 100);
            }
            return array;
            
        }
        static void GetSum(Task<int[]> task)
        {
            int[] array = task.Result;
            int s = 0;
            for (int i = 0; i < array.Count(); i++)
            { 
                s += array[i];
            }
            Console.WriteLine("Сумма чисел массива равна {0}", s);
        }
        static void GetMax(Task<int[]> task)
        {
            int[] array = task.Result;
            int max = 0;
            for (int i = 0; i < array.Count(); i++)
            {
                if (max < array[i])
                    max = array[i];
            }
            Console.WriteLine("Максимальное число массива равно {0}", max);
        }
    }
}
