public class Solution {
    public int RomanToInt(string s) {
        int num = 0;
        bool V_read = false, X_read = false, 
        L_read = false, C_read = false, 
        D_read = false, M_read = false;
        for(int i = s.Length-1; i >= 0; i--){
            switch(s[i]){
                case 'I':
                num += 1*((V_read || X_read)?-1:1);
                break;
                case 'V':
                V_read = true;
                num += 5;
                break;
                case 'X':
                X_read = true;
                num += 10*((C_read || L_read)?-1:1);
                break;
                case 'L':
                L_read = true;
                num += 50;
                break;
                case 'C':
                C_read = true;
                num += 100*((D_read || M_read)?-1:1);
                break;
                case 'D':
                D_read = true;
                num += 500;
                break;
                default:
                M_read = true;
                num+=1000;
                break;
            }
        }//End of for
        return num;        
    }// End of RomanToInt
}
