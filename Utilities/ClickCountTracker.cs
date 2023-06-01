using System.Collections.Generic;

namespace iTunes_WebApp_API.Utilities
{
    public static class ClickCountTracker
    {
        private static Dictionary<int, int> clickCounts = new Dictionary<int, int>();

        public static int GetClickCount(int trackId)
        {
            if (clickCounts.ContainsKey(trackId))
                return clickCounts[trackId];
            else
                return 0;
        }

        public static void IncrementClickCount(int trackId)
        {
            if (clickCounts.ContainsKey(trackId))
                clickCounts[trackId]++;
            else
                clickCounts[trackId] = 1;
        }
    }
}
