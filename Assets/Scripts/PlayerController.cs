using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float jumpForce;
    public float shootForce;    
    private Rigidbody2D rb2d;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public GameObject bullet;

    void Start () {
        rb2d = GetComponent<Rigidbody2D>();

    }

    void FixedUpdate () {
        float move = Input.GetAxis("Horizontal");
        rb2d.velocity = new Vector2(move * speed, rb2d.velocity.y);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() == true)
        {
            // rb2d.AddForce(new Vector2(0, jumpForce));
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }

        if (Input.GetMouseButtonDown(0))
        {
            shoot();
            Debug.Log("click");
        }
    }

    bool IsGrounded()
    {

        // Peut etre que j'ai inversé positionleft/right
        Vector2 positionLeft = new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x / 2), transform.position.y);
        Vector2 positionRight = new Vector2(transform.position.x - (GetComponent<SpriteRenderer>().bounds.size.x / 2), transform.position.y);


        Vector2 direction = Vector2.down;
        float distance = 0.5f;

        RaycastHit2D hitLeft = Physics2D.Raycast(positionLeft, direction, distance, groundLayer);
        RaycastHit2D hitRight = Physics2D.Raycast(positionRight, direction, distance, groundLayer);

        if (hitLeft.collider != null || hitRight.collider != null)
        {
            return true;
        }
        return false;
    }

    void shoot()
    {
        Vector3 shootDirection;
        shootDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Debug.Log("shoot x = " + shootDirection.x);
        Debug.Log("shoot y = " + shootDirection.y);



        GameObject bull = Instantiate(bullet, transform.position, transform.rotation);
        Vector2 direction = new Vector2(shootDirection.x - transform.position.x, shootDirection.y - transform.position.y);
        direction.Normalize();   
        bull.GetComponent<Rigidbody2D>().AddForce(direction * shootForce);



    }
}

