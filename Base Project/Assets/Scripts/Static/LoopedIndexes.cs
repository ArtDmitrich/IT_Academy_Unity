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

    public static int GetPrevIndex(int currentIndex, int count)
    {
        currentIndex--;

        if (currentIndex < 0)
        {
            return count - 1;
        }

        return currentIndex;
    }
}
