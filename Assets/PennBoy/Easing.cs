using UnityEngine;

public static class Easing
{
    public static float EaseOutExpo(float t) {
        return Mathf.Approximately(t, 1f) ? 1f : 1f - Mathf.Pow(2f, -10f * t);
    }

    public static float EaseInOutQuint(float t) {
        return t < 0.5f ? 16f * t * t * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 5f) / 2f;
    }
}