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
    [SerializeField] private GameObject Player;
    [SerializeField] private Text sceneName;
    [SerializeField] private GameObject mainCamera;
    private bool sceneChangeFlag;
    private string yesOrNo;
    [SerializeField]private string thisSceneName;
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
                            thisSceneName = "Main Game";
                        }
                        break;
                    case "Title":
                        {
                            thisSceneName = "Title";
                        }
                        break;
                }
                Player.transform.position = this.gameObject.transform.position + new Vector3(0, 0.3f, 0);//0.3はボタンサイズ
                sceneName.text = thisSceneName;
                PlayerMove.Drop = false;
            }
        }
      
    }

    

    //選択したときにそのシーンに移動するかどうかをチェックする
    //ステータス割り振りの時にはこのシーンはスキップする
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


    //シーン切り替え時の演出（仮）
    private IEnumerator SceneChanger()
    {
        mainCamera.transform.position = new Vector3(Player.transform.position.x, 2, -10);
        sceneCheckBackGround.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        sceneChangeFlag = true;
        hole.SetActive(false);
        yield return new WaitForSeconds(1f);
        sceneGround.SetActive(false);
        this.gameObject.transform.GetComponent<SpriteRenderer>().color=new Color(0,0,0,0);  
        this.gameObject.transform.GetComponent<BoxCollider2D>().isTrigger = true;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(thisSceneName);
    }
}
