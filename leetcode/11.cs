/*  I'm not sure whether I used the reference(pointer?)
 *  in the right way?
 */
public class Solution {
    public int MaxArea(int[] height) {
        ref int i = ref height[0], j = ref height[height.Length-1];
        int ii = 0, jj = height.Length-1;
        int L = height.Length-1, Vmax = Math.Min(i, j)*(jj-ii);
        while(ii!=jj){
            if(i < j){
                while(height[++ii] <= i);
                i = ref height[ii];
            }else{
                while(height[--jj] < j);
                j = ref height[jj];
            }
            Vmax=Math.Max(Vmax, Math.Min(i, j)*(jj-ii));
        }        
        return Vmax;
    }
}
