using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
//EnemyCheckの4のキャラがバグってる
public class EnemyDefault : MonoBehaviour
{
    float posy;
    float posx;
    Rigidbody2D rb;
    [SerializeField]bool OnGround;
    [SerializeField]bool OnWall;
    [SerializeField] bool hitPlayer;
    [SerializeField] private bool hitDirUp;
    [SerializeField] private bool hitDirDown;
    [SerializeField] private bool hitDirRight;
    [SerializeField] private bool hitDirLeft;
    Vector3 target;
    private float momongaUp=15;
    private bool momongaUpFlag;
    private bool momongaDownFlag;
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
   
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        momongaUpFlag = false ;
      
        momongaDownFlag = true ;
        OnGround = false;
        right = false;
        dir = 1;
        Jump = false;
        defaultSpeed = speed;
        int random = Random.Range(0, 6);
        player=GameObject.Find("Player").gameObject;
        target = player.transform.position-this.transform.position;
        EnemyCheck = random;
        Rota = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemyCheck<4)
        {
            posy = transform.position.y;
            posx = transform.position.x;
            posx += speed * Time.deltaTime * dir;
            transform.position = new Vector3(posx, posy);
        }
        
        if (!OnGround&&EnemyCheck<4)
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
        MomongaEnemy();
    }


    private void UnGravityEnemy()
    {
        if (EnemyCheck == 4)
        {
            if(OnWall||OnGround)
            {
                HitRay(Vector3.left);
                HitRay(Vector3.up);
                HitRay(Vector3.right);
                HitRay(Vector3.down);
            }
            if (OnGround)
            {
               target = player.transform.position - this.gameObject.transform.position;//EnmeyからPlayerへのベクトル
                target = target.normalized;//ベクトルの正規化
                target.y = 0;
                GameObject obj = this.gameObject;
                float y = obj.transform.position.y;
                if(obj.transform.position.y<player.transform.position.y)
                {
                    y += 1;
                    this.gameObject.transform.position = new Vector3(obj.transform.position.x, y, 0);
                }
                transform.position += target * speed * Time.deltaTime;
                Rota = false;
            
            }
            if(OnWall)
            {
                target = player.transform.position - this.gameObject.transform.position;//EnmeyからPlayerへのベクトル
                target = target.normalized;//ベクトルの正規化
                target.x = 0;
                transform.position += target * speed * Time.deltaTime;
                Rota = false;
              
            }
            if(!OnWall&&!OnGround)
            {
                target = player.transform.position - this.gameObject.transform.position;//EnmeyからPlayerへのベクトル
                target = target.normalized;//ベクトルの正規化
                transform.position += target * speed * Time.deltaTime;
               
            }
        }
    }

    private void MomongaEnemy()
    {
        if(EnemyCheck == 5)
        {
            HitRay(Vector3.up);
            if (this.transform.position.y > momongaUp||hitDirUp)
            {
                momongaUpFlag = false;
                momongaDownFlag = true;
            }
            if (momongaDownFlag)
            {
                Debug.Log("プレイヤーの位置を獲得しました");
                target = player.transform.position - this.gameObject.transform.position;//EnmeyからPlayerへのベクトル
                target = target.normalized;//ベクトルの正規化
                momongaDownFlag = false;
            }
            if (OnGround || hitPlayer)
            {
                momongaUpFlag = true;
                Rota = false;
                Debug.Log("地面と接触しました");
            }
            if(OnWall)
            {
                target.x = 0;
                Rota = false;
            }

            if(momongaUpFlag)
            {
                Debug.Log("上昇を始めました");
                Vector3 pos=this.transform.position;
                target.x = 0;
                pos.y += speed * Time.deltaTime;
                this.transform.position=new Vector3(pos.x,pos.y,pos.z);
            }
            else
            {
                transform.position += target * speed * Time.deltaTime;
            }
         
        }
       
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("プレイヤーと接触しました");
            hitPlayer = true;
          
        }
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

        if (other.gameObject.CompareTag("DestroyObj"))
        {
            Destroy(this.gameObject);
        }
    }
  
    private void OnTriggerEnter2D(Collider2D other)
    {

      
        if (other.gameObject.CompareTag("Ground") && EnemyCheck <= 4)
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
            Debug.Log("地面から離れました");
        }
        if(other.gameObject.CompareTag("Wall"))
        {
            Rota = true;
            OnWall = false;
            Debug.Log("壁から離れました");
        }
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("プレイヤーから離れました");
            hitPlayer = false;
            Rota = true;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
      if(collision.gameObject.CompareTag("Ground")||collision.gameObject.CompareTag("Wall"))
        {
                
                Rota = false;
        }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall"))
        {
          
            Rota = false;
        }

        if (collision.gameObject.CompareTag("DestroyObj"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Wall")||collision.gameObject.CompareTag("Player"))
        {
            Rota = true;
        }

    }

    private void HitRay(Vector3 dir)
    {
        RaycastHit2D hit=
        Physics2D.Raycast(this.transform.position, dir,1);
        if (hit.collider != null)
        {
            switch (dir)
            {
                case Vector3 v when v.Equals(Vector3.down):
                    {
                        hitDirDown = true;
                    }
                    break;
                case Vector3 v when v.Equals(Vector3.up):
                    {
                       hitDirUp = true;
                    }
                    break;
                case Vector3 v when v.Equals(Vector3.right):
                    {
                       hitDirRight = true;
                    }
                    break;
                case Vector3 v when v.Equals(Vector3.left):
                    {
                       hitDirLeft = true;
                    }
                    break;
            }
            
        }
        else
        {
            hitDirDown=false;   
            hitDirLeft=false;
            hitDirRight=false;
            hitDirUp=false; 
        }
       
    }
}
