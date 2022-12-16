/**
 * Definition for a binary tree node.
 * public class TreeNode {
 *     public int val;
 *     public TreeNode left;
 *     public TreeNode right;
 *     public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
 *         this.val = val;
 *         this.left = left;
 *         this.right = right;
 *     }
 * }
 */
public class Solution {
    public bool LeafSimilar(TreeNode root1, TreeNode root2) {
        List<int> tips1 = new List<int>();
        List<int> tips2 = new List<int>();
        tips1 = FindTips(root1);
        tips2 = FindTips(root2);
        if (tips1.Count != tips2.Count)
            return false;
        for (int i=0; i < tips1.Count; ++i)
        {
            if (tips1[i]!=tips2[i])
                return false;
        }
        return true;
    }
    List<int> FindTips(TreeNode root)
    {
        if (root.left == null)
        {
            if (root.right == null)
                return new List<int> {root.val};
            return FindTips(root.right);
        }
        List<int> tips = FindTips(root.left);
        if (root.right == null)
        {
            return tips;    
        }
        else
        {
            tips.AddRange(FindTips(root.right));
            return tips;
        }
    }
}
