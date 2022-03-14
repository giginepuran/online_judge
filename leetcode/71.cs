/* Medium
16:51 start


17:31
AC - 1 ~ 40 min
Runtime 88.97%
Memory  5.42%
用List實驗Stack


17:45 最速解?
AC - 2 ~ 40+14 min
Runtime 88.97%
Memory  5.42%
最速解是直接用C#中的Stack Class
好像沒有快多少@@?
最速解是直接Split by "/" 並且使用 StringSplitOptions.RemoveEmptyEntries
嘗試後發現使用 StringSplitOptions.RemoveEmptyEntries 反而會拖慢速度?
*/

public class Solution1 {
    public string SimplifyPath(string path) {
        for (int i = 1; i < path.Length; )
        {
            if (path[i]=='/' && path[i-1]=='/')
                path = path.Remove(i, 1);
            else
                ++i;
        }
        
        string[] dirs = path.Split("/");
        List<string> ans = new List<string>();
        foreach (string dir in path.Split("/"))
        {
            if (dir == "..")
            {
                if (ans.Count != 0)
                    ans.RemoveAt(ans.Count-1);
            }
            else if (dir != "." && dir.Length != 0)
            {
                ans.Add(dir);
            }
        }
        path = "";
        foreach (string dir in ans)
            path += ("/"+dir);
            
        return path.Length!=0?path:"/";
    }
}

public class Solution2 {
    public string SimplifyPath(string path) {
        for (int i = 1; i < path.Length; )
        {
            if (path[i]=='/' && path[i-1]=='/')
                path = path.Remove(i, 1);
            else
                ++i;
        }
        Stack<string> ans = new Stack<string>();

        foreach (string dir in path.Split("/"))
        {
            if (dir == "..")
            {
                if (ans.Count != 0)
                    ans.Pop();
            }
            else if (dir != "." && dir.Length != 0)
            {
                ans.Push(dir);
            }
        }
        path = "";
        foreach (string dir in ans)
            path = ("/"+dir)+path;
            
        return path.Length!=0?path:"/";
    }
}
