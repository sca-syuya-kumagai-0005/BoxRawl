using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    public static bool Rota;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(Rota==true)
        {
            transform.Rotate(0, 0, speed);
        }
        if (Rota == false)
        {
            transform.rotation=new Quaternion(0,0,0,0);
        }
    }
}
