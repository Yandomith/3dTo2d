using UnityEngine;

public class PetCollectable : MonoBehaviour {
    public PetData petData;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            PetManager.Instance.FindPet(petData);
            Destroy(gameObject);
        }
    }
}