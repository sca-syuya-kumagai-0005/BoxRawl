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

    // Start is called before the first frame update
    void Start()
    {
        rg = playerObj.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Return))
        {
            startGame();
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
        rg.velocity = new Vector2(1, 5) * 1;

        yield return new WaitForSeconds(3.0f);
        //SceneManager.LoadScene("Menu");
        yield return null;
    }
}
