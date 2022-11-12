using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public float force = 200f;
    public float jumpForce = 1000f;
    private bool isJumping;


    private float inputHorizontal;
    private float inputVertical;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        isJumping = false;
    }


    void Update(){ 
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
    }


    void FixedUpdate() {
        
        // Move character Horizontal

        if(inputHorizontal != 0){
            rb.AddForce(new Vector2(inputHorizontal * force, 0f));
            animator.SetBool("isWalking", true);
        } else {
            animator.SetBool("isWalking", false);
        }

        // Flip character

        if(inputHorizontal > 0){
            spriteRenderer.flipX = false;
        } else if(inputHorizontal < 0){
            spriteRenderer.flipX = true;
        }


        // Jump character

        if( (Input.GetKey(KeyCode.Space)) && (!isJumping)){
            //isJumping = true;
            rb.AddForce(transform.up * jumpForce);
        }
            
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Platform"){
            isJumping = false;
        }
    }

        void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Platform"){
            isJumping = true;
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Enemy")
            animator.SetBool("gotHit", true);
    }


}
