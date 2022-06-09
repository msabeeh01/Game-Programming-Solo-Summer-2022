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


    private bool red;
    private bool blue = true;
    private bool yellow;
    private bool nothing;


    private Rigidbody2D rBody;
    private bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
        Debug.Log(red);
    }

    // Update is called once per frame
    void Update()
    {
        //ColorCheck();
        //Debug.Log(colors[0]);
        GameObject[] redObjects = GameObject.FindGameObjectsWithTag("red");
        GameObject[] blueObjects = GameObject.FindGameObjectsWithTag("blue");
        GameObject[] yellowObjects = GameObject.FindGameObjectsWithTag("yellow");
        
        //if not red, ignore collision
        if(red == false){
            foreach (GameObject obj in redObjects) {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true); 
            }
        }
        else{
            foreach (GameObject obj in redObjects) {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false); 
            }
        }

        if(blue == false){
            foreach (GameObject obj in blueObjects) {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>(), true); 
            }
        }
        else{
            foreach (GameObject obj in blueObjects) {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false); 
            }
        }
    }


    void ChangeRed(){
        red = true;
        blue = false;
        yellow = false;
    }

    void ChangeBlue(){
        red = false;
        blue = true;
        yellow = false;
    }


    //For Physics
    private void FixedUpdate() {

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

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "changeRed")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            red = true;
            yellow = false;
            blue=false;
            Debug.Log(red);
        }

        if (other.gameObject.tag == "changeBlue")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            red = false;
            yellow = false;
            blue = true;
            Debug.Log("colour isa now blue");
            Debug.Log(red);
        }

        
    }

}
