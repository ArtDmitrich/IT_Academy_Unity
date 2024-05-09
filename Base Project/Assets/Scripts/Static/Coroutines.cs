using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public static class Coroutines
{
    public static IEnumerator SetActiveObjectAfterTime(GameObject obj, bool isActive, float time)
    {
        yield return new WaitForSeconds(time);

        obj.SetActive(isActive);
    }

    public static IEnumerator SetButtonInteractivityForTime(Button button, bool isInteractivity, float time)
    {
        Debug.Log("Coroutine method");
        button.interactable = isInteractivity;

        yield return new WaitForSeconds(time);

        button.interactable = !isInteractivity;
    }
}
