using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TitleEnemy : MonoBehaviour
{
    Rigidbody2D EnemyRg;
    Vector3 enemyStartPos;
    int EnemyNum = 0;

    public GameObject playerObj;

    bool isUp;
    // Start is called before the first frame update
    void Start()
    {
        enemyStartPos = this.gameObject.transform.position;
        EnemyRg = this.gameObject.GetComponent<Rigidbody2D>();
        EnemyNum = 0;

        isUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!TitleManager.isStart)
        {
            this.gameObject.transform.position += new Vector3(-4.0f, 0, 0) * Time.deltaTime;
        }

        if (this.gameObject.transform.position.x < -11)
        {
            TitlePlayer.isJump = false;
            EnemyNum = Random.Range(0, 2);
            if (EnemyNum == 0)
            {
                EnemyRg.velocity = Vector2.zero;
                EnemyRg.angularVelocity = 0.0f;
                EnemyRg.gravityScale = 0.0f;
                this.gameObject.transform.position = enemyStartPos;
            }
            else if (EnemyNum == 1)
            {
                isUp = true;
                EnemyRg.velocity = Vector2.zero;
                EnemyRg.angularVelocity = 0.0f;
                EnemyRg.gravityScale = 0.12f;
                this.gameObject.transform.position = new Vector3(enemyStartPos.x, 4.3f, enemyStartPos.z);
            }
        }

        if (this.gameObject.transform.position.x - playerObj.transform.position.x < 2 && isUp)
        {
            if (EnemyNum == 1)
            {
                isUp = false;
                EnemyRg.velocity = Vector2.zero;
                EnemyRg.angularVelocity = 0.0f;
                EnemyRg.gravityScale = -0.11f;

                Debug.Log("ã‚És‚­");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            this.gameObject.transform.position = new Vector3(-12.0f, 0f, 0f);
        }
    }
}
