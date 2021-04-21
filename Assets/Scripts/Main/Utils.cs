using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Utils
{
    public static T GetRandom<T>(this IEnumerable<T> collection)
        => collection.ElementAt(new Random().Next(collection.Count()));
}
