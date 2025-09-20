using UnityEngine;

public static class DebugTools {
    /// <summary>
    /// Checks if a required component exists. Logs an error if missing. Returns true if present.
    /// </summary>
    public static bool HasRequiredComponent<T>(GameObject obj) where T : Component {
        if (obj.TryGetComponent<T>(out _)) return true;
        Debug.LogError($"Missing required component: {typeof(T).Name} on GameObject '{obj.name}'");
        return false;
    }

    /// <summary>
    /// Checks if an optional component exists. Logs a warning if missing. Returns true if present.
    /// </summary>
    public static bool HasOptionalComponent<T>(GameObject obj) where T : Component {
        if (obj.TryGetComponent<T>(out _)) return true;
        Debug.LogWarning($"Missing optional component: {typeof(T).Name} on GameObject '{obj.name}'");
        return false;
    }

    /// <summary>
    /// Returns a required component if it exists. Returns null and logs an error if missing.
    /// </summary>
    public static bool GetRequiredComponent<T>(GameObject obj, out T component) where T : Component {
        if (obj.TryGetComponent<T>(out component)) return true;
        Debug.LogError($"Missing required component: {typeof(T).Name} on GameObject '{obj.name}'");
        component = null;
        return false;
    }

    /// <summary>
    /// Returns an optional component if it exists. Returns null and logs a warning if missing.
    /// </summary>
    public static bool GetOptionalComponent<T>(GameObject obj, out T component) where T : Component {
        if (obj.TryGetComponent<T>(out component)) return true;
        Debug.LogWarning($"Missing optional component: {typeof(T).Name} on GameObject '{obj.name}'");
        component = null;
        return false;
    }
}
