using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
#pragma warning disable 649
    //Player Movement
    CharacterController controller;
    Vector2 horizontalInput;

    //Gravity
    float gravity = -30; // -9.81
    [SerializeField] float speed;
    Vector3 verticalVelocity = Vector3.zero;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    bool jumped;
    public bool isWalking;

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
            Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
            controller.Move(horizontalVelocity * Time.deltaTime);

            if(isWalking) { FindObjectOfType<AudioManager>().Play("Footsteps"); }
            
            Jump();
        } 
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
