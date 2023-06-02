using System.Collections.Generic;
using System.Linq;

public static class ClickCountTracker
{
    private static readonly Dictionary<int, int> clickCounts = new Dictionary<int, int>();

    public static int GetClickCount(int trackId)
    {
        if (clickCounts.ContainsKey(trackId))
        {
            return clickCounts[trackId];
        }
        else
        {
            return 0;
        }
    }

    public static void IncrementClickCount(int trackId)
    {
        if (clickCounts.ContainsKey(trackId))
        {
            clickCounts[trackId]++;
        }
        else
        {
            clickCounts.Add(trackId, 1);
        }
    }

    public static int GetTotalClicks()
    {
        return clickCounts.Sum(x => x.Value);
    }
}
