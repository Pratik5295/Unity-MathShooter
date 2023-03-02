using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;

    [SerializeField] private float timeLimit;
    [SerializeField] private float timeCounter;

    [SerializeField] private float speed;

    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if(timeCounter >= timeLimit)
        {
            //Destroy this object as it hit nothing
            Destroy(this.gameObject);
        }

        else
        {
            Vector3 offset = Vector3.forward.normalized * Time.deltaTime * speed;
            rb.MovePosition(transform.position + offset);
            timeCounter += Time.deltaTime;
        }
    }

    public void SetDamage(float _dmg)
    {
        damage = _dmg;
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }
}
