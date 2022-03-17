/*
21:35 start
AC_1 10:43 (68 min)
Runtime 98.18%
Memory  10.91%

AC_2 11:26 (68 + 43 min)
Runtime 98.18%
Memory  10.91%
這是LC上最快的參考code，他在讀braket時順便把數字推進去
但實際跑起來沒有比我快很多@@

AC_3 
(()()) =
(()) + (())

(( () () ) () ) = 
((())) + ((())) + (())
直接用被多少個Bra，去算power再加到ans上的方法
code非常短，但好像沒辦法比Stack快耶@@
*/

public class Solution1 {
    public int ScoreOfParentheses(string s) {
        Dictionary<int, int> findKet = new Dictionary<int, int>();
        Stack<int> brasIndex = new Stack<int>();
        for (int i = 0; i < s.Length; ++i)
        {
            if (s[i] == '(')
                brasIndex.Push(i);
            else
                findKet.Add(brasIndex.Pop(), i);
        }

        int ans = 0;        
        for (int i = 0; i < s.Length; ++i)
        {
            if (s[i] == '(')
            {
                if (ans != 0)
                {
                    int sectionEndAt = findKet[i];
                    ans += ScoreOfParentheses(s.Substring(i, sectionEndAt-i+1));
                    i = sectionEndAt;
                }
                else
                    brasIndex.Push(i);
            }
            else
            {
                if (brasIndex.Count == 0)
                    return ans;
                int j = brasIndex.Pop();
                if (i-j == 1)
                    ans += 1;
                else
                    ans *= 2;
            }
        }
        return ans;
    }
}

public class Solution2 {
    public int ScoreOfParentheses(string s) {
        Stack<int> brasIndex = new Stack<int>();
        for (int i = 0; i < s.Length; ++i)
        {
            if (s[i] == '(')
                brasIndex.Push(i);
            else
            {
                int value = brasIndex.Pop();
                if (value + 1 == i)
                    brasIndex.Push(-1);
                else
                {
                    while (brasIndex.Peek() < 0)
                        value += brasIndex.Pop();
                    brasIndex.Pop();
                    brasIndex.Push(value*2);
                }
            }
        }
        int ans = -brasIndex.Pop();
        while (brasIndex.Count != 0)
            ans -= brasIndex.Pop();
        return ans;
    }
}

public class Solution {
    public int ScoreOfParentheses(string s) {
        int ans = 0;
        int power = 0;
        for (int i = 0; i < s.Length; ++i)
        {
            if (s[i] == '(')
                ++power;
            else
            {
                --power;
                if (s[i-1] == '(')
                    ans += (1 << power);
            }
        }
        return ans;
    }
}
