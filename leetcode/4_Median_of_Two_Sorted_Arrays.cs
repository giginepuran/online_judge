// Hard?
public class Solution {
    public double FindMedianSortedArrays(int[] nums1, int[] nums2) {
        int l1 = nums1.Length, l2 = nums2.Length;
        int i1 = 0, i2 = 0, NO = (l1+l2)/2; 
        bool isEven = ((l1+l2)%2==0)? true:false;
        int next = 0;
        while (true) {
            int before = 0;
            int num1 = (i1<l1)? nums1[i1]:1000002;
            int num2 = (i2<l2)? nums2[i2]:1000002;
            if (num2 > num1){
                before = next;
                next = num1;
                i1++;
            }else{
                before = next;
                next = num2;
                i2++;
            }
            if((i1+i2-1)==NO){
                return (isEven)?((double)(before+next))/2:(double)next;
            }
        }
    }
}
