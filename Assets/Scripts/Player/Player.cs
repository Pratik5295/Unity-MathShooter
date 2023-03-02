using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    ///<summary>
    ///
    /// Class for detecting player movement from player controls
    /// 
    /// For testing:
    /// Keyboard controls A and D for left and right, Space button to shoot
    /// 
    /// </summary>
    /// 

    #region Player Variables
    [SerializeField] private Transform leftPosition;
    [SerializeField] private Transform rightPosition;

    [SerializeField] private float speed;

    [SerializeField] private Rigidbody rb;

    [SerializeField] private Projectile projectilePrefab;

    [SerializeField] private GameObject shootPoint;

    [SerializeField] private float projectileSpeed; //Only for testing purposes: To be determined from code, otherwise prefabs will handle it

    //Temporary health variable, TODO: Work on it later to improve system
    [SerializeField] private float health;
    #endregion

    #region Player Manager Variables
    public static Player Instance;
    public float fireRate;
    public float playerDamage;
    [SerializeField] private float fireRateCounter;

    //Events for fireRate and playerDamage Update
    public Action fireRateUpdateEvent;
    public Action playerDamageUpdateEvent;
    #endregion

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (fireRateCounter >= fireRate)
        {
            ShootControls();
        }

        fireRateCounter += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        RigidBodyMovement();
    }

    private void RigidBodyMovement()
    {
        Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        //Apply the movement vector to the current position, which is
        //multiplied by deltaTime and speed for a smooth MovePosition
        Vector3 movePosition;

        Vector3 finalPosition = transform.position + m_Input * Time.deltaTime * speed;

        if (leftPosition.position.x <= transform.position.x &&
            rightPosition.position.x >= transform.position.x)
        {
            if (finalPosition.x < leftPosition.position.x)
            {
                movePosition = Vector3.zero;
            }
            else if (finalPosition.x > rightPosition.position.x)
            {
                movePosition = Vector3.zero;
            }
            else
            {
                movePosition = m_Input * Time.deltaTime * speed; ;
            }
            rb.MovePosition(transform.position + movePosition);
        }


    }

    private void ShootControls()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Player is shooting");
            ShootProjectile();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Dead!!");
        }
    }

    private void ShootProjectile()
    {
        Projectile projectile = Instantiate(projectilePrefab, shootPoint.transform.position, Quaternion.identity);
        projectile.SetDamage(playerDamage);

        projectile.SetSpeed(projectileSpeed);

        fireRateCounter = 0;
    }

    public void SetDamage(float amount)
    {
        playerDamage += amount;

        playerDamageUpdateEvent?.Invoke();
    }

    public void SetFireRate(float _fireRate)
    {
        fireRate -= _fireRate;

        if (fireRate <= 0.2f)
        {
            Debug.Log("Reached maxed rate");
            fireRate = 0.1f;
            fireRateUpdateEvent?.Invoke();
        }
    }
}
