using System.Collections.Generic;

using Far.Library.Models;

namespace Far.Library.Abstractions;

public interface IStringPositionFinder
{
    public IEnumerable<CharPosition> GetCharPositions(string toBeSearched, char expected, bool ignoreCase);
    public IEnumerable<CharPosition> GetCharPositions(IEnumerable<string> strings, char expected, bool ignoreCase);

    public IEnumerable<StringPosition> GetStringPositions(string stringToBeSearched, string expected, bool ignoreCase);

    public IEnumerable<StringPosition> GetStringPositions(IEnumerable<string> strings, string expected, bool ignoreCase);

}