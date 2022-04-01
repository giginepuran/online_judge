/* easy
09:50 start
09:56 AC1
*/

public class Solution {
    public void ReverseString(char[] s) {
        char buffer = ' ';
        for (int i = 0; i < s.Length; ++i)
        {
            for (int j = 0; j < s.Length - i - 1; ++j)
            {
                buffer = s[j];
                s[j] = s[j+1];
                s[j+1] = buffer;
            }
        }
    }
}
