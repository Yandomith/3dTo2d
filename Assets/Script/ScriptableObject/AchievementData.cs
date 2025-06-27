using UnityEngine;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "Game/Achievement")]
public class AchievementData : ScriptableObject {
    public string achievementID;
    public string title;
    public string description;
    public Sprite icon;
    [HideInInspector] public bool isUnlocked; // runtime only
}