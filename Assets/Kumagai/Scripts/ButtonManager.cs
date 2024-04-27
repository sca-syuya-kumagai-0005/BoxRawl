using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

//íSìñé“Å@SK
public class ButtonManager : MonoBehaviour
{
    public static GameObject buttonManager;
    private List<GameObject> menuButton;
    public static int selectButtonNumber;
    Coroutine SizeUpCoroutine;
    private float tmpBSizeX;
    private float tmpBSizeY;
    // Start is called before the first frame update
    void Start()
    {
        ArraySet();
        tmpBSizeX = menuButton[1].gameObject.transform.localScale.x;
        tmpBSizeY = menuButton[1].gameObject.transform.localScale.y;
        selectButtonNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {
        KeyManager();
        SelectButton();
    }

    void ArraySet()
    {
        buttonManager = GameObject.Find("ButtonManager").gameObject;
        menuButton = new List<GameObject>();
        for (int i = 0; i < buttonManager.transform.childCount; i++)
        {
            menuButton.Add(buttonManager.transform.GetChild(i).gameObject);
        }
        Debug.Log(menuButton.Count);
    }

    void KeyManager()
    {
        if(Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(selectButtonNumber != 0)
            {
                selectButtonNumber--;
            }
          
        }
        
        if(Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(selectButtonNumber!=3)
            {
                selectButtonNumber++;
            }
        }
       
    }

    void SelectButton()
    {
        for(int i=0;i<buttonManager.transform.childCount;i++)
        {
            if(i==selectButtonNumber)
            {
                SizeUpCoroutine = StartCoroutine(ButtonSizeUp(menuButton[i]));
            }
            else
            {
                StartCoroutine(ButtonSizeDown(menuButton[i]));
            }
        }
    }

    const float maxSize=1.5f;
    IEnumerator ButtonSizeUp(GameObject obj)
    {
        while(obj.transform.localScale.x<tmpBSizeX*maxSize)
        {
            obj.transform.localScale += new Vector3(1, 1f, 1f) * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator ButtonSizeDown(GameObject obj)
    {
        while(obj.transform.localScale.x>tmpBSizeX)
        {
            obj.transform.localScale -= new Vector3(1, 1f, 1f) * Time.deltaTime;
            yield return null;
        }
    }

    void StateMove()
    {
        if(Input.GetKeyDown(KeyCode.Return)||Input.GetMouseButtonDown(0))
        {
            switch(selectButtonNumber)
            {
                case 0:
                    {
                        state=GameState.MAINGAME;
                    }
                    break;
                case 1:
                    {
                        state=GameState.STATUS;   
                    }
                    break;
                case 2:
                    {
                        state=GameState.CONTROL;
                    }
                    break;
                case 3:
                    {
                        state=GameState.TITLE;
                    }
                    break;
            }
        }
    }
}
