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
    public ListNode AddTwoNumbers(ListNode l1, ListNode l2) {
        ListNode l3 = new ListNode();
        int sum = l1.val + l2.val;
        l3.val = sum % 10;
        if(sum / 10 == 0){
            if(l1.next != null || l2.next != null){
                l3.next = AddTwoNumbers((l1.next==null)? (new ListNode()):l1.next, (l2.next==null)? (new ListNode()):l2.next);
            }
        }else{
            if (l1.next == null) {
                ListNode l4 = new ListNode();
                l4.val = 1;
                l3.next = AddTwoNumbers(l4, (l2.next==null)? (new ListNode()):l2.next);
            }else{
                l1.next.val += 1;
                l3.next = AddTwoNumbers(l1.next, (l2.next==null)? (new ListNode()):l2.next);
            }
        }
        return l3;
    }
}
