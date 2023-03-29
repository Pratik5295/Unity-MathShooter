using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    [SerializeField] private Player player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ground")
        {
            Debug.Log("On ground");
            player.SetGroundFlag(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            Debug.Log("Left ground");
            player.SetGroundFlag(false);
        }
    }
}
