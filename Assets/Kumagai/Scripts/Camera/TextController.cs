using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextController : MonoBehaviour
{
    private Vector3 pos;
    [SerializeField] private GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        pos=this.transform.position;
        this.transform.position = transform.TransformPoint(pos);
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
