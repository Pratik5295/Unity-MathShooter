using UnityEngine;

public class FireRate_Pickup : Pickup
{
    [SerializeField] private float fireRate;
    protected override void OnPlayerTriggered()
    {
        Debug.Log("FireRate pick up");
        Player.Instance.SetFireRate(fireRate);

        OnDestroyEvent();
    }
}
