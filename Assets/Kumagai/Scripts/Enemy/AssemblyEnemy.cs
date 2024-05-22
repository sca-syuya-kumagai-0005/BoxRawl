using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AssemblyEnemy : MonoBehaviour
{
    [SerializeField]private GameObject centerObj;
    [SerializeField] private int moveVecX;//ˆÚ“®•ûŒü
    [SerializeField] private int moveVecY;//ˆÚ“®•ûŒü
    [SerializeField] private float speed;
    [SerializeField] private float moveSpace;
    // Start is called before the first frame update
    void Start()
    {
        int x = UnityEngine.Random.Range(0, 2);
        if(x==0)
        {
            moveVecX = -1;
        }
        else if(x==1)
        {
            moveVecX = 1;
        }

        int y = UnityEngine.Random.Range(0, 2);
        if (y == 0)
        {
            moveVecY = -1;
        }
        else if (y == 1)
        {
            moveVecY = 1;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        centerObj.transform.position += new Vector3(1, 0, 0)*Time.deltaTime;
    }

    private void Move()
    {
        Vector3 pos=this.transform.position;
        float x=pos.x;  
        float y=pos.y;
        if(Math.Abs(Math.Abs(this.transform.position.x)-Math.Abs(centerObj.transform.position.x))>moveSpace/3)
        {
            moveVecX *= -1;
        } 
        if(Math.Abs(Math.Abs(this.transform.position.y)-Math.Abs(centerObj.transform.position.y))>moveSpace)
        {
            moveVecY *= -1;
        }
        int b = UnityEngine.Random.Range(0, 90);
        if( b==0)
        {
            moveVecX *= -1;
        }
        int a = UnityEngine.Random.Range(0, 90);
        if (a == 0)
        {
            moveVecY*= -1;   
        }
        x += speed * Time.deltaTime * moveVecX;
        y += speed * Time.deltaTime * moveVecY;
        this.transform.position=new Vector3(x,y,pos.z); 
    }
}
