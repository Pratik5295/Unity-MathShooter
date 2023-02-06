using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private float speed;
    private void Update()
    {
        transform.Translate(-Vector3.forward * Time.deltaTime * speed);

    }

    private void OnTriggerEnter(Collider other)
    {
        //Only detect trigger when its a player
        if(other.gameObject.tag == "Player")
        {
            OnPlayerTriggered();
        }
    }

    protected virtual void OnPlayerTriggered()
    {
        Debug.Log("Player has entered in the parent class");
        OnDestroyEvent();
    }

    public void OnDestroyEvent()
    {
        Destroy(gameObject);
    }
}
