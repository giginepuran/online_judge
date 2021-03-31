// bored and fast
public class Solution {
    public string IntToRoman(int num) {
        string s = "";
        for(int i = 0; i < num/1000; i++){
            s+='M';
        }num%=1000;
        int ii = 0;
        switch (num/100){
            case int n when (n < 4):
            for(ii = 0; ii < n; ii++) s+='C';
            break;
            case 4:
            s+="CD";
            break;
            case 5:
            s+='D';
            break;
            case 9:
            s+="CM";
            break;
            default:
            s+='D';
            for(ii = 5; ii < num/100; ii++) s+="C";
            break;
        }num%=100;

        switch (num/10){
            case int n when (n < 4):
            for(ii = 0; ii < n; ii++) s+="X";
            break;
            case 4:
            s+="XL";
            break;
            case 5:
            s+="L";
            break;
            case 9:
            s+="XC";
            break;
            default:
            s+='L';
            for(ii = 5; ii < (num/10); ii++) s+='X';
            break;
        }num%=10;

        switch (num){
            case int n when (n < 4):
            for(ii = 0; ii < n; ii++) s+='I';
            break;
            case 4:
            s+="IV";
            break;
            case 5:
            s+='V';
            break;
            case 9:
            s+="IX";
            break;
            default:
            s+='V';
            for(ii = 5; ii < num; ii++) s+='I';
            break;
        }
        return s;    
    }
}
