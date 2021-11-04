using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{

    [SerializeField] CharacterController characterController;
    [SerializeField] Animator animator;
    [SerializeField] Transform groundChecker;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] float speed;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float jumpHeight = 3;

    float xInput;
    Vector3 velocity;
    bool isGrounded;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        xInput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.fixedDeltaTime;
        characterController.Move(velocity * Time.deltaTime);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        #region FACING DIRECTION
        // Character Facing Direction Logic
        if (xInput < 0)
        {
            this.transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (xInput > 0)
        {
            this.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        #endregion
        #region ANIMATIONS
        //Walking Animation Logic
        if (xInput != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        #endregion
    }

    private void FixedUpdate()
    {
        #region CHARACTER MOVEMENT
        // Character Movement Logic
        Vector3 move = new Vector3(xInput, 0, 0);
        characterController.Move( move * Time.fixedDeltaTime * speed);
        #endregion
    }
}
