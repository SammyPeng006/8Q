using System;
using System.Collections.Generic;

namespace _8Q
{
    public class Q8
    {

        #region 大到小
        /// <summary>
        /// 大到小
        /// </summary>
        /// <param name="length"></param>
        /// <param name="firstSite"></param>
        /// <returns></returns>
        public static List<string> GetResult1(int length, int firstSite)
        {
            var result = new List<string> { GetRow(length, firstSite) };
            var sameCol = new HashSet<int> { { firstSite } };
            var leftCol = new HashSet<int> { { firstSite + 1 } };
            var rightCol = new HashSet<int> { { firstSite - 1 } };

            for (int r = 2; r < length; r++)
            {
                Random _rnd = new Random();
                var s = GetSite1(r, length, sameCol, leftCol, rightCol);

                result.Add(GetRow(length, s));
                sameCol.Add(s);
                leftCol.Add(r + s);
                rightCol.Add((s - r));
            }

            return result;
        }

        /// <summary>
        /// 大到小
        /// </summary>
        /// <param name="row"></param>
        /// <param name="site"></param>
        /// <param name="sameCol"></param>
        /// <param name="leftCol"></param>
        /// <param name="rightCol"></param>
        /// <returns></returns>
        private static int GetSite1(
            int row,
            int site,
            HashSet<int> sameCol,
            HashSet<int> leftCol,
            HashSet<int> rightCol)
        {
            if (site <= 0)
            {
                throw new Exception($"GG in row={row}");
            }
            return CheckSite(row, site, sameCol, leftCol, rightCol)
                ? site
                : GetSite1(row, site - 1, sameCol, leftCol, rightCol);
        }
        #endregion

        #region 隨機

        /// <summary>
        /// 隨機
        /// </summary>
        /// <param name="length"></param>
        /// <param name="firstSite"></param>
        /// <returns></returns>
        public static List<string> GetResult2(int length, int firstSite)
        {
            var result = new List<string> { GetRow(length, firstSite) };

            // set 1st row
            var sameCol = new HashSet<int> { firstSite };
            var leftCol = new HashSet<int> { firstSite+1 };
            var rightCol = new HashSet<int> { firstSite-1 };

            var pool = new List<int>();
            for (int i = 1; i <= length; i++)
            {
                pool.Add(i);
            }
            pool.Remove(firstSite);

            pool.Shuffle();

            for (int r = 2; r < length; r++)
            {
                var s = GetSite2(r, 0, sameCol, leftCol, rightCol, pool);

                result.Add(GetRow(length, s));
                sameCol.Add(s);
                leftCol.Add(r + s);
                rightCol.Add(s - r);
                pool.Remove(s);
            }

            return result;
        }

        /// <summary>
        /// 隨機
        /// </summary>
        /// <param name="row"></param>
        /// <param name="site"></param>
        /// <param name="sameCol"></param>
        /// <param name="leftCol"></param>
        /// <param name="rightCol"></param>
        /// <returns></returns>
        private static int GetSite2(
            int row,
            int poolIdx,
            HashSet<int> sameCol,
            HashSet<int> leftCol,
            HashSet<int> rightCol,
            List<int> pool)
        {
            return CheckSite(row, pool[poolIdx], sameCol, leftCol, rightCol)
                ? pool[poolIdx]
                : GetSite2(row, poolIdx+1, sameCol, leftCol, rightCol, pool);
        }

        #endregion

        private static string GetRow(int length, int site)
        {
            return $"{new String('.', site - 1)}Q{new String('.', length - site)}";
        }

        private static bool CheckSite(
            int row,
            int site,
            HashSet<int> sameCol,
            HashSet<int> leftCol,
            HashSet<int> rightCol)
        {
            return !(sameCol.Contains(site) || leftCol.Contains(site + row) || rightCol.Contains(site - row));
        }



    }
}
