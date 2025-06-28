using System.Collections.Generic;
using UnityEngine;

public class PetManager : MonoBehaviour {
    public static PetManager Instance;

    public List<PetData> allPets = new List<PetData>();

    private HashSet<string> foundPets = new HashSet<string>();

    public delegate void OnPetFound(string petID);
    public event OnPetFound PetFoundEvent;
    
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        LoadAllPets();
    }

    private void LoadAllPets() {
        PetData[] loadedPets = Resources.LoadAll<PetData>("Pets");
        allPets = new List<PetData>(loadedPets);

        Debug.Log($"Loaded {allPets.Count} pets from Resources/Pets.");
    }

    public void FindPet(PetData pet) {
        if (!foundPets.Contains(pet.petID)) {
            foundPets.Add(pet.petID);
            Debug.Log($"Pet found: {pet.petName}");
            AchievementManager.Instance.CheckAchievements(foundPets.Count);
            PetFoundEvent?.Invoke(pet.petID);  
        }
    }

    public bool IsPetFound(string petID) => foundPets.Contains(petID);
    public int GetFoundCount() => foundPets.Count;
}
