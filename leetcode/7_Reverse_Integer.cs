// Learned:to deal with reversed use long to save output is faster than use string...
// string is harm for memory use, and even slower....
public class Solution {
    public int Reverse(int x) {
        long y = 0;
        bool isPositive = x>=0;
        if (!isPositive) x*=-1;
        while(x!=0){
            y=((x%10)+y*10);
            x/=10;
        }
        y *= (isPositive)?1:-1;
        return (y>int.MaxValue || y<int.MinValue)?0:(int)y;
    }
}
