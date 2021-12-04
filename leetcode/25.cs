/** 2021.12.04
 * Runtime PR 99.15
 * Memory PR 34.07
 */

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
    public ListNode ReverseKGroup(ListNode head, int k) {
        if (head == null) return null;
        ListNode newHead, tail = head;
        bool overK = true;
        int i;
        for (i = 1; i < k; i++) {
            newHead = tail.next;
            if (newHead == null){
                overK = false;
                break;
            }
            tail.next = newHead.next;
            newHead.next = head;
            head = newHead;
        }
        if (!overK) return ReverseKGroup(head, i);
        tail.next = ReverseKGroup(tail.next, k);
        return head;
    }
}
