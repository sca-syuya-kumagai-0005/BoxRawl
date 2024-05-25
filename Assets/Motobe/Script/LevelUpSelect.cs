using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpSelect : MonoBehaviour
{
    public GameObject Select;
    public GameObject SelectBack;
    int set;

    // Start is called before the first frame update
    void Start()
    {
        set = 2;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(set);
        if (Input.GetKeyDown(KeyCode.A) && set>=2)
        {
            set -= 1;
            SelectBack.transform.position += new Vector3(-7.5f, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.D) && set <= 2)
        {
            set += 1;
            SelectBack.transform.position += new Vector3(7.5f, 0, 0);
        }
        if (set > 3)
        {
            set = 3;
        }
        if (set < 1)
        {
            set = 1;
        }
    }
}
