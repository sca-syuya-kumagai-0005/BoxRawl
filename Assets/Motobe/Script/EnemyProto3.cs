using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProto3 : MonoBehaviour
{
    float posy;
    float posx;
    Rigidbody2D rb;
    [SerializeField] bool OnGround;
    [SerializeField] bool OnWall;
    bool Rota;
    int rota;

    public float speed;
    float defaultSpeed;
    bool right;
    int dir;

    public int EnemyCheck;
    bool Jump;

    public GameObject EnemySkin;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        OnGround = false;
        right = false;
        dir = 1;
        Jump = false;
        defaultSpeed = speed;
        int random = Random.Range(0, 4);
        EnemyCheck = random;
        Rota = true;
    }

    // Update is called once per frame
    void Update()
    {
        posy = transform.position.y;
        posx = transform.position.x;
        posx += speed * Time.deltaTime * dir;
        transform.position = new Vector3(posx, posy);

        if (!OnGround)
        {
            Vector2 myGravity = new Vector2(0, -9.81f * 50 * Time.deltaTime);
            rb.AddForce(myGravity);
        }
        if (right)
        {
            dir = 1;
            rota = -1;
        }
        else
        {
            dir = -1;
            rota = 1;
        }
        if (Jump)
        {
            rb.velocity = new Vector3(0, 13, 0);
            Jump = false;
        }

        if (Rota)
        {
            EnemySkin.transform.Rotate(0, 0, 750 * rota * Time.deltaTime);
        }
        if (!Rota)
        {
            EnemySkin.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Ground"))
        {
            transform.position = new Vector3(posx, posy);
            OnGround = true;
            Jump = true;
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                if (right)
                {
                    right = false;
                }
                else
                {
                    right = true;
                }
            }
        }

        if (other.gameObject.CompareTag("Drop"))
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("DestroyObj"))
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            OnWall = true;
                rb.velocity = new Vector3(0, 20, 0);
                OnGround = false;
                speed += 2;
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
