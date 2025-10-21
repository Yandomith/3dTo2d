using UnityEngine;
using UnityEngine.UI;

public class PetIconUI : MonoBehaviour
{
    public Image iconImage;
    public GameObject foundHighlight;

    private string petID;

    public void Initialize(PetData petData, bool isFound)
    {
        petID = petData.petID;
        iconImage.sprite = petData.petIcon;
        SetFound(isFound);
    }

    public void SetFound(bool found)
    {
        foundHighlight.SetActive(!found);

    }
}