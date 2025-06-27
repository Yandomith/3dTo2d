public static class PlatformAPI {
    public static void UnlockAchievement(string id) {
        // Later: integrate Google Play, Steam, etc.
        UnityEngine.Debug.Log($"[PlatformAPI] Achievement {id} unlocked.");
    }
}
