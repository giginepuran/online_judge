public class Solution {
    static public int MyAtoi(string s) {
        int heading = -1;
        int start = 0, l = 0;
        for (int i = 0; i < s.Length; i++) {
            if (l == 0) {
                if (Char.GetNumericValue(s[i])!=-1) {
                    start = i;
                    l++;
                    continue;
                }
                else if (s[i]==' ') continue;
                else if ((s[i]=='+' || s[i]=='-') && heading==-1) {
                    heading=i;                      
                }
                else break;
            }
            else {
                if (Char.GetNumericValue(s[i])==-1) break;
                l++;
            }
        }
        
        while (l != 0 && s[start]=='0') {
            start++;
            l--;
        }
        
        if (l==0) return 0;
        bool isPositive = (heading==-1)? true:(s[heading]=='+')? true:false;
        if (l>10)
            return (isPositive?int.MaxValue:int.MinValue);
            
        long num = long.Parse(s.Substring(start, l));
        num *= isPositive?1:-1;
        if (num > int.MaxValue) return int.MaxValue;
        if (num < int.MinValue) return int.MinValue;
        return (int)num;
    }
}
