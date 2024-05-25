using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ParyController : MonoBehaviour
{
    public GameObject PlayerObject;
    public static bool parySet;
    public static bool paryJump;
    // Start is called before the first frame update
    void Start()
    {
        parySet = false;
        paryJump = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position = PlayerObject.transform.position;
        if (parySet)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerMove.JumpCount = 1;
                PlayerMove.paryCheck = true;
                SEController.pary = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            PlayerMove.JumpCount = 0;
            paryJump = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            parySet = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            PlayerMove.JumpCount = 1;
            parySet = false;
            paryJump = false;
            PlayerMove.paryCheck = false;
        }
    }
}
