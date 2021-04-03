// DIY Binary search is faster than IndexOf()
// To make it faster... maybe DIY sorting?
// Need to review

public class Solution {
    public IList<IList<int>> ThreeSum(int[] nums) {
        IList<IList<int>> all = new List<IList<int>>();
        bool[] isSame = new bool[nums.Length];
        bool[] isSame2 = new bool[nums.Length];
        Array.Sort(nums);
        for(int i = 1; i < nums.Length; i++){
            isSame[i] = nums[i-1]==nums[i];
        }
        for(int j = 2; j < nums.Length; j++){
            isSame2[j] = nums[j-2]==nums[j];
        }
        for(int i = 0; i < nums.Length-2; i++){
            if(isSame[i]) continue;
            if(nums[i]>0) break;
            for(int j = i+1; j < nums.Length-1; j++){
                if(isSame[j] && (j-1)!=i) continue;
                if((nums[i]+nums[j]+nums[j+1])>0) break;
                if(nums[nums.Length-1]<(-nums[i]-nums[j]))continue;
                int k = BinarySearch(nums, -nums[i]-nums[j], j+1, nums.Length-1);
                if(k!=-1){
                    IList<int> set = new List<int>(){nums[i], nums[j], nums[k]};
                    all.Add(set);
                }
            }
        }
        return all;
    }
    public int BinarySearch(int[] nums,int num, int start, int end){
        if((end-start)<=1){
            return (nums[start]==num)?start:(nums[end]==num)?end:-1;
        }
        int i = (start + end)/2;
        if(nums[i]==num) return i;
        if(nums[i]>num) return BinarySearch(nums, num, start, i);
        return BinarySearch(nums, num, i, end);
    }
}
