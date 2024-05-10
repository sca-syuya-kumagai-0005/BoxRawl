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

    [SerializeField] GameObject windObj;
    // Start is called before the first frame update
    void Start()
    {
        rg = playerObj.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            startGame();
        }

        windObj.transform.Translate(-0.1f, 0f, 0);
        if(windObj.transform.position.x < -25)
        {
            windObj.transform.position = new Vector3(10.0f, -3.0f, 0.0f);
        }
    }

    void startGame()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Debug.Log("ƒƒjƒ…[‚ÉˆÚ“®");
        //    SceneManager.LoadScene("Menu");
        //}


        GroundObj.transform.DOMove(tergetObj.transform.position, 5.0f);
        StartCoroutine(playerJump());
        
    }

    public IEnumerator playerJump()
    {
        yield return new WaitForSeconds(3.0f);
        rg.velocity = new Vector2(1, 10) * 1;

        yield return null;
    }
}
