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

    bool isJump = false;
    public static bool isStart;


    public GameObject hallObject;

    bool startFlag;

    // Start is called before the first frame update
    void Start()
    {
        rg = playerObj.GetComponent<Rigidbody2D>();

        isStart = false;

        startFlag = false;
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

        FadeIO.FadeOut(startFlag);
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
            EnemyObj.transform.DOMoveX(EnemyObj.transform.position.x + (GroundObj.transform.position.x - tergetObj.transform.position.x), 5.0f);
        }

        StartCoroutine(playerJump());
        
    }

    public IEnumerator playerJump()
    {
        yield return new WaitForSeconds(3.0f);
        Vector2 force = new Vector3(1.0f, 9.5f);
        rg.AddForce(force *50);

        yield return new WaitForSeconds(2.0f);
        startFlag = true;
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Menu");
        yield return null;
    }

    

}
