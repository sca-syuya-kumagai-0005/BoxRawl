using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefault : MonoBehaviour
{
    float posy;
    float posx;
    Rigidbody2D rb;
    [SerializeField]bool OnGround;
    [SerializeField]bool OnWall;

    bool Rota;
    int rota;

    public float speed;
    float defaultSpeed;
    bool right;
    int dir;

    public int EnemyCheck;
    bool Jump;

    public GameObject EnemySkin;
    private GameObject player;
    private bool myIsTrigger;
    private Collider2D setColl;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        setColl = GetComponent<Collider2D>();
        setColl.isTrigger = true;
        OnGround = false;
        right = false;
        dir = 1;
        Jump = false;
        defaultSpeed = speed;
        int random = Random.Range(0, 4);
        player=GameObject.Find("Player").gameObject;
        if(player!=null )
        {
            Debug.Log(player);
        }
        EnemyCheck = random;
        Rota = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemyCheck!=4)
        {
            posy = transform.position.y;
            posx = transform.position.x;
            posx += speed * Time.deltaTime * dir;
            transform.position = new Vector3(posx, posy);
        }
      
        if (OnGround == false&&EnemyCheck!=4)
        {
            Vector2 myGravity = new Vector2(0, -9.81f*200*Time.deltaTime);
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
        UnGravityEnemy();
    }


    private void UnGravityEnemy()
    {
        if (EnemyCheck == 4)
        {
          
            if (OnGround)
            {
                Vector3 target = player.transform.position - this.gameObject.transform.position;//EnmeyからPlayerへのベクトル
                target = target.normalized;//ベクトルの正規化
                target.y = this.gameObject.transform.position.y;
                transform.position += target * speed * Time.deltaTime;
               
                Debug.Log("地面と接触しました");
            }
            else
            {
                Vector3 target = player.transform.position - this.gameObject.transform.position;//EnmeyからPlayerへのベクトル
                target = target.normalized;//ベクトルの正規化
                transform.position += target * speed * Time.deltaTime;
               
            }
           


        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (EnemyCheck == 0 || EnemyCheck == 3)
            {
                transform.position = new Vector3(posx, posy);
                OnGround = true;
            }

            if (EnemyCheck == 1)
            {
                transform.position = new Vector3(posx, posy + 0.01f * Time.deltaTime);
                Jump = true;
            }

            if (EnemyCheck == 2)
            {
                transform.position = new Vector3(posx, posy + 0.01f * Time.deltaTime);
                Jump = true;
                int random = Random.Range(0, 3);
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
            

            if (speed != defaultSpeed)
            {
                speed = defaultSpeed;
            }
        }

        if (other.gameObject.CompareTag("Drop"))
        {
            Destroy(this.gameObject);
        }
    }
  
    private void OnTriggerEnter2D(Collider2D other)
    {
      
        
        if (other.gameObject.CompareTag("Ground") && EnemyCheck != 4)
        {
          
            transform.position = new Vector3(posx, posy-0.2f);
            transform.position = new Vector3(posx, posy - 0.2f);
            Rota = false;
        }
        if(other.gameObject.CompareTag("Ground"))
        {
            OnGround = true;
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            OnWall = true;
            if (EnemyCheck == 1 || EnemyCheck == 2 || EnemyCheck == 3)
            {
                rb.velocity = new Vector3(0, 20, 0);
                OnGround = false;
                speed += 2;
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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground")||other.gameObject.CompareTag("Button"))
        {
            Rota = true;
            OnGround = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("プレイヤーと接触しました");
            setColl.isTrigger = false;
        }
        if(collision.gameObject.CompareTag("Ground")||collision.gameObject.CompareTag("Button"))
        {
            OnGround = true;
            //setColl.isTrigger = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
}
