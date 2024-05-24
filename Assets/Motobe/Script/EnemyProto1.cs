using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProto1 : MonoBehaviour
{
    float posy;
    float posx;
    Rigidbody2D rb;
    [SerializeField] bool OnGround;
    [SerializeField] bool OnWall;

    public float speed;
    float defaultSpeed;
    bool right;
    int dir;

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
        SpawnDraw();
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
                Vector2 myGravity = new Vector2(0, -9.81f * 200 * Time.deltaTime);
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
                rb.velocity = new Vector3(0, 13, 0);
                Jump = false;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {

        if (PlayerMove.PlayerDead)
        {
            return;
        }

        if (other.gameObject.CompareTag("Ground"))
        {
                transform.position = new Vector3(posx, posy);
                OnGround = true;

            
            if (speed != defaultSpeed)
            {
                speed = defaultSpeed;
            }
        }
        if (other.gameObject.CompareTag("Drop"))
        {
            var sequence = DOTween.Sequence();
            EXPController.EXP += 3 * PlayerMove.EXPUP;
            PlayerMove.EXPUP += 1;
            sequence.AppendCallback(() => dest());
        }

        if (other.gameObject.CompareTag("DestroyObj"))
        {
            Destroy(this.gameObject);
        }
    }

    public void dest()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (PlayerMove.PlayerDead)
        {
            return;
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            transform.position = new Vector3(posx, posy - 0.2f);
            transform.position = new Vector3(posx, posy - 0.2f);
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            OnGround = true;
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            OnWall = true;
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
        if (PlayerMove.PlayerDead)
        {
            return;
        }
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Button"))
        {
            OnGround = false;
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            OnWall = false;
        }
    }
    private void ObjectEnemy(float x, float y)
    {
        GameObject Enemy_prefab = Resources.Load<GameObject>("EnemyProto1-1");
        GameObject Enemy = Instantiate(Enemy_prefab, new Vector3(x, y, 0), Quaternion.identity);
        Enemy.transform.parent = this.transform;
        return;
    }
    void SpawnDraw()
    {
        int random = Random.Range(0, 17);
        int rand=0;
        if (random <= 3){//0123
            rand = 3;
        } 
        else if(random<=10)//45678910
        {
            rand = 4;
        }
        else if (random <= 15)//1112131415
        {
            rand = 5;
        }
        else if (random == 16)//
        {
            rand = 6;
        }
        Debug.Log(random);
        for (int i = 1; i < rand; i++)
        {
            ObjectEnemy(this.transform.position.x, this.transform.position.y + i);
        }
        
    }
}
