using System;
using System.Collections.Generic;
using System.Linq;

namespace FlyFiguresTraineeProject.Utils;

public static class RandomHelper
{
    private static readonly Random Random = new();

    public static int Next(int min, int max, ICollection<int> exclude)
    {
        var range = Enumerable.Range(1, 100).Where(i => !exclude.Contains(i));
        var index = Random.Next(0, 100 - exclude.Count);
        return range.ElementAt(index);
    }

    public static double NextDirection()
    {
        return Next(-2, 1, new[] { 0 });
    }
}