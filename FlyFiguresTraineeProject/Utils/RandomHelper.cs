﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace FlyFiguresTraineeProject.Utils;

public static class RandomHelper
{
    private static readonly Random Random = new();

    public static int Next(int min, int max, ICollection<int> exclude)
    {
        int randomNumber;
        
        do {
            randomNumber = Random.Next(min, max);
        } while(exclude.Contains(randomNumber));
        
        return randomNumber;
    }

    public static Point NextDirection()
    {
        var x = NextWithoutZero() * (Random.NextDouble() + 0.01);
        var y = NextWithoutZero() * (Random.NextDouble() + 0.01);

        return x < 0.5 
            ? new Point(x, NextWithoutZero()) 
            : new Point(NextWithoutZero(), y);
    }

    private static int NextWithoutZero()
    {
        return Next(-1, 2, new[] { 0 });
    }
}