using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject hole;
    [SerializeField] private GameObject ground;
    public static bool sceneChangeFlag;
    
    // Start is called before the first frame update
    void Start()
    {
        sceneChangeFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.transform.name == "Player") 
        {
            if(PlayerMove.Drop)
            {
                StartCoroutine(SceneChanger());
            }
        }

        else
        {
            Debug.Log("•¨‘Ì‚ªŒ©‚Â‚©‚è‚Ü‚¹‚ñ");
        }
    }


    private IEnumerator SceneChanger()
    {
        sceneChangeFlag = true;
        hole.SetActive(false);
        yield return new WaitForSeconds(1f);
        ground.SetActive(false);
        this.gameObject.transform.GetComponent<SpriteRenderer>().color=new Color(0,0,0,0);  
        this.gameObject.transform.GetComponent<BoxCollider2D>().isTrigger = true;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(this.gameObject.name);
    }
}
