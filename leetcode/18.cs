/*
Day11 (4/4)
Runtime PR: 98.96
Memory PR: 58.13
*/
public class Solution {
    public IList<IList<int>> FourSum(int[] nums, int target) {
        IList<IList<int>> all = new List<IList<int>>();
        if(nums.Length < 4) return all;
        Array.Sort(nums);
        int last3 = nums[nums.Length-1] + nums[nums.Length-2] + nums[nums.Length-3];
        int last2 = nums[nums.Length-1] + nums[nums.Length-2];
        bool[] isSame = new bool[nums.Length];
        for(int i = 1; i < nums.Length; i++){
            isSame[i] = nums[i-1]==nums[i];
        }
        IList<int> nums_list = new List<int>(nums);
        IList<int> set = new List<int>();
        add2List(ref set, 0, 1, target);
        return all;

        void add2List(ref IList<int> set, int start, int layer, int target){
            switch(layer){
                case 3:
                for(int i = start; i < nums.Length-1; i++){
                    if((nums[i]+nums[i+1])>target) break;
                    if((nums[i]+nums[nums.Length-1])<target) continue;
                    if(isSame[i] && i!=start) continue;
                    int j = BinarySearch(ref nums, target-nums[i], i+1, nums.Length-1);
                    if(j!=-1){
                        IList<int> set2 = new List<int>(set);
                        set2.Add(nums[i]);
                        set2.Add(nums[j]);
                        all.Add(set2);
                    }
                }
                break;
                case 2:
                for(int i = start; i < nums.Length-(4-layer); i++){
                    if((nums[i]+nums[i+1]+nums[i+2])>target) break;
                    if((nums[i]+last2)<target) continue;
                    if(i!=start && isSame[i]) continue;
                    set.Add(nums[i]);
                    add2List(ref set, i+1, 3, target-nums[i]);
                    set.Remove(nums[i]);
                }
                break;
                case 1:
                for(int i = start; i < nums.Length-(4-layer); i++){
                    if((nums[i]+nums[i+1]+nums[i+2]+nums[i+3])>target) break;
                    if((nums[i]+last3)<target) continue;
                    if(isSame[i]) continue;
                    set.Add(nums[i]);
                    add2List(ref set, i+1, 2, target-nums[i]);
                    set.Remove(nums[i]);
                }
                break;
            }
        }
        int BinarySearch(ref int[] nums,int num, int start, int end){
            if((end-start)<=1){
                return (nums[start]==num)?start:(nums[end]==num)?end:-1;
            }
            int i = (start + end)/2;
            if(nums[i]==num) return i;
            if(nums[i]>num) return BinarySearch(ref nums, num, start, i);
            return BinarySearch(ref nums, num, i, end);
        }
    }
}
