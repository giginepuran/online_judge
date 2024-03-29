/* Medium 15 min
Runtime 78.14% %
Memory  22.29% %
做法跟最速解一樣，都是用銜尾蛇+找node切斷+return newHead
*/

public class Solution {
    public ListNode RotateRight(ListNode head, int k) {
        if (head == null)
            return null;

        ListNode operating = head;
        int len = 1;
        while (operating.next != null)
        {
            operating = operating.next;
            ++len;
        }
        k = (len - k%len)%len;
        // come to here operating is tail (not null)
        operating.next = head;

        // use operating to find the node before newhead
        for (int i = 0; i < k; i++)
            operating = operating.next;

        ListNode newHead = operating.next;
        operating.next = null;

        return newHead;
    }
}
