public class Solution {
    public bool IsMatch(string s, string p) {
        return match(s, p, s.Length-1, p.Length-1);
    }
    public bool match(string s, string p, int i, int j) {
        if(i==-1 && j==-1) return true;
        if(j==-1) return false;
        if(p[j]=='*') {
            if (i==-1) return match(s, p, i, j-2);
            if (s[i]==p[j-1] || p[j-1]=='.'){
                return (match(s, p, i-1, j-2) || match(s, p, i-1, j) || match(s, p, i, j-2));
                // stop using * or keep using * or do not using * 
            }
            return match(s, p, i, j-2);
            // ignore this term in p
        }
        if(i==-1) return false;
        if(s[i]==p[j] || p[j]=='.') return match(s, p, i-1, j-1);
        return false;
    }
}
