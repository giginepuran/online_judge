/*
Day12 (4/5)
Runtime PR: 63.99
Memory PR: 94.08
*/
public class Solution {
    public ListNode RemoveNthFromEnd(ListNode head, int n) {
        ref ListNode testNode = ref head;
        int l = 1;
        while(testNode.next!=null){
            testNode = ref testNode.next;
            l++;
        }// Length of node series = (test.val-head.val)+1
        testNode = ref head;
        // NO n node from end <=> NO l-n+1 node from head
        // ^ that is node need to be removed
        for(int i = 1; i!=(l-n+1); i++){
            testNode = ref testNode.next;
        }
        testNode = testNode.next;
        return head;
    }
}
