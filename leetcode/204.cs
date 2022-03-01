/*
Runtime 88.56 %
Memory  76.84 %

Leetcode更快的算法有省略二的過程的做法
*/

public class Solution {
    public int CountPrimes(int n) {
        if (n <= 2) return 0;
        bool[] isNotPrime = new bool[n];

        for (int num = 2; num <= (int)Math.Sqrt(n); ++num)
        {
            if (!isNotPrime[num])
                for (int notPrime = num*num; notPrime < n; notPrime += num)
                    isNotPrime[notPrime] = true;
        }
        int ans = 0;
        for (int i = 2; i < n; ++i)
            if (!isNotPrime[i])
                ++ans;
        return ans;
    }
}

