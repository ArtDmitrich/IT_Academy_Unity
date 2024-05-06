using System.Collections;
using UnityEngine;

public static class Coroutines
{
    public static IEnumerator SetActiveObjectAfterTime(GameObject obj, bool isActive, float time)
    {
        yield return new WaitForSeconds(time);

        obj.SetActive(isActive);
    }
}
