using UnityEngine;
using UnityEngine.Events;
using System.Collections;
/// <summary>
/// A static class for general helpful methods
/// </summary>
public static class Helpers
{
    /// <summary>
    /// Destroy all child objects of this transform (Unintentionally evil sounding).
    /// Use it like so:
    /// <code>
    /// transform.DestroyChildren();
    /// </code>
    /// </summary>
    public static void DestroyChildren(this Transform t)
    {
        foreach (Transform child in t) Object.Destroy(child.gameObject);
    }
    public static void Wait(this MonoBehaviour mono, float delay, UnityAction action)
    {
        mono.StartCoroutine(ExecuteAction(delay, action));
    }

    private static IEnumerator ExecuteAction(float delay, UnityAction action)
    {
        yield return new WaitForSecondsRealtime(delay);
        action.Invoke();
        yield break;
    }
}
