using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Serialize keeps property private (cannot be changed from other scripts) but allows unity accesses it
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float jumpForce = 500.0f;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private LayerMask whatIsGround;

    private Rigidbody2D rBody;
    private bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update Time" + Time.deltaTime);
    }


    //For Physics
    private void FixedUpdate() {
        Debug.Log("Fixed Update Time" + Time.deltaTime);

        float horiz = Input.GetAxis("Horizontal");
        isGrounded = GroundCheck();

        //jump
        if(isGrounded && Input.GetAxis("Jump") > 0){
            rBody.AddForce(new Vector2(0.0f, jumpForce));
            isGrounded = false;
        }

        //change horiz axis by speed
        rBody.velocity = new Vector2(horiz * speed, rBody.velocity.y);
    }

    //Player Ground Check
    private bool GroundCheck(){
        //creates a circle which uses given funcitons to check if grounded
        return Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, whatIsGround);
    }
}
