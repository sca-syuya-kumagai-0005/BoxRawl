using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        startGame();
    }

    void startGame()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("���j���[�Ɉړ�");
            //SceneManager.LoadScene("menu");
        }
    }
}
