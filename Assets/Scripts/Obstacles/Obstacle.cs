using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float cost;

    [SerializeField] private float health;

    public Action OnDestroyEvent;

    private void Start()
    {
        OnDestroyEvent += OnDestroyEventHandler;
    }

    private void OnDisable()
    {
        OnDestroyEvent -= OnDestroyEventHandler;
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Player hit me");
        }
        else if(collision.gameObject.tag == "Projectile")
        {
            Projectile attacker = collision.gameObject.GetComponent<Projectile>();
            if (attacker != null)
            {
                DamageDealt(attacker.damage);
            }
        }
    }

    private void DamageDealt(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            OnDestroyEvent?.Invoke();
        }
    }

    public void SetHealth(float amount)
    {
        health = amount;
    }

    private void OnDestroyEventHandler()
    {
        ScoreManager.Instance.AddToScore(cost);
        Destroy(this.gameObject);
    }
}
