public class Solution {
    public bool IsPalindrome(int x) {
        if(x < 0) return false;
        if(x < 10) return true;
        int digi = 10;
        while((x/digi)>=10) digi *= 10;    
        while(digi>=10){
            if(x%10 != x/digi) return false;
            x %= digi;
            x /= 10;
            digi /= 100;
        }
        return true;
    }
}
