public class Solution {
    public int LengthOfLongestSubstring(string s) {
        int max_l = 0, L = s.Length;
        int[] same_position = new int[L];
        for(int i = 0; i < L; i++){
            same_position[i] = -1;
            for(int j = i-1; j >= 0; j--){
                if(s[j] == s[i]){
                    same_position[i] = j;
                    break;
                }
            }
        }
        for(int i = 0; (i < L) && (i+max_l)<L; i++){
            int j = i+1, ll = 0;
            while(j < L){
                if(same_position[j] >= i) {
                    ll = j-i;
                    i = same_position[j];
                    break;
                }
                j++;
            }
            ll = (j==L)? (j-i):ll;
            max_l = (max_l<ll)? ll:max_l;
        }
        return max_l;
    }
}
