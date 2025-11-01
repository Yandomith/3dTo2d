
using UnityEngine;

namespace Assets.Script.Player
{
    public class WaterArea : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {

            Debug.Log("WaterArea: OnTriggerEnter with " + other.name);
            if (other.CompareTag("Player"))
            {
                PlayerStateMachine player = other.GetComponent<PlayerStateMachine>();
                if (player != null)
                    player.currentWaterSurfaceY = (GetComponent<Renderer>().bounds.center.y + GetComponent<Renderer>().bounds.extents.y)- 0.3f;
            }
        }
    }
}