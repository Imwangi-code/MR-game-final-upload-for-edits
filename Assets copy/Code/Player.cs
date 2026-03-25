using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerFPSController : MonoBehaviour
{
    int moveSpeed = 5; // how fast the player moves
    float lookSpeedX = .2f; // left/right mouse sensitivity
    float lookSpeedY = .1f; // up/down mouse sensitivity
    int jumpForce = 300; // ammount of force applied to create a jump

    Transform camTrans; // a reference to the camera transform
    float xRotation;
    float yRotation;
    Rigidbody _rigidbody;
    InputAction MoveAction;
    InputAction LookAction;
    InputAction JumpAction;

    //The physics layers you want the player to be able to jump off of. Just dont include the layer the palyer is on.
    public LayerMask groundLayer;
    //public Transform feetTrans; //Position of where the players feet touch the ground
    float groundCheckDist = .35f; //How far down to check for the ground. The radius of Physics.CheckSphere
    public bool grounded = false; //Is the player on the ground

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Hides the mouse and locks it to the center of the screen.

        camTrans = Camera.main.transform; //Get a reference to the main camera.
        _rigidbody = GetComponent<Rigidbody>(); // Using GetComponent is expensive. Always do it in start and chache it when you can.

        MoveAction = InputSystem.actions.FindAction("Move"); //Find the set of actions in the input system called "Move". Assign it to MoveAction
        LookAction = InputSystem.actions.FindAction("Look");
        JumpAction = InputSystem.actions.FindAction("Jump");
    }

    void FixedUpdate()
    {
        //Creates a movement vector local to the direction the player is facing.
        Vector3 moveDir = transform.forward * MoveAction.ReadValue<Vector2>().y + transform.right * MoveAction.ReadValue<Vector2>().x; //Use Move X and Y for forward and strafing movement
        moveDir *= moveSpeed;
        moveDir.y = _rigidbody.linearVelocity.y; // We dont want y so we replace y with that the _rigidbody.velocity.y already is.
        _rigidbody.linearVelocity = moveDir; // Set the velocity to our movement vector

        //The sphere check draws a sphere like a ray cast and returns true if any collider is withing its radius.
        //grounded is set to true if a sphere at feetTrans.position with a radius of groundCheckDist detects any objects on groundLayer within it
        grounded = Physics.CheckSphere(transform.position, groundCheckDist, groundLayer);
    }

    void Update()
    {
        yRotation += LookAction.ReadValue<Vector2>().x * lookSpeedX;
        xRotation -= LookAction.ReadValue<Vector2>().y * lookSpeedY; //Y is inverted
        xRotation = Mathf.Clamp(xRotation, -90, 90); //Keeps up/down head rotation realistic

        camTrans.localEulerAngles = new Vector3(xRotation, 0, 0); //Rotate just the head up and down
        transform.eulerAngles = new Vector3(0, yRotation, 0);  //Rotate the whole body left and right

        if (grounded && JumpAction.WasPressedThisFrame()) //if the player is on the ground and press Spacebar
        {
            _rigidbody.linearVelocity = Vector3.zero; //Zero out gravity before applying the jumpForce
            _rigidbody.AddForce(new Vector3(0, jumpForce, 0)); // Add a force jumpForce in the Y direction
        }
    }
}
