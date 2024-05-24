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
        enemyStartPos = EnemyObj.transform.position;

        isStart = false;

        moveNum = 0;
        EnemyNum = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && !isStart)
        {
            isStart = true;
            hallObject.SetActive(false);
            startGame();
        }

        //“G‚Ì“®‚«
        if (!isStart)
        {
            EnemyObj.transform.position += new Vector3(-4.0f, 0, 0) * Time.deltaTime;
        }
        if(EnemyObj.transform.position.x < -11)
        {
            EnemyNum = Random.Range(0,1);
            isJump = false ;

            if(EnemyNum == 0)
            {
                EnemyObj.transform.position = enemyStartPos;
            }
            else if(EnemyNum == 1)
            {
                EnemyObj.transform.position = new Vector3(enemyStartPos.x,4.3f,enemyStartPos.z);
            }

            //if(isStart)
            //{
            //    Destroy(EnemyObj);
            //}

            Debug.Log(EnemyNum);
        }

        if(EnemyObj.transform.position.x - playerObj.transform.position.x < 2
            && !isJump)
        {
            rg.velocity = new Vector2(0, 8.0f) * 1;
            isJump = true;

            if(isStart)
            {
                //startGame();
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
        rg.velocity = new Vector2(1, 9.5f) * 1;

        yield return new WaitForSeconds(3.0f);
        //SceneManager.LoadScene("Menu");
        yield return null;
    }


}
