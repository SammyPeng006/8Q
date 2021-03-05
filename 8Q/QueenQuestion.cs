using System.Collections.Generic;
using System.Linq;

namespace _8Q
{
    public class QueenQuestion
    {
        public static List<List<int>> GetAllResult(int length)
        {
            var result = new List<List<int>>();
            var soucePool = new List<int>();
            for (int i = 1; i <= length; i++)
            {
                soucePool.Add(i);
            }

            var resultT = new Stack<int>();
            var leftCol = new Stack<int>();
            var rightCol = new Stack<int>();

            CheckRow(1, soucePool.Count - 1, soucePool, ref resultT, ref result, ref leftCol, ref rightCol);

            return result;
        }

        private static void CheckRow(
            int rowIdx,
            int startColIdx,
            List<int> sourcePool,
            ref Stack<int> resultRun,
            ref List<List<int>> resultPool,
            ref Stack<int> leftCol,
            ref Stack<int> rightCol)
        {
            // sourcePool.Count = 0 => 完成1組
            if (sourcePool.Count == 0)
            {
                //設置後回上一動
                resultPool.Add(resultRun.ToList());
                return;
            }

            if (startColIdx < 0)
            {
                //回上一row
                return;
            }

            // 取得本Row的col
            var colIdx = CheckCol(resultRun.Count + 1,startColIdx, sourcePool, leftCol, rightCol);

            if (colIdx < 0)
            {
                //謀~ => 回上一row
                return;
            }
            else
            {
                //屋~ => 存一row往下找
                var rr = sourcePool[colIdx];
                resultRun.Push(rr);
                leftCol.Push(rr + rowIdx);
                rightCol.Push(rr - rowIdx);
                var nextSourcePool = sourcePool.Where(i => i != rr).ToList();
                CheckRow(rowIdx + 1, nextSourcePool.Count - 1, nextSourcePool, ref resultRun, ref resultPool, ref leftCol, ref rightCol);

                //回同row找下一個col
                resultRun.Pop();
                leftCol.Pop();
                rightCol.Pop();
                CheckRow(rowIdx, colIdx - 1, sourcePool, ref resultRun, ref resultPool, ref leftCol, ref rightCol);
            }
        }

        private static int CheckCol(
            int row,
            int checkIdx,
            List<int> sourcePool,
            in Stack<int> leftCol,
            in Stack<int> rightCol)
        {
            if (checkIdx < 0)
            {
                return -1;
            }
            return CheckSite(row, sourcePool[checkIdx], leftCol, rightCol)
                ? checkIdx
                : CheckCol(row, checkIdx - 1, sourcePool, leftCol, rightCol);
        }

        private static bool CheckSite(
            int row,
            int site,
            Stack<int> leftCol,
            Stack<int> rightCol)
        {
            return !(leftCol.Contains(site + row) || rightCol.Contains(site - row));
        }

    }
}
