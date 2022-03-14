public class Solution {
    public string LongestPalindrome(string s) {
        short L = (short)s.Length;
        short Max_P = 0;
        bool[] isConnect = new bool[L];
        for(short i = 0; i < L-1; i++){
            if(s[i+1]==s[i]){
                isConnect[i] = true;
            }
        }
        short start = 0, end = 0;
        for(short i = (short)(L-1); i >= 0; i--){
            if(i-Max_P/2 < 0) break;
            short j = 0, ll;
            if(isConnect[i]){
                j = 1;
                ll = 2;
                while((i-j)>=0 && isConnect[i-j]){
                    j++;
                    ll++;
                }
                while((i-j)>=0){
                    if(i-j+ll+1 >= L) break;
                    if(s[i-j]!=s[i-j+ll+1]) break;
                    j++;
                    ll+=2;
                }
                if(Max_P < ll){
                    Max_P = ll;
                    start = (short)(i-j+1);
                    end = (short)(i-j+ll);
                }
            }
            else{
                j = 1;
                ll = 1;
                while((i-j)>=0){
                    if(i-j+ll+1 >= L) break;
                    if(s[i-j]!=s[i-j+ll+1]) break;
                    j++;
                    ll+=2;
                }
                if (Max_P < ll){
                    Max_P = ll;
                    start = (short)(i-j+1);
                    end = (short)(i-j+ll);
                }
            }
        }
        string ss = "";
        for(short i = start; i <= end; i++) ss+=s[i];
        return ss;
    }
}
