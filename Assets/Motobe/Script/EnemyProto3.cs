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
    bool OnCeiling;
    bool speedCheck;
    float speedCount;

    public GameObject EnemySkin;

    // Start is called before the first frame update
    void Start()
    {
        speedCheck = false;
        speedCount = 0;
        rb = GetComponent<Rigidbody2D>();
        OnGround = false;
        right = false;
        dir = 1;
        Jump = false;
        defaultSpeed = speed;
        Rota = true;
        OnCeiling = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMove.PlayerDead)
        {
            rb.velocity = new Vector2(0, 0);
            return;
        }
        if (!PlayerMove.PlayerDead)
        {
            posy = transform.position.y;
            posx = transform.position.x;
            posx += speed * Time.deltaTime * dir;
            transform.position = new Vector3(posx, posy);

            if (!OnGround)
            {
                Vector2 myGravity = new Vector2(0, -9.81f * 25 * Time.deltaTime);
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
                speedCheck = true;
                Jump = false;
            }
            if (speedCheck)
            {
                speedCount += 1 * Time.deltaTime;
                if (speedCount > 2 || OnCeiling)
                {
                    speed = 10;
                    speedCheck = false;
                    speedCount = 0;
                }
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
    }
    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Ground"))
        {
            speed = defaultSpeed;
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
            EXPController.EXP += 5 * PlayerMove.EXPUP;
            PlayerMove.EXPUP += 1;
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("DestroyObj"))
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            OnWall = true;
            OnGround = false;
            if (right == false)
            {
                right = true;
            }
            else
            {
                right = false;
            }
        }
        if (other.gameObject.CompareTag("Ceiling"))
        {
            transform.position = new Vector3(posx, posy);
            transform.position = new Vector3(posx, posy);
            OnCeiling = true;
            Vector2 myGravity = new Vector2(0, -9.81f * 200 * Time.deltaTime);
            rb.AddForce(myGravity);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Button"))
        {
            speed = defaultSpeed;
            Rota = true;
            OnGround = false;
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            if (!OnCeiling)
            {
                Rota = true;
                OnWall = false;
            }
            
        }
        if (other.gameObject.CompareTag("Ceiling"))
        {
            OnCeiling = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            speed = defaultSpeed;
            transform.position = new Vector3(posx, posy - 0.2f);
            transform.position = new Vector3(posx, posy - 0.2f);
            Rota = false;
        }
        
        if (other.gameObject.CompareTag("Ground"))
        {
            OnGround = true;
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            if (!OnCeiling)
            {
                OnWall = true;
                OnGround = false;
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
}
