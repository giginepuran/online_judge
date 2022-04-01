/* Medium
10:13 start
11:36 AC1
Runtime: 76.61%
Memory:  34.43%

11:54 AC2
Runtime: 96.39%
Memory:  15.85%
***
最佳解是由後面往前確認遞增，而不是從頭開始判斷遞減!

*/

public class Solution {
    public void NextPermutation(int[] nums) {
        if (nums.Length == 1) return;
        int n = nums.Length-2;
        while (n >= 0 && nums[n] >= nums[n+1])
            --n;
        if (n != -1)
        {
            int m = nums.Length-1;
            while (nums[m] <= nums[n])
                --m;
            Swap(nums, n, m);
        }
        Reverse(nums, n+1);
    }
    void Swap(int[] nums, int i, int j)
    {
        int buffer = nums[i];
        nums[i] = nums[j];
        nums[j] = buffer;
    }
    void Reverse(int[] nums, int start)
    {
        for (int i = 0; i < (nums.Length-start)/2; ++i)
        {
            int buffer = nums[i+start];
            nums[i+start] = nums[nums.Length-1-i];
            nums[nums.Length-1-i] = buffer;
        }
    }
}
