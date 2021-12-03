/* 2021.12.03
 * RunTime PR 29.94
 * Memory PR 5.67
 *
 **/

using System;
using System.Collections.Generic;

public class Solution {
    public IList<string> GenerateParenthesis(int n) {
        IList<string> ans = new List<string>(){};
        if (n == 1){
            ans.Add("()");
            return ans;
        }
        IList<string> subs = GenerateParenthesis(n-1);
        foreach (string sub in subs){
            ans.Add($"({sub})");
        }
        for (int i=1; i<n; i++){
            IList<string> subs1 = GenerateParenthesis(i);
            IList<string> subs2 = GenerateParenthesis(n-i);
            foreach (string sub1 in subs1){
                foreach (string sub2 in subs2){
                    string buf = $"{sub1}{sub2}";
                    if (!ans.Contains(buf))
                        ans.Add(buf);
                }
            }
        }
        return ans;
    }
}
