namespace Far.Library.Extensions;

public static class PartialMatchExtensions
{
    /// <summary>
    /// Determines whether a string is or contains a partial match to another string.
    /// </summary>
    /// <param name="toBeSearched">The string to be searched.</param>
    /// <param name="s">The string to look for.</param>
    /// <returns>true if a partial match to a string is found within the string to be searched; false otherwise.</returns>
    public static bool IsAPartialMatch(this string toBeSearched, string s)
    {
        return (toBeSearched.ToLower().Equals(s.ToLower()) || toBeSearched.ToLower().Contains(s.ToLower()));
    }
}