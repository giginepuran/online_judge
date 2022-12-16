public class Solution {
    public int SearchInsert(int[] nums, int target) {
        int i;
        for(i = 0; i < nums.Length; i++){
            if (target <= nums[i]) {
                break;
            }
        }
        return i;
    }
}
