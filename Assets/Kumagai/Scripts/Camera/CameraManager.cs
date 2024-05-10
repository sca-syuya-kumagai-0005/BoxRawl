using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float jumpCameraRevision;
    private float tmpRevision;
    [SerializeField]
    private bool OnGround = false;
    // Start is called before the first frame update
    void Start()
    {
        tmpRevision=jumpCameraRevision;
    }

    // Update is called once per frame
    void Update()
    {
       
        if(PlayerMove.Drop)
        {
           jumpCameraRevision = 1;
        }
        else if(!OnGround)
        {
            jumpCameraRevision = tmpRevision;
        }
        gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y * jumpCameraRevision, -10);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Ground")
        {
            OnGround=false;
        }
    }
}
