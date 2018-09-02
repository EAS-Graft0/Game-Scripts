using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed = 6f;

    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidBody;
    int floorMask;
    float camRayLength = 100f;
    //declare vars

    void Awake()
    {
        //assign vars
        floorMask = LayerMask.GetMask("Floor");

        anim = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //sets horizontal and vertical vars if the buttons are pressed
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);
    }
    //moves player based on horizontal and vertical buttons
    void Move(float h, float v)
    {
        //sets movement var which is a vector3
        movement.Set(h, 0f, v);
        //sets the movement var to normalized against speed and time to make it seamless
        movement = movement.normalized * speed * Time.deltaTime;
        //applies moevment to player
        playerRigidBody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Creates a ray cast, line of light from camera to an mouse point
        RaycastHit floorHit;
        //Return of the raycast hitting something

        if(Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
            //callsed physics raycast function to say if our raycast line of light's return(floorhit) hits floorMask within the camyray lenght which is 100
        {
            Vector3 playerToMouse = floorHit.point - transform.position;

            playerToMouse.y = 0f;
            //way to store rotation - stops player falling forward
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidBody.MoveRotation(newRotation);
            //applies rotation to player
        }
    }

    void Animating(float h, float v)
    {
        Debug.Log("Moving");

        //sets the boolean var of walknig if horizontal or vertical isn't nothing
        bool walking = h != 0f || v != 0f;
        //sets the var "IsWalking" to our walking var bool to instantiate our walking animation
        anim.SetBool("IsWalking", walking);
    }

}
