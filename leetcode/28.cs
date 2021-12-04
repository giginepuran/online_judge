/** 2021.12.04
 * Runtime PR 74.82
 * Memory PR 50.70
 */

public class Solution {
    public int StrStr(string haystack, string needle) {
        if (needle.Length == 0) return 0;
        int i, shift;
        bool found;
        for(i = 0; i < haystack.Length && needle.Length <= haystack.Length-i; i++) {
            found = true;
            shift = 0;
            foreach (char c in needle) {
                if (haystack[i+shift] != c) {
                    found = false;
                    break;
                }
                shift++;
            }
            if (found) return i;
        }
        return -1;
    }
}
