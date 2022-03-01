/* Easy
03/01/22
Runtime 85.88%
Memory  29.39%
*/

public class Solution {
    public int[] CountBits(int n) 
    {
        int[] ans = new int[n+1];
        // if n = 0, ans = [0] just retuen ans
        if (n > 0)
            ans[1] = 1;

        for (int i = 2; i <= n; i++)
        {
            ans[i] = i%2 + ans[i/2];
        }
        return ans;
    }
}

/*
做法是類似費波納氣數列的做法，從小做到大，大的會回去取小的結果
0 1 
0 1 ( 2%2 + ans(2/2) ) ( 3%2 + ans(3/2) ) ...

Time complexity = O(n)
------------------------------------------------
本來想要用類似slide window的作法，但不知道為何速度沒辦法超越單純迴圈= =
0 1

0 1 (0+1) (1+1)
0 1 1 2

0 1 1 2 (0+1) (1+1) (1+1) (2+1)
0 1 1 2 1 2 2 3

Time complexity = O(logn) ?吧 看起來是= =
有考量到可能是array與Ienum間轉換的時間所導致，
而一開始就把[0, 1]宣告成IEnum還是快不起來

...
*/

/* Sliding window的作法
Runtime 67.82% 
Memory   5.25% 
*/
public class Solution {
    public int[] CountBits(int n) 
    {
        var series = Enumerable.Range(0, 2);

        int length = 2;
        while (length <= n)
        {
            series = series.Concat(
                     from num in series
                     select num+1);
            length *= 2;
        }
        series = series.Take(n+1);
        return series.ToArray();
    }
}
