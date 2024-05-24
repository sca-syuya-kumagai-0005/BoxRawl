using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.SetActive(true);
        if (Input.GetKey(KeyCode.Space))
        {
            PlayerMove.PlayerDead = false;
            this.gameObject.SetActive(false);
        }
    }
}
