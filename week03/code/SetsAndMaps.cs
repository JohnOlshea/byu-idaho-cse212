using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// Problem 1 - FindPairs, O(n) using a set.
    /// Put all words in a HashSet, then for each word check if its
    /// reverse is also in the set. Remove both once paired so each
    /// pair is only reported once.
    /// </summary>
    public static string[] FindPairs(string[] words)
    {
        var wordSet = new HashSet<string>(words);
        var pairs = new List<string>();

        foreach (var word in words)
        {
            // "aa" has no distinct reverse partner - skip it
            if (word[0] == word[1])
                continue;

            // Reverse the 2-char word: "am" -> "ma"
            string reversed = new string(new[] { word[1], word[0] });

            // Only emit if the reverse is still in the set.
            // Removing both prevents emitting the same pair twice
            // when we later reach the reversed word.
            if (wordSet.Contains(reversed))
            {
                pairs.Add($"{word} & {reversed}");
                wordSet.Remove(word);
                wordSet.Remove(reversed);
            }
        }

        return pairs.ToArray();
    }

    /// <summary>
    /// Problem 2 - SummarizeDegrees
    /// Degree is column 4 = index 3 (0-based).
    /// </summary>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            var fields = line.Split(",");
            string degree = fields[3].Trim();

            if (degrees.ContainsKey(degree))
                degrees[degree]++;
            else
                degrees[degree] = 1;
        }

        return degrees;
    }

    /// <summary>
    /// Problem 3 - IsAnagram, O(n) using a dictionary.
    /// Ignores spaces and case. Counts letters of word1, then
    /// subtracts letters of word2. If every count returns to 0,
    /// they are anagrams.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        var counts = new Dictionary<char, int>();

        foreach (char c in word1)
        {
            if (c == ' ')
                continue;
            char lower = char.ToLowerInvariant(c);
            if (counts.ContainsKey(lower))
                counts[lower]++;
            else
                counts[lower] = 1;
        }

        foreach (char c in word2)
        {
            if (c == ' ')
                continue;
            char lower = char.ToLowerInvariant(c);
            if (!counts.ContainsKey(lower))
                return false;

            counts[lower]--;
            if (counts[lower] == 0)
                counts.Remove(lower);
        }

        // True only if every letter was cancelled out
        return counts.Count == 0;
    }

    /// <summary>
    /// Problem 5 - EarthquakeDailySummary
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        if (featureCollection?.Features == null)
            return [];

        return featureCollection.Features
            .Select(f => $"{f.Properties.Place} - Mag {f.Properties.Mag}")
            .ToArray();
    }
}