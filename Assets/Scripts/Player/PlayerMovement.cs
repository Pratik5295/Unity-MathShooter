using UnityEngine;

public class PlayerMovement : MonoBehaviour
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
    [SerializeField] private Transform leftPosition;
    [SerializeField] private Transform rightPosition;

    [SerializeField] private float speed;

    [SerializeField] private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Movement();
        
        ShootControls();
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


    private void Movement()
    {
        Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        Vector3 finalPosition = transform.position + m_Input;
        Vector3 movePosition;

        if (leftPosition.position.x <= transform.position.x &&
            rightPosition.position.x >= transform.position.x)
        {
            if(finalPosition.x < leftPosition.position.x)
            {
                movePosition = leftPosition.position;
            }
            else if(finalPosition.x > rightPosition.position.x)
            {
                movePosition = rightPosition.position;
            }
            else
            {
                movePosition = finalPosition;
            }
            transform.position = Vector3.MoveTowards(transform.position, movePosition, Time.deltaTime * speed);

        }
    }

    private void ShootControls()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Player is shooting");
        }
    }
}
