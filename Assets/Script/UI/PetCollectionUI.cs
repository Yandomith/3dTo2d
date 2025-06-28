using System.Collections.Generic;
using UnityEngine;

public class PetCollectionUI : MonoBehaviour
{
    public GameObject petIconPrefab;
    public Transform petIconParent;

    private Dictionary<string, PetIconUI> petIcons = new Dictionary<string, PetIconUI>();

    private void Start()
    {
        PopulatePetIcons();
        if (PetManager.Instance != null)
            PetManager.Instance.PetFoundEvent += UpdatePetIcon;
    }

    private void OnDestroy()
    {
        if (PetManager.Instance != null)
            PetManager.Instance.PetFoundEvent -= UpdatePetIcon;
    }

    private void PopulatePetIcons()
    {
        foreach (var petData in PetManager.Instance.allPets)
        {
            GameObject iconGO = Instantiate(petIconPrefab, petIconParent);
            PetIconUI iconUI = iconGO.GetComponent<PetIconUI>();
            bool isFound = PetManager.Instance.IsPetFound(petData.petID);
            iconUI.Initialize(petData, isFound);

            petIcons.Add(petData.petID, iconUI);
        }
    }

    private void UpdatePetIcon(string petID)
    {
        if (petIcons.TryGetValue(petID, out PetIconUI icon))
        {
            icon.SetFound(true);
        }
    }
}