using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    public static bool Rota;
    public static int rota;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        rota = 0;
        speed = 750f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Rota==true)
        {
            transform.Rotate(0, 0, speed*rota * Time.deltaTime);
        }
        if (Rota == false)
        {
            transform.rotation=new Quaternion(0,0,0,0);
        }
    }
}
