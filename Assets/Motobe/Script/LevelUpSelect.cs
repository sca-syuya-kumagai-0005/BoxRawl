using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpSelect : MonoBehaviour
{
    public GameObject Select;
    public GameObject SelectBack;
    int set;
    int dir;

    // Start is called before the first frame update
    void Start()
    {
        set = 2;
        dir = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(set);
        if (Input.GetKeyDown(KeyCode.A) && set>=2)
        {
            set -= 1;
            SelectBack.transform.position += new Vector3(-7.5f, 0, 0);
            dir = -1;
        }
        if (Input.GetKeyDown(KeyCode.D) && set <= 2)
        {
            set += 1;
            SelectBack.transform.position += new Vector3(7.5f, 0, 0);
            dir = 1;
        }
        if (SelectBack.transform.position.x > Select.transform.position.x&&dir==1)
        {
            Select.transform.position += new Vector3(75f*Time.deltaTime, 0, 0);
        }
        if (SelectBack.transform.position.x < Select.transform.position.x && dir == -1)
        {
            Select.transform.position += new Vector3(-75f * Time.deltaTime, 0, 0);
        }
    }
}
