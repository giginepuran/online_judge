public class Solution {
    public int ClimbStairs(int n) {
        if (n==1)
            return 1;
        int a = 1, b = 2;
        for (int i = 3; i <= n; ++i)
        {
            (a,b) = (b,a+b);
        }
        return b;
    }
}
