/* Medium
10:13 start
11:36 AC1
Runtime: 76.61%
Memory:  34.43%

***
最佳解是由後面往前確認遞增，而不是從頭開始判斷遞減!

*/

public class Solution {
    public void NextPermutation(int[] nums) {
        if (EndOfPermuatation(nums, 0))
        {
            Reverse(nums, 0);
        }
    }
    bool EndOfPermuatation(int[] nums, int start)
    {
        if (start == nums.Length-1)
            return true;
        if (nums[start] >= nums[start+1])
            return EndOfPermuatation(nums, start+1);
        if (EndOfPermuatation(nums, start+1))
        {
            int buffer = nums[start];
            for (int i = nums.Length-1; i > start; --i)
            {
                if (nums[i] > buffer)
                {
                    nums[start] = nums[i];
                    nums[i] = buffer;
                    Reverse(nums, start+1);
                    break;
                }
            }
        }
        return false;
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
