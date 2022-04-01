/* Hard
17:30 start
18:14 AC1
Runtime: 99.53%
Memory:   5.67%

最佳解是用抓bra數量=key數量，的方式來求最長，
沒有用任何Dictionary/Stack有點厲害的想法@@
*/

public class Solution {
    public int LongestValidParentheses(string s) {
        Dictionary<int,int> pair = new Dictionary<int, int>();
        Stack<int> bra = new Stack<int>();
        for (int i = 0; i < s.Length; ++i)
        {
            if (s[i] == '(')
                bra.Push(i);
            else
            {
                if (bra.Count != 0)
                    pair.Add(bra.Pop(), i);
            }
        }
        int ans = 0;
        foreach (var kv in pair)
        {
            int head = kv.Key;
            int tail = kv.Value;
            while (pair.ContainsKey(tail+1))
            {
                tail = pair[tail+1];
            }
            if (ans < tail - head + 1)
                ans = tail - head + 1;
        }
        return ans;
    }
}
