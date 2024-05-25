using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class TitleManager : MonoBehaviour
{
    [SerializeField]GameObject GroundObj;
    [SerializeField] GameObject tergetObj;

    [SerializeField] GameObject playerObj;
    Rigidbody2D rg;

    [SerializeField] GameObject EnemyObj;
    Rigidbody2D EnemyRg;
    Vector3 enemyStartPos;

    private bool isJump = false;
    private bool isStart;

    int moveNum;
    int EnemyNum;

    public GameObject hallObject;

    // Start is called before the first frame update
    void Start()
    {
        rg = playerObj.GetComponent<Rigidbody2D>();
        EnemyRg = EnemyObj.GetComponent<Rigidbody2D>();
        enemyStartPos = EnemyObj.transform.position;

        isStart = false;

        moveNum = 0;
        EnemyNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //ゲームスタート
        if(Input.GetKeyDown(KeyCode.Return) && !isStart
            || Input.GetKeyDown(KeyCode.Space) && !isStart)
        {
            isStart = true;
            hallObject.SetActive(false);
            startGame();
        }

        //敵の動き
        if (!isStart)
        {
            EnemyObj.transform.position += new Vector3(-4.0f, 0, 0) * Time.deltaTime;
        }
        if(EnemyObj.transform.position.x < -11)
        {
            EnemyNum = Random.Range(0,2);
            moveNum = Random.Range(0,2);
            isJump = false ;

            if(EnemyNum == 0)
            {
                EnemyRg.velocity = Vector2.zero;
                EnemyRg.angularVelocity = 0.0f;
                EnemyRg.gravityScale = 0.0f;
                EnemyObj.transform.position = enemyStartPos;
            }
            else if(EnemyNum == 1)
            {
                EnemyRg.velocity = Vector2.zero;
                EnemyRg.angularVelocity = 0.0f;
                EnemyRg.gravityScale = 0.12f;
                EnemyObj.transform.position = new Vector3(enemyStartPos.x,4.3f,enemyStartPos.z);
            }

            Debug.Log(EnemyNum);
        }

        //playerの動き
        if(EnemyObj.transform.position.x - playerObj.transform.position.x < 2
            && !isJump)
        {
            rg.velocity = new Vector2(0, 8.0f) * 1;
            isJump = true;

            if(moveNum == 1)
            {
                StartCoroutine(playerAttack());
            }

            if(EnemyNum == 1)
            {
                EnemyRg.velocity = Vector3.zero;
                EnemyRg.angularVelocity = 0.0f;
                EnemyRg.gravityScale = -0.11f;
            }
        }

    }

    void startGame()
    {
        GroundObj.transform.DOMove(tergetObj.transform.position, 5.0f);
        if(playerObj.transform.position.x > EnemyObj.transform.position.x)
        {
            EnemyObj.transform.DOMove(EnemyObj.transform.position + (tergetObj.transform.position + GroundObj.transform.position), 5.0f);
        }
        else
        {
            //EnemyObj.transform.DOMove(EnemyObj.transform.position + (GroundObj.transform.position - tergetObj.transform.position), 5.0f);
            EnemyObj.transform.DOMoveX(EnemyObj.transform.position.x + (GroundObj.transform.position.x - tergetObj.transform.position.x), 5.0f);
        }

        StartCoroutine(playerJump());
        
    }

    public IEnumerator playerJump()
    {
        yield return new WaitForSeconds(3.0f);
        //rg.velocity = new Vector2(1, 9.5f);
        Vector2 force = new Vector3(1.0f, 9.5f);
        rg.AddForce(force *50);

        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene("Menu");
        yield return null;
    }

    IEnumerator playerAttack()
    {
        yield return new WaitForSeconds(0.4f);
        rg.gravityScale = 20.0f;
        yield return new WaitForSeconds(0.2f);
        if(moveNum == 1)
        {
            EnemyObj.transform.position = new Vector3(-12.0f, 0f, 0f);
        }
        yield return new WaitForSeconds(0.3f);
        rg.gravityScale = 1.0f;
    }

}
