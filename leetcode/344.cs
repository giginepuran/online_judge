/* easy
09:50 start
09:56 AC1
Runtime: 97.89%
Memory:   6.89%
*/

public class Solution {
    public void ReverseString(char[] s) {
        char buffer = ' ';
        for (int i = 0; i < s.Length/2; ++i)
        {
            buffer = s[s.Length-i-1];
            s[s.Length-i-1] = s[i];
            s[i] = buffer;
        }
    }
}
