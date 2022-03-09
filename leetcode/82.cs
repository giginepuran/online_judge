/* Medium
Runtime: 93.86% ??
Memory:   5.26% 
同一個程式碼，LC Runtime浮動可以差了兩倍= =
*/

public class Solution {
    public ListNode DeleteDuplicates(ListNode head) {
        if (head == null) return head;
        
        ListNode next = head.next;
        if (next == null)
            return head;
        else if (next.val == head.val)
        {
            int val = head.val;
            next = next.next;
            while (next != null && next.val == val)
                next = next.next;
            return DeleteDuplicates(next);
        }
        else
        {
            head.next = DeleteDuplicates(next);
            return head;
        }
    }
}
