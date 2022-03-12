/* Medium
03/12/22
19:42 start

AC_1 20:10 (28 min)
Runtime 76.82%
Memory  43.26%
O(nlogn)

AC_2(最速解) 21:01 (79 min)
Runtime 97.40%
Memory   5.49%
O(n)

原本的方法 solution2
先clone一個value相同等長的Node series
再重掃一次head，讓兩串掃的位置相同，來找出
原.random 相對於 clone.random的 object

最快的方法 solution1
利用hashtable + reverse hashtable
直接紀錄
原始 random pointer對應的位置
以及
clone 對應位置的 pointer，
就可以在O(n)的時間完成！！！
有點厲害，感覺這技巧很多地方用的到

其他
最速解寫完，好像其實還有很多東西要最佳化
才能達到最快，
例如
if (before != null) 比 if (index > 0)快
原本是寫後者，速度一直沒辦法上去 (黑人問號)
*/
public class Solution1 {
    public Node CopyRandomList(Node head) {
        if (head == null) return null;

        Dictionary<Node, int> N2I = new Dictionary<Node, int>();
        Dictionary<int, Node> I2N = new Dictionary<int, Node>();
        
        // First, clone nodes with same val
        Node operating = head;
        Node before = null;
        int index = 0;
        while (operating != null)
        {
            Node clone = new Node(operating.val);
            N2I.Add(operating, index);
            I2N.Add(index, clone);
            if (before != null)
                before.next = clone;
            before = clone;
            operating = operating.next;
            ++index;
        }
        index = 0;
        operating = head;
        while (operating != null)
        {
            if (operating.random != null)
                I2N[index].random =  I2N[N2I[operating.random]];
            operating = operating.next;
            ++index;
        }
        return I2N[0];   
    } 
}



public class Solution2 {
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
