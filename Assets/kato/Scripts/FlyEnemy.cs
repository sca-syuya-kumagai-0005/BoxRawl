using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    public GameObject BulletPrefab;
    GameObject Bullet;
    Rigidbody2D BulletRg;
    Vector2 BulletDir;  //’e‚ð”ò‚Î‚·•ûŒü

    public GameObject PlayerObj;

    float timer;

    bool isMove;
    bool isAttack;

    Vector2 BulletPos;

    int addPos_X;
    int addPos_Y;

    int MoveNum;
    void Start()
    {
        PlayerObj = GameObject.FindWithTag("Player");

        isMove = true;
        isAttack = true;
        timer = 0;

        MoveNum = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 1 && isAttack)
        {
            isAttack = false;
            BulletAttack();
        }

        if(timer > 2 && isMove) 
        {
            isMove = false ;
            Move();
        }

        if(timer > 4) 
        {
            isAttack = true;
            isMove = true ;
            timer = 0;
        }
    }

    public void BulletAttack()
    {
        BulletPos = new Vector2(this.gameObject.transform.position.x,
                                this.gameObject.transform.position.y - 0.8f);

        BulletDir = PlayerObj.transform.position - this.gameObject.transform.position;
        BulletDir.Normalize();
        Bullet = Instantiate(BulletPrefab,BulletPos,Quaternion.identity);
        BulletRg = Bullet.GetComponent<Rigidbody2D>();
        BulletRg.AddForce(BulletDir * 400);
    }

    public void Move()
    {
        MoveNum = Random.RandomRange(1, 2);

        if(MoveNum == 1)
        {
            if (this.gameObject.transform.position.x - PlayerObj.transform.position.x > 0)
            {
                addPos_X = -2;
            }
            else
            {
                addPos_X = 2;
            }

            if (this.gameObject.transform.position.y > 10)
            {
                addPos_Y = -2;
            }
            else if (this.gameObject.transform.position.y > 0)
            {
                addPos_Y = Random.RandomRange(-2, 2);
            }
            else if (this.gameObject.transform.position.y < 0)
            {
                addPos_Y = 2;
            }
        }
        else if(MoveNum == 2)
        {
            addPos_X = Random.RandomRange(-2, 2);
            addPos_Y = Random.RandomRange(1, 2);
        }

        this.gameObject.transform.DOMove(new Vector2( this.gameObject.transform.position.x + addPos_X,
                                         this.gameObject.transform.position.y + addPos_Y), 1.0f);
    }
}
