using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
#pragma warning disable 649
    //Player Movement
    CharacterController controller;
    Vector2 horizontalInput;
    float smoothTime = 0.05f;
    float currentVelocity;

    //Gravity
    public float gravity = -30; // -9.81
    [SerializeField] float speed;
    Vector3 verticalVelocity = Vector3.zero;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    bool jumped;
    public bool isWalking;
    Vector3 direction;

    //Reference
    MainMenu _mainMenu;
    InputManager _inputManager;

    void Awake()
    {
        //References
        controller = GetComponent<CharacterController>();
        _inputManager = GetComponent<InputManager>();
        _mainMenu = GameObject.Find("Canvas").GetComponent<MainMenu>();
    }
    

    void Update()
    {
        if(!_mainMenu.paused)
        {
            Move();            
            Jump();
        } 
    }

    void Move()
    {
        if(horizontalInput.sqrMagnitude == 0) return;
        direction = new Vector3(horizontalInput.x, 0.0f, horizontalInput.y);
        controller.Move(direction* speed * Time.deltaTime);

        var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        
        FindObjectOfType<AudioManager>().Play("Footsteps");
    }


   public void ReceiveInput(Vector2 _horizontalInput)
   {
        horizontalInput = _horizontalInput;
   }
   
   void Jump()
   {
        //Checks if ground is touched
        float halfHeight = controller.height * 0.5f;
        var bottomPoint = transform.TransformPoint(controller.center - Vector3.up * halfHeight);
        isGrounded = Physics.CheckSphere(bottomPoint, 0.1f, groundMask);
        if(isGrounded)
        {
            verticalVelocity.y = 0;
        }

        //Jump
        if(jumped)
        {
            if(isGrounded)
            {
               
                verticalVelocity.y = Mathf.Sqrt(-2 * 3.5f * gravity);
            }
            jumped = false;
        }

        //Moves player verticaly (gravity)
        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
   }

   public void OnJumpPressed()
   {
        if(isGrounded)
        {
           jumped = true;
           FindObjectOfType<AudioManager>().Play("Jump");
        }
   }

//    void OnTriggerEnter(Collider other)
//    {
//         if(other.gameObject.CompareTag("Zombie"))
//         {
//             other.GetComponent<EnemyBehaviour>().OnAware();
//         }
//    }
}
