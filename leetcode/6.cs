public class Solution {
    public string Convert(string s, int numRows) {
        string[] ss = new string[numRows];
        int period = 2*numRows-2;
        for(int i = 0; i < s.Length; i++){
            int NO = (period==0)?0:(i%period);
            ss[(NO>=numRows)?(period-NO):NO]+=s[i];
        }
        string sss = "";
        foreach(string str in ss) sss+=str;
        return sss;
    }
}
