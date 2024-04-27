using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] public float DefaultJumpForce;
    [SerializeField] public float PlusJumpForce;
    private float JumpForce;

    [SerializeField] public float DefaultSpeed;
    [SerializeField] public float PlusSpeed;
    private float Speed;

    [SerializeField] public float DefaultSize;
    [SerializeField] public float PlusSize;
    private float Size;

    private float DefaultHp=2;
    [SerializeField] public float PlusHp;
    private float Hp;

    private int JumpCount;

    private bool OnGround;
    private bool OnWall;
    private float WallJumpCount;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        JumpForce = DefaultJumpForce + PlusJumpForce;
        Speed = DefaultSpeed + PlusSpeed;
        Size = DefaultSize + PlusSize;
        Hp = DefaultHp + PlusHp;
        JumpCount = 0;
        OnWall = false;
    }

    // Update is called once per frame
    void Update()
    {
        //ÉWÉÉÉìÉv
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (JumpCount==0)
            {
                rb = GetComponent<Rigidbody2D>();
                rb.velocity = new Vector3(0, JumpForce, 0);
                JumpCount += 1;
                OnGround = false;
            }
            else if (JumpCount==1) 
            {
                rb = GetComponent<Rigidbody2D>();
                rb.velocity = new Vector3(0, -JumpForce*2, 0);
                JumpCount += 1;
                OnGround = false;
            }
        }
        //ï«ÇﬂÇËçûÇ›ñhé~
        if(Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.D))
        {
            OnWall = false;
        }
        //à⁄ìÆ
        if (Input.GetKey(KeyCode.A))
        {
            if (OnWall == false)
            {
                this.transform.position += new Vector3(-Speed, 0, 0);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (OnWall == false)
            {
                this.transform.position += new Vector3(Speed, 0, 0);
            }
        }
        if (OnGround == false)
        {
            PlayerSkin.Rota = true;
        }
        else
        {
            PlayerSkin.Rota = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            OnGround = true;
            JumpCount = 0;
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            OnWall = true;
            JumpCount = 0;
        }
    }
}
