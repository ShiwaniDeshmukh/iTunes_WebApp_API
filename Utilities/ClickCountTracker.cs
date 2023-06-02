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
            {
                clickCounts[trackId]++;
            }
            else
            {
                clickCounts.Add(trackId, 1);
            }
        }

        public static int GetAlbumClickCount(int albumId)
        {
            if (clickCounts.ContainsKey(albumId))
                return clickCounts[albumId];
            else
                return 0;
        }

        public static void IncrementAlbumClickCount(int albumId)
        {
            if (clickCounts.ContainsKey(albumId))
            {
                clickCounts[albumId]++;
            }
            else
            {
                clickCounts.Add(albumId, 1);
            }
        }

        public static int GetTVEpisodeClickCount(int episodeId)
        {
            if (clickCounts.ContainsKey(episodeId))
                return clickCounts[episodeId];
            else
                return 0;
        }
        public static void IncrementTVEpisodeClickCount(int episodeId)
        {
            if (clickCounts.ContainsKey(episodeId))
            {
                clickCounts[episodeId]++;
            }
            else
            {
                clickCounts.Add(episodeId, 1);
            }
        }

        public static int GetMusicVideoClickCount(int videoId)
        {
            if (clickCounts.ContainsKey(videoId))
                return clickCounts[videoId];
            else
                return 0;
        }
        public static void IncrementMusicVideoClickCount(int videoId)
        {
            if (clickCounts.ContainsKey(videoId))
            {
                clickCounts[videoId]++;
            }
            else
            {
                clickCounts.Add(videoId, 1);
            }
        }
    }
}
