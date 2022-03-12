/* Medium
03/12/22
19:42 start

AC_1 8:10 (28 min)
Runtime 76.82%
Memory  43.26%

最快的方法是用
hashtable + reverse hashtable
直接查找random pointer對應的位置
感覺這技巧很多地方用的到
*/

public class Solution {
    public Node CopyRandomList(Node head) {
        if (head == null) return null;

        // clone nodes with same val only
        Node newHead = new Node(head.val);
        Node operating = head.next;
        Node operating2 = newHead;
        while (operating != null)
        {
            operating2.next = new Node(operating.val);
            operating2 = operating2.next;
            operating = operating.next;
        }

        // sync two nodes' pointer
        operating = head;
        operating2 = newHead;
        while (operating != null)
        {
            Node surve = head;
            Node surve2 = newHead;
            // sync search nodes through two nodes
            while (surve != operating.random)
            {
                surve = surve.next;
                surve2 = surve2.next;
            }
            operating2.random = surve2;
            operating = operating.next;
            operating2 = operating2.next;
        }

        return newHead;   
    } 
}
