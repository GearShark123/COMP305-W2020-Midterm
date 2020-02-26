using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // "Public" variables
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float jumpForce = 700.0f;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private float groundCheckRadius = 0.15f;

    // Private variables
    private bool isGrounded = false;
    private Rigidbody2D rBody;
    private Animator anim;
    private bool isFacingRight = true;
    private bool isDucking = false;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            isFacingRight = false;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            isFacingRight = true;
        }        
    }

    // Physics
    void FixedUpdate()
    {
        Vector2 movement = Vector2.zero;
        float horiz = Input.GetAxis("Horizontal");

        if (isFacingRight == false && rBody.velocity.x != 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);           
        }
        else if (isFacingRight == true && rBody.velocity.x != 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }

        isGrounded = GroundCheck();

        // Jumping code will go here!
        if (isGrounded && Input.GetAxis("Jump") > 0)
        {
            rBody.AddForce(new Vector2(0.0f, jumpForce));
            isGrounded = false;
        }

        rBody.velocity = new Vector2(horiz * speed, rBody.velocity.y);


        // Check if sprite is crouching
        if (isGrounded && rBody.velocity.x == 0 && Input.GetAxis("Vertical") < 0)
        {
            isDucking = true;
        }
        else
        {
            isDucking = false;
        }

        anim.SetFloat("xSpeed", Mathf.Abs(rBody.velocity.x));
        anim.SetFloat("ySpeed", rBody.velocity.y);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDucking", isDucking);
    }

    private bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, whatIsGround); ;
    }
}
