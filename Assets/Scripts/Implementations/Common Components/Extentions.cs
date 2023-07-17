using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Extentions
{
    public static T Next<T>(this IList<T> list, T element)
    {
        var index = list.IndexOf(element);

        if (index == list.Count - 1)
            return list[0];

        return list[index + 1];
    }

    public static T Previous<T>(this IList<T> list, T element)
    {
        var index = list.IndexOf(element);

        if (index == 0)
            return list[list.Count - 1];

        return list[index - 1];
    }
}
