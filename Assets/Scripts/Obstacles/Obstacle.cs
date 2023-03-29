using System;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float cost;    //The health with which the object was created

    [SerializeField] private float health; 

    [SerializeField] private TMP_Text healthText;

    public Action OnDestroyEvent;

    private void Start()
    {
        OnDestroyEvent += OnDestroyEventHandler;
        UpdateHealthText();
    }

    private void OnDisable()
    {
        OnDestroyEvent -= OnDestroyEventHandler;
    }
    private void Update()
    {
        transform.Translate(-Vector3.forward * Time.deltaTime * speed);

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Player hit me");
            GameManager.instance.OnObstacleHitDestroyer(health);
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.tag == "Projectile")
        {
            Projectile attacker = collision.gameObject.GetComponent<Projectile>();
            if (attacker != null)
            {
                DamageDealt(attacker.damage);
            }
        }
        else if(collision.gameObject.tag == "Destroyer")
        {
            Debug.Log("Collided with destroyer!!");
            GameManager.instance.OnObstacleHitDestroyer(health);
            Destroy(this.gameObject);
        }
    }

    private void DamageDealt(float damage)
    {
        health -= damage;
        UpdateHealthText();
        if (health <= 0)
        {
            OnDestroyEvent?.Invoke();
        }
    }

    public void SetHealth(float amount, float playerDamage,float diffcultyQuotient)
    {
        float index = Random.Range(-playerDamage, playerDamage);
        health = diffcultyQuotient * (amount + index);
        health = Mathf.Round(health);

        if (health <= 1)
        {
            health = amount;
        }

        cost = health;
    }

    private void OnDestroyEventHandler()
    {
        ScoreManager.Instance.AddToScore(cost);
        Destroy(this.gameObject);
    }



    #region UI Functions

    private void UpdateHealthText()
    {
        healthText.text = health.ToString();
    }
    #endregion
}
