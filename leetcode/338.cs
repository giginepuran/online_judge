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
