using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoopedIndexes
{
    public static int GetNextIndex(int currentIndex, int count)
    {
        currentIndex++;

        if (currentIndex >= count)
        {
            return 0;
        }

        return currentIndex;
    }

    public static int GetPreviousIndex(int currentIndex, int count)
    {
        currentIndex--;

        if (currentIndex <= 0)
        {
            return count - 1;
        }

        return currentIndex;
    }
}
