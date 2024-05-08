using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefault : MonoBehaviour
{
    float posy;
    float posx;
    Rigidbody2D rb;
    bool OnGround;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        OnGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        posy = transform.position.y;
        posx = transform.position.x;
        transform.position = new Vector3(posx, posy);
        if (OnGround == false)
        {
            Vector2 myGravity = new Vector2(0, -2f*Time.timeScale);
            rb.AddForce(myGravity);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            transform.position = new Vector3(posx, posy);
            OnGround = true;
        }
        if (other.gameObject.CompareTag("Drop"))
        {
            Destroy(this.gameObject);
        }
    }
}
