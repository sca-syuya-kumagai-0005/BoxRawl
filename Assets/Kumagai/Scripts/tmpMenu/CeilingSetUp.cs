using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingSetUp : MonoBehaviour
{
    [SerializeField]
    private GameObject ceiling;
    private float timer;
    private float alpha = 0;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
      
        if(timer>2)
        {
            alpha += Time.deltaTime;
            ceiling.SetActive(true);
            ceiling.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, alpha);
        }
        else
        {
            timer += Time.deltaTime;
         
        }
    }
}
