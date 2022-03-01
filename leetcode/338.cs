/* Easy
03/01/22
LinkedNode + Sliding window 作法
Runtime 93.60%
Memory  5.25%

IEnum + Sliding 作法
Runtime 67.82% 
Memory   5.25% 

迴圈作法
Runtime 85.88%
Memory  29.39%
*/

// 迴圈作法
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

// List + Sliding window的作法
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

// LinkedNode + Sliding window 作法
public class Solution {
    public int[] CountBits(int n) 
    {
        int[] ans = new int[n+1];
        LinkedNode head = new LinkedNode(0, null);
        int len = 1;
        while (len <= n)
        {
            LinkedNode scanning = head;
            LinkedNode head2 = new LinkedNode(scanning.val+1, null);
            LinkedNode tail2 = head2;

            while (scanning.next != null)
            {
                scanning = scanning.next;
                tail2.next = new LinkedNode(scanning.val+1, null);
                tail2 = tail2.next;
            }
            // scanning become tail here
            scanning.next = head2;
            len *= 2;
        }
        for (int i = 0; i <= n; i++)
        {
            ans[i] = head.val;
            head = head.next;
        }
        return ans;
    }
    class LinkedNode
    {
        internal int val;
        internal LinkedNode next;
        public LinkedNode(int val, LinkedNode next)
        {
            this.val = val;
            this.next = next;
        }
    }
}
        


/*
迴圈做法是類似費氏數列的做法，從小做到大，大的會回去取小的結果
0 1 
0 1 ( 2%2 + ans(2/2) ) ( 3%2 + ans(3/2) ) ...

Time complexity = O(n)
------------------------------------------------
slide window的作法，但不知道為何速度沒快很多= =
0 1

0 1 (0+1) (1+1)
0 1 1 2

0 1 1 2 (0+1) (1+1) (1+1) (2+1)
0 1 1 2 1 2 2 3

Time complexity = O(logn) ?吧 看起來是= =

Runtime 93.60%
Memory  5.25%
*/

