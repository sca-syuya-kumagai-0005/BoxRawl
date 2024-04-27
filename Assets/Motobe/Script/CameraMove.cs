using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position= Player.transform.position;
        if (Player.transform.position.y > 0)
        {
            this.transform.position = new Vector3(0, 0, 0);
        }
    }
}
