/* Medium
03/06/22

Runtime 76.11%
Memory  55.37%

*/

public class Solution {
    public int DeleteAndEarn(int[] nums) {
        Dictionary<int, int> maxEarns = new Dictionary<int, int>();
        Dictionary<int, int> counts = new Dictionary<int, int>();
        List<int> keys = new List<int>();
        
        Array.Sort(nums);
        foreach (int key in nums)
        {
            if (counts.ContainsKey(key))
                ++counts[key];
            else
            {
                keys.Add(key);
                counts.Add(key, 1);
            }
        }
        return TryPick(0, keys, counts, maxEarns);
    }
    int TryPick(int start, List<int> keys, Dictionary<int,int> counts, Dictionary<int,int> maxEarns)
    {
        if (start >= keys.Count)
            return 0;

        int key = keys[start];
        if (maxEarns.ContainsKey(key))
            return maxEarns[key];

        if (start == keys.Count-1)
        {
            maxEarns[key] = counts[key]*key;
            return maxEarns[key];
        }
        
        if (NextIsContinue(start, keys))
        {
            int path1 = key * counts[key] + TryPick(start+2, keys, counts, maxEarns);
            int path2 = keys[start+1] * counts[keys[start+1]];
            if (NextIsContinue(start+1, keys))
                path2 += TryPick(start+3, keys, counts, maxEarns);
            else
                path2 += TryPick(start+2, keys, counts, maxEarns);

            maxEarns[key] = Math.Max(path1, path2);
            return maxEarns[key];
        }
        else
        {
            maxEarns[key] = key * counts[key] + TryPick(start+1, keys, counts, maxEarns);
            return maxEarns[key];
        }
    }
    bool NextIsContinue(int i, List<int> keys)
    {
        if (i >= keys.Count-1)
            return false;
        return (keys[i] + 1) == keys[i + 1];
    }
    
}
