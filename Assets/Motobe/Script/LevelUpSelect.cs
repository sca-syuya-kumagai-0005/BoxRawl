using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class LevelUpSelect : MonoBehaviour
{
    public GameObject Select;
    public GameObject SelectBack;
    public GameObject Select2;
    public GameObject Select2Back;
    public GameObject[] Level;
    bool conf;
    public GameObject ConfirmWindow;
    public GameObject EnemySpawner;
    int set;
    int set2;
    int dir;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        conf = false;
        set = 2;
        set2 = 0;
        dir = 0;
//        for(int i = 0; i < 3; i++)
//        {
//            Level[i].SetActive(false);
//        }
        ConfirmWindow.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!conf)
        {
            if (Input.GetKeyDown(KeyCode.A) && set >= 2)
            {
                set -= 1;
                SelectBack.transform.position += new Vector3(-8.15f, 0, 0);
                dir = -1;
            }
            if (Input.GetKeyDown(KeyCode.D) && set <= 2)
            {
                set += 1;
                SelectBack.transform.position += new Vector3(8.15f, 0, 0);
                dir = 1;
            }
            if (SelectBack.transform.position.x > Select.transform.position.x && dir == 1)
            {
                Select.transform.position += new Vector3(75f * Time.deltaTime, 0, 0);
            }
            if (SelectBack.transform.position.x < Select.transform.position.x && dir == -1)
            {
                Select.transform.position += new Vector3(-75f * Time.deltaTime, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (set == 1)
                {
                    //Level[0].SetActive(true);
                }
                if (set == 2)
                {
                    //Level[1].SetActive(true);
                }
                if (set == 3)
                {
                    //Level[2].SetActive(true);
                }
                ConfirmWindow.SetActive(true); 
                conf = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.A) && set2 ==1)
            {
                set2 = 0;
                Select2Back.transform.position += new Vector3(-8.85f, 0, 0);
                dir = -1;
            }
            if (Input.GetKeyDown(KeyCode.D) && set2 ==0)
            {
                set2 = 1;
                Select2Back.transform.position += new Vector3(8.85f, 0, 0);
                dir = 1;
            }
            if (Select2Back.transform.position.x > Select2.transform.position.x && dir == 1)
            {
                Select2.transform.position += new Vector3(75f * Time.deltaTime, 0, 0);
            }
            if (Select2Back.transform.position.x < Select2.transform.position.x && dir == -1)
            {
                Select2.transform.position += new Vector3(-75f * Time.deltaTime, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                conf = false;
                if (set2 == 0)
                {
                    ConfirmWindow.SetActive(false);
                }
                if (set2 == 1)
                {
                    if (set == 1)
                    {
                        PlayerMove.PlusSpeed += 3.3f;
                    }else if(set == 2)
                    {
                        PlayerMove.PlusJumpForce += 3.3f;
                    }
                    else if (set == 3)
                    {
                        PlayerMove.PlusInvincibleTime += 4;
                    }
                    set2 = 0;
                    Select2Back.transform.position += new Vector3(-8.9f, 0, 0);
                    Select2.transform.position = Select2Back.transform.position;
                    dir = -1;
                    EnemySpawner.SetActive(true);
                    ConfirmWindow.SetActive(false);
                    PlayerMove.blink = true;
                    PlayerMove.PlayerDead = false;
                    this.gameObject.SetActive(false);

                }

            }
         }
    }
}
