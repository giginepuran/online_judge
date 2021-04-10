// Easy
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
/*
4/10 update
Runtime: 
220ms
PR 99.81
Memory: 
32MB
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
