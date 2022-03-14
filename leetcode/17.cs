/*
Day11 (4/4)
Runtime PR: 97.80
Memory PR: 63.34
*/

public class Solution {
    public IList<string> LetterCombinations(string digits) {
        IList<string> combinations = new List<string>(){};
        if(digits.Length==0) return combinations;
        string[] tables = new string[]{"abc","def","ghi","jkl","mno","pqrs","tuv","wxyz"};
        add2List(ref combinations, "", 0);
        return combinations;

        void add2List(ref IList<string> combinations, string str, int layer){
            if(digits.Length-1==layer){
                foreach(char c in tables[(int)Char.GetNumericValue(digits[layer])-2]){
                    combinations.Add(str+c);
                }
            }else{
                foreach(char c in tables[(int)Char.GetNumericValue(digits[layer])-2]){
                    add2List(ref combinations, str+c, layer+1);
                }
            }
        }
    }
}
