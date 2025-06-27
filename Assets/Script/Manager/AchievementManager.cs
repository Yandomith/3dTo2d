using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour {
    public static AchievementManager Instance;
    public List<AchievementData> achievements;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void CheckAchievements(int foundPetCount) {
        foreach (var ach in achievements) {
            if (!ach.isUnlocked && ShouldUnlock(ach, foundPetCount)) {
                ach.isUnlocked = true;
                Debug.Log($"Achievement unlocked: {ach.title}");

                // Optional: show popup or call platform API
                PlatformAPI.UnlockAchievement(ach.achievementID);
            }
        }
    }

    private bool ShouldUnlock(AchievementData ach, int count) {
        if (ach.achievementID == "find_10" && count >= 10) return true;
        if (ach.achievementID == "find_50" && count >= 50) return true;
        if (ach.achievementID == "find_100" && count >= 100) return true;
        return false;
    }
}