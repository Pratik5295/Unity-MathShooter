using UnityEngine;

public class Damage_Pickup : Pickup
{
    [SerializeField] private float damage;
    protected override void OnPlayerTriggered()
    {
        Debug.Log("Damage pick up");
        Player.Instance.SetDamage(damage);

        OnDestroyEvent();
    }
}
