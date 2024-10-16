using UnityEngine;

public static class Logger
{
    public static void Log(string message)
    {
        Debug.Log($"[INFO] {Time.time}: {message}");
    }

    public static void LogWarning(string message)
    {
        Debug.LogWarning($"[WARNING] {Time.time}: {message}");
    }

    public static void LogError(string message)
    {
        Debug.LogError($"[ERROR] {Time.time}: {message}");
    }
}