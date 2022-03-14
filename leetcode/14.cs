public class Solution {
    public string LongestCommonPrefix(string[] strs) {
        string s = "";
        int L = 200;
        foreach(string str in strs){
            L = (L>str.Length)?str.Length:L;
        }
        if (strs.Length==0) return "";
        for(int i = 0; i < L; i++){
            char c = strs[0][i];
            bool iscommon = true;
            foreach(string str in strs){
                iscommon = (c==str[i]);
                if (!iscommon) break;
            }
            if(iscommon) s+= c;
            else break;
        }
        return s;
    }
}
