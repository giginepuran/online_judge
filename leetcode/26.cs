/** 2021.12.04
 * Runtime PR 78.57
 * Memory PR 62.93
 */

public class Solution {
    public int RemoveDuplicates(int[] nums) {
        int i = 0, shift = 0;
        while(i+shift < nums.Length) {
            nums[i] = nums[i+shift];
            i++;
            while (i+shift < nums.Length && nums[i+shift]==nums[i-1])
                shift++;
        }
        return i;
    }
}
