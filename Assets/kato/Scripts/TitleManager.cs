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

    // Start is called before the first frame update
    void Start()
    {
        rg = playerObj.GetComponent<Rigidbody2D>();
        enemyStartPos = EnemyObj.transform.position;

        isStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            isStart = true;
            //startGame();
        }

        //“G‚Ì“®‚«
        EnemyObj.transform.position += new Vector3(-4.0f, 0, 0) * Time.deltaTime;
        if (!isStart)
        {
        }
        if(EnemyObj.transform.position.x < -11)
        {
            EnemyObj.transform.position = enemyStartPos;
            isJump = false ;

            if(isStart)
            {
                Destroy(EnemyObj);
            }
        }

        if(EnemyObj.transform.position.x - playerObj.transform.position.x < 2
            && !isJump)
        {
            rg.velocity = new Vector2(0, 8.0f) * 1;
            isJump = true;

            if(isStart)
            {
                startGame();
            }
        }
    }

    void startGame()
    {
        GroundObj.transform.DOMove(tergetObj.transform.position, 5.0f);
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
