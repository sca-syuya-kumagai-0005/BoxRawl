using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefault : MonoBehaviour
{
    float posy;
    float posx;
    Rigidbody2D rb;
    bool OnGround;

    public float speed;
    bool right;
    int dir;

    public int EnemyCheck;
    bool Jump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        OnGround = false;
        right = false;
        dir = 1;
        Jump = false;
    }

    // Update is called once per frame
    void Update()
    {
        posy = transform.position.y;
        posx = transform.position.x;
        posx += speed * Time.deltaTime*dir;
        transform.position = new Vector3(posx, posy);
        if (OnGround == false)
        {
            Vector2 myGravity = new Vector2(0, -9.81f/2*Time.timeScale);
            rb.AddForce(myGravity);
        }
        if (right)
        {
            dir = 1;
        }
        else
        {
            dir = -1;
        }
        if (Jump)
        {
            rb.velocity = new Vector3(0, 20, 0);
            Jump = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            
            if (EnemyCheck == 1)
            {
                transform.position = new Vector3(posx, posy + 0.01f);
                Jump = true;
                OnGround = false;
            }
            else
            {
                transform.position = new Vector3(posx, posy);
                OnGround = true;
            }
        }

        if (other.gameObject.CompareTag("Drop"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            transform.position = new Vector3(posx, posy-0.01f);
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            if (EnemyCheck == 1)
            {
                Jump = true;
            }
            if (right == false)
            {
                right = true;
            }
            else
            {
                right = false;
            }
        }
    }
}
