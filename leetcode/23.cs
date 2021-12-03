/* 2021.12.03
 * RunTime PR 97.73
 * Memory PR 46.41
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
    public ListNode MergeKLists(ListNode[] lists) {
        if (lists.Length == 0) return null;
        ListNode ans = new ListNode(){val = -999999, next = null};
        ListNode tail = ans;
        bool finish = true;
        foreach (ListNode list in lists) {
            finish &= list==null;
            if (!finish) break;
        }
        while(!finish){
            int mI = FindMin(lists);
            int minimum = lists[mI].val;
            for (int i = mI; i < lists.Length; i++){
                while (lists[i]!=null && lists[i].val == minimum){
                    tail.next = lists[i];
                    tail = tail.next;
                    lists[i] = lists[i].next;
                }
            }

            // check whether lists left nulls only
            finish = true;
            foreach(ListNode list in lists){
                finish &= list==null;
                if (!finish) break;
            }
        }
        return ans.next;
    }
    
    public int FindMin(ListNode[] lists) {
        int index , mini = 10001;
        for (index = 0; index < lists.Length; index++) {
            if (lists[index]!=null) {
                mini = lists[index].val;
                break;
            }
        }
        
        for (int i = index+1; i < lists.Length; i++) {
            if (lists[i]!=null) { // **first minimum**
                index = (lists[i].val<mini)?i:index;
                mini = lists[index].val;
            }
        }
        return index;
    }
}
