using System;

namespace _8Q
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            /*
            do
            {
                #region 輸入值
                Console.WriteLine("enter length:");
                int length = Convert.ToInt32(Console.ReadLine());
                if (length < 4)
                {
                    Console.WriteLine("length should >= 4");
                    break;
                }
                Console.WriteLine("enter first queen site:");
                int fq = Convert.ToInt32(Console.ReadLine());
                if (fq > length || fq < 1)
                {
                    Console.WriteLine("first queen site should <= length & > 0");
                    break;
                }
                #endregion

                // 執行
                var a = Q8.GetResult2(length, fq);

                #region 顯示結果
                Console.WriteLine("answer is:");
                foreach (var item in a)
                {
                    Console.WriteLine(item);
                }
                #endregion
            } while (true);
            */
            do
            {
                Console.WriteLine("enter length:");
                int length = Convert.ToInt32(Console.ReadLine());
                if (length < 1)
                {
                    Console.WriteLine("end");
                    break;
                }
                var t1 = DateTime.Now;
                var result = QueenQuestion.GetAllResult(length);
                var t2 = DateTime.Now;

                Console.WriteLine($"result.Count: {result.Count}");
                Console.WriteLine($"use time(ms): {(t2-t1).TotalMilliseconds}");

            } while (true);

            Console.ReadLine();
        }
    }
}
