/* 2021.12.03
 * RunTime PR 92.39
 * Memory PR 63.35
 * 
 **/
 
 /**
 * Definition for singly-linked list.
 * public class ListNode {
 *     public int val;
 *     public ListNode next;
 *     public ListNode(int val=0, ListNode next=null) {
 *         this.val = val;
 *         this.next = next;
 *     }
 * }
 */
public class Solution {
    public ListNode SwapPairs(ListNode head) {
        if (head == null) return null;
        ListNode ans = head.next;
        if (ans == null) return head;
        ListNode nextHead = head.next.next;
        ans.next = head;
        ans.next.next = (nextHead==null)?null:SwapPairs(nextHead);
        return ans;
    }
}
