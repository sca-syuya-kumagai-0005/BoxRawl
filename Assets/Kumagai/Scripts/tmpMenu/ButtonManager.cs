using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject hole;
    [SerializeField] private GameObject sceneGround;
    [SerializeField] private GameObject sceneCheckBackGround;
    [SerializeField] private Text sceneName;
    private bool sceneChangeFlag;
    private string yesOrNo;
    private string thisSceneName;
    public static bool sceneCheck;

    
    // Start is called before the first frame update
    void Start()
    {
        sceneCheck = false;
        yesOrNo = "No";
    }

    // Update is called once per frame
    void Update()
    {
        SceneCheck();
        Debug.Log(sceneCheck);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.transform.name == "Player") 
        {
            if(PlayerMove.Drop)
            {
                sceneCheck = true;
                switch(this.gameObject.transform.name) 
                {
                    case "Main Game":
                        {
                            thisSceneName = "ÉÅÉCÉìÉQÅ[ÉÄ";
                        }
                        break;
                }
                sceneName.text = thisSceneName;
            }
        }

        else
        {
            Debug.Log("ï®ëÃÇ™å©Ç¬Ç©ÇËÇ‹ÇπÇÒ");
        }
    }

    private void SceneCheck()
    {
        if(sceneCheck)
        {
            sceneCheckBackGround.SetActive(true) ;
            if(Input.GetKeyDown(KeyCode.Z))
            {
                yesOrNo = "Yes";
            }
            if(Input.GetKeyDown(KeyCode.X))
            {
                yesOrNo = "No";
                sceneCheck = false;
            }
        }
        else
        {
            sceneCheckBackGround.SetActive(false);
        }
        if (yesOrNo=="Yes")
        {
            StartCoroutine(SceneChanger());
        }
       
    }


    private IEnumerator SceneChanger()
    {
        sceneCheckBackGround.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        sceneChangeFlag = true;
        hole.SetActive(false);
        yield return new WaitForSeconds(1f);
        sceneGround.SetActive(false);
        this.gameObject.transform.GetComponent<SpriteRenderer>().color=new Color(0,0,0,0);  
        this.gameObject.transform.GetComponent<BoxCollider2D>().isTrigger = true;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(this.gameObject.name);
    }
}
