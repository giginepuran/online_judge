public class Solution {
    public int[] TwoSum(int[] nums, int target) {
        int l = nums.Length;
        for(int i = 0; i < l; i++){
            for(int j = l-1; j > i; j--){
                if((nums[i] + nums[j])==target){
                    int[] result = new int[2] {i, j};
                    return result;
                }
            }
        }
        return new int[]{0, 0};
    }
}
