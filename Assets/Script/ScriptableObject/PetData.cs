using UnityEngine;

[CreateAssetMenu(fileName = "NewPet", menuName = "Game/Pet")]
public class PetData : ScriptableObject {
    public string petID;
    public string petName;
    public Sprite petIcon;
}