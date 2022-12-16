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
    public int RangeSumBST(TreeNode root, int low, int high) {
        if (root == null)
            return 0;
        int n = 0;
        if (root.val <= high && root.val >= low)
            n = root.val;
        n += RangeSumBST(root.left, low, high);
        n += RangeSumBST(root.right, low, high);
        return n;
    }
}
