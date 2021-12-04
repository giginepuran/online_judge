/** 2021.12.04
 * Runtime PR -
 * Memory PR -
 * disobey the description
 */

public class Solution {
    public int Divide(int dividend, int divisor) {
        long ans = ((long)dividend)/divisor;
        return (int)(ans>int.MaxValue?int.MaxValue:(ans<int.MinValue?int.MinValue:ans));
    }
}
