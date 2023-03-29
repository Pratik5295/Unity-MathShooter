using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[DefaultExecutionOrder(-2)]
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
    private Vector3 middlePosition;
    [SerializeField] private Transform leftPosition;
    [SerializeField] private Transform rightPosition;

    [SerializeField] private float speed;

    [SerializeField] private float jumpForce;

    [SerializeField] private bool onGround;

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

    //For Restart Values
    private float defaultFireRate;
    private float defaultPlayerDamage;
    private float defaultFireRateCounter;


    //Events for fireRate and playerDamage Update
    public Action fireRateUpdateEvent;
    public Action playerDamageUpdateEvent;
    #endregion

    #region Player Movement
    public enum POSITION 
    { 
        MIDDLE = 0,
        LEFT = 1,
        RIGHT = 2
    }

    [SerializeField] private POSITION Place;

    #endregion

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            defaultFireRate = fireRate;
            defaultPlayerDamage = playerDamage;
            defaultFireRateCounter = fireRateCounter;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        middlePosition = transform.position;
        Place = POSITION.MIDDLE;

        
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
    public void PlayerMove(bool goLeft) //Consider swipe left as a bool, for swipe right it would be false
    {
        switch (Place)
        {
            case POSITION.MIDDLE:
                if (goLeft)
                {
                    //transform.position = leftPosition.position;
                    Place = POSITION.LEFT;
                    transform.position = Vector3.MoveTowards(transform.position, leftPosition.position, speed);
                }
                else
                {
                    transform.position = rightPosition.position;
                    Place = POSITION.RIGHT;
                }
                break;

            case POSITION.LEFT:
                if (!goLeft)
                { 
                        transform.position = middlePosition;
                        Place = POSITION.MIDDLE;
                }
                break;

            case POSITION.RIGHT:
                if(goLeft)
                {
                    transform.position = middlePosition;
                    Place = POSITION.MIDDLE;
                }
                break;
        }
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

    public void Shoot()
    {
        //This is from mobile
        if (fireRateCounter >= fireRate)
        {
            ShootProjectile();
        }
    }

    public void SetGroundFlag(bool flag)
    {
        onGround = flag;
    }
    public void Jump()
    {
        if (!onGround) return;
        rb.AddForce(Vector3.up * jumpForce);
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

    public void ResetGameValues()
    {
        fireRate = defaultFireRate;
        playerDamage = defaultPlayerDamage;
        fireRateCounter = defaultFireRateCounter;
        Time.timeScale = 1;
    }
}
