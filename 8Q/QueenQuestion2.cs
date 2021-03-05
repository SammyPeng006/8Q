using System.Collections.Generic;
using System.Linq;

namespace _8Q
{
    /// <summary>
    /// 用 HashSet 比對 => 比 Stack 慢...
    /// </summary>
    public class QueenQuestion2
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
            var leftCol = new HashSet<int>();
            var rightCol = new HashSet<int>();

            CheckRow(1, soucePool.Count - 1, soucePool, ref resultT, ref result, ref leftCol, ref rightCol);

            return result;
        }

        private static void CheckRow(
            int rowIdx,
            int startColIdx,
            List<int> sourcePool,
            ref Stack<int> resultRun,
            ref List<List<int>> resultPool,
            ref HashSet<int> leftCol,
            ref HashSet<int> rightCol)
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
                var prelc = rr + rowIdx;
                var prerc = rr - rowIdx;
                resultRun.Push(rr);
                leftCol.Add(prelc);
                rightCol.Add(prerc);
                var nextSourcePool = sourcePool.Where(i => i != rr).ToList();
                CheckRow(rowIdx + 1, nextSourcePool.Count - 1, nextSourcePool, ref resultRun, ref resultPool, ref leftCol, ref rightCol);

                //回同row找下一個col
                resultRun.Pop();
                leftCol.Remove(prelc);
                rightCol.Remove(prerc);
                CheckRow(rowIdx, colIdx - 1, sourcePool, ref resultRun, ref resultPool, ref leftCol, ref rightCol);
            }
        }

        private static int CheckCol(
            int row,
            int checkIdx,
            List<int> sourcePool,
            in HashSet<int> leftCol,
            in HashSet<int> rightCol)
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
            HashSet<int> leftCol,
            HashSet<int> rightCol)
        {
            return !(leftCol.Contains(site + row) || rightCol.Contains(site - row));
        }

    }
}
