// LGTM 
// But what happens in 15_3SUM....?????????
public class Solution {
    public int ThreeSumClosest(int[] nums, int target) {
        Array.Sort(nums);
        int min_error = 100000;
        bool[] isSame = new bool[nums.Length];
        for(int i = 1; i < nums.Length; i++){
            isSame[i] = nums[i-1]==nums[i];
        }
        for(int i = 0; i < nums.Length-2; i++){
            if(isSame[i]) continue;
            for(int j = i+1; j < nums.Length-1; j++){
                if(isSame[j] && (j-1)!=i) continue;
                int k = 0, target2 = target-nums[i]-nums[j];
                if (target2<=nums[j+1]) k = j+1;
                else if(target2 >= nums[nums.Length-1]) k = nums.Length-1;
                else k = ClosestBinarySearch(nums, target2, j+1, nums.Length-1);
                if(Math.Abs(min_error)>Math.Abs(target2 - nums[k])){
                        min_error = target2 - nums[k];
                        if(min_error==0) return target;
                }
            }
        }
        return (target-min_error);
    }

    public int ClosestBinarySearch(int[] nums,int num, int start, int end){
        if((end-start)<=1){
            return ((num - nums[start]) < (nums[end] - num))?start:end;
        }
        int i = (start + end)/2;
        if(nums[i]==num) return i;
        if(nums[i]>num) return ClosestBinarySearch(nums, num, start, i);
        return ClosestBinarySearch(nums, num, i, end);
    }
}
