/*
  Stil TLE
  idea : 
  123(4:false) -> 1 put back (skips[1] = false)
  next -> 23(...)
  or
  using bool[words.Length, words.Length]
*/

using System.Collections;
using System.Collections.Generic;
public class Solution {
    public IList<int> FindSubstring(string s, string[] words) {
        int totLen = 0;
        foreach (string word in words)
            totLen += word.Length;
        IDictionary<string, bool[]> table = new Dictionary<string, bool[]>();
        bool[] ignore = new bool[s.Length];
        bool[] dup = new bool[s.Length];
        for (int i = 0; i < words.Length; i++) {
            try {
                var get = table[words[i]];
                dup[i] = true;
            }
            catch {
                table.Add(words[i], new bool[s.Length]);
                for (int j = 0; j+words[i].Length <= s.Length; j++){
                    if (ignore[j]) continue;
                    int k = StrStr(s.Substring(j), words[i]);
                    if (k == -1) break;
                    table[words[i]][j+k] = true;
                    ignore[j+k] = true;
                    j += k;
                } 
            }
             
        }
        
        IList<int> ans = new List<int>();
        bool[] skips = new bool[words.Length];
        FindContinue(table, words, ans, totLen);
        return ans;
    }

    public bool FindContinue(IDictionary<string, bool[]> table, string[] words, IList<int> ans, int totLen) {
        bool[] skips = new bool[words.Length];
        for (int j = 0; j < table[words[0]].Length; j++) {
            //if (!ignore[j]) continue;
            for (int i = 0; i < words.Length; i++) {
                if (j + totLen > table[words[i]].Length) break;
                if (table[words[i]][j]) {
                    skips[i] = true;
                    bool found = FindSubstring(table, words, skips, 1, j+words[i].Length);
                    skips[i] = false;
                    if (found) {
                        ans.Add(j);
                        break;
                    }
                }
            }
        }
        return true;
    }
    public bool FindSubstring(IDictionary<string, bool[]> table, string[] words, bool[] skips, int layer, int start) {
        if (layer == words.Length) return true;
        for (int i = 0; i < words.Length; i++) {
            if (skips[i]) continue;
            if (table[words[i]][start]) {
                skips[i] = true;
                bool found = FindSubstring(table, words, skips, layer+1, start+words[i].Length);
                skips[i] = false;
                if (found)
                    return true;
            }
            
        }
        return false;
    }
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
