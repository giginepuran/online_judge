/** 2021.12.04
 * Runtime PR 99.40
 * Memory PR 23.75
 */
 
 public class Solution {
    public int RemoveElement(int[] nums, int val) {
        int i = 0, shift = 0;
        while (true) {
            while (i+shift < nums.Length && nums[i+shift]==val) {
                shift++;
            }
            if (i+shift >= nums.Length) break;
            nums[i] = nums[i+shift];
            i++;
        }
        return i;
    }
}
