using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesByMatch
{
    class Program
    {
        public static int sockMerchant(int n, List<int> ar)
        {
            /*int numOfPairs = 0;
            Dictionary<int, int> sockCnt = new Dictionary<int, int>();

            //loop through socks array
            foreach(int sock in ar)
            {
                //adds sock to dictionary if it doesnt exsist
                if(!sockCnt.ContainsKey(sock))
                    sockCnt.Add(sock, 1);
                else
                {
                    numOfPairs++;
                    sockCnt.Remove(sock);
                }
            }
            return numOfPairs;*/

            return ar.GroupBy(x => x).Select(x => (x.Count() / 2)).Sum();
        }
        static void Main(string[] args)
        {
        }
    }
}
