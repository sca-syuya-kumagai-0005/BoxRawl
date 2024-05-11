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
       
        //if(PlayerMove.JumpCount==0)
        //{
        //        jumpCameraRevision = 1;
        //}
        //else 
        //{
        //        jumpCameraRevision = tmpRevision;
        //}
        this.gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y * jumpCameraRevision, -10);
    }
}
