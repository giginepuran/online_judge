/*
4/10 
Runtime: 
PR 99.81
Memory: 
PR 15.69
*/
public class Solution {
    public int[] TwoSum(int[] nums, int target) {
        var nums_copy = (int[]) nums.Clone();
        Array.Sort(nums_copy);
        for(int i = 0, j = nums.Length-1; i<j;){
            int sum = nums_copy[i] + nums_copy[j];
            if (sum > target) j--;
            else if (sum < target) i++;
            else{
                for (int ii = 0; ii < nums.Length; ii++) {
                    if (nums[ii] == nums_copy[i]){
                        for (int jj = ii+1; jj < nums.Length; jj++){
                            if (nums[jj] == nums_copy[j]){
                                return new int[]{ii, jj};
                            }
                        }
                    }
                    else if (nums[ii] == nums_copy[j]){
                        for (int jj = ii+1; jj < nums.Length; jj++){
                            if (nums[jj] == nums_copy[i]){
                                return new int[]{ii, jj};
                            }
                        }
                    }
                }
            }
        }
        return new int[]{};
    }
}
