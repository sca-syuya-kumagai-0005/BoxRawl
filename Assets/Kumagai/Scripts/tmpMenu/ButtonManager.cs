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
    [SerializeField] private float alphaSec;
    [SerializeField] private GameObject statusWindow;
    private bool sceneChangeFlag;
    private string yesOrNo;
    [SerializeField]private string thisSceneName;
    public static bool sceneCheck;

    
    // Start is called before the first frame update
    void Start()
    {
        sceneCheck = false;
        yesOrNo = "No";
        StartCoroutine(ButtonStart());
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
                Player.transform.position = this.gameObject.transform.position + new Vector3(0, 0.3f, 0);//0.3�̓{�^���T�C�Y
                sceneName.text = thisSceneName;
                PlayerMove.Drop = false;
            }
        }
      
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        thisSceneName = "";
    }

    //�I�������Ƃ��ɂ��̃V�[���Ɉړ����邩�ǂ������`�F�b�N����
    //�X�e�[�^�X����U��̎��ɂ͂��̃V�[���̓X�L�b�v����
    private void SceneCheck()
    {
        if(sceneCheck&&sceneName.text=="Title"||sceneName.text=="Main Game")
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
        else if(sceneCheck&&sceneName.text=="StatusUp")
        {
            statusWindow.SetActive(true);
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


    //�V�[���؂�ւ����̉��o�i���j
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

    float alpha = 0;
    private IEnumerator ButtonStart()
    {
        while (this.transform.gameObject.GetComponent<SpriteRenderer>().color.a <= 1)
        {
            Color bc = this.GetComponent<SpriteRenderer>().color;
            Color gc=sceneGround.GetComponent<SpriteRenderer>().color;
            alpha += Time.deltaTime/alphaSec;
            this.transform.gameObject.GetComponent<SpriteRenderer>().color = new Color(bc.r, bc.g, bc.b, alpha);
            sceneGround.transform.gameObject.GetComponent<SpriteRenderer>().color=new Color(gc.r, gc.g, gc.b, alpha);
            Debug.Log("�Ă΂�Ă��܂�");
            yield return new WaitForEndOfFrame();
        }
      
    }
}
