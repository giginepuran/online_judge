/* Medium
03/06/22

Method1:
Runtime 87.22%
Memory  31.85%

Method2:
Runtime 76.11%
Memory  55.37%
*/



// Method1
public class Solution {
    public int DeleteAndEarn(int[] nums) {
        Array.Sort(nums);
        List<int> keys = new List<int>();
        List<int> counts = new List<int>();
        int pos = -1;
        foreach (int num in nums)
        {
            if (keys.Contains(num))
                ++counts[pos];
            else
            {
                counts.Add(1);
                keys.Add(num);
                ++pos;
            }
        }
        List<int> maxEarns = new List<int>();
        for (int i = 0; i <= pos; i++)
            maxEarns.Add(-1); // -1 means never try to pick
        
        return TryPick(0, keys, counts, maxEarns);
    }
    
    int TryPick(int start, List<int> keys, List<int> counts, List<int> maxEarns)
    {
        if (start >= keys.Count)
            return 0;
        
        if (maxEarns[start]!=-1)
            return maxEarns[start];
        
        if (start == keys.Count-1)
        {
            maxEarns[start] = keys[start] * counts[start];
            return maxEarns[start];
        }

        int path1 = keys[start] * counts[start];
        if (keys[start]+1 != keys[start+1])
        {
            path1 += TryPick(start+1, keys, counts, maxEarns);
            maxEarns[start] = path1;
            return path1;
        }
        else
        {
            path1 += TryPick(start+2, keys, counts, maxEarns);
        } // wait to compare with path2

        int path2 = keys[start+1] * counts[start+1];
        if (start+2 < keys.Count)
        {
            if (keys[start+1]+1 == keys[start+2])
                path2 += TryPick(start+3, keys, counts, maxEarns);
            else
                path2 += TryPick(start+2, keys, counts, maxEarns);
        }
        maxEarns[start] = path1>path2? path1:path2;
        return maxEarns[start];
    }
    
}

// Method2
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
