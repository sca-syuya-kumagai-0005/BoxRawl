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
    public static bool Drop;
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
        Drop = false;
    }

    // Update is called once per frame
    void Update()
    {
        //ƒWƒƒƒ“ƒv
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (JumpCount==0)
            {
                rb.velocity = new Vector3(0, JumpForce, 0);
            }
            
            JumpCount += 1;
            OnGround = false;
        }
        //•Ç‚ß‚èž‚Ý–hŽ~
        if(Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.D))
        {
            OnWall = false;
        }
        //ˆÚ“®
        if (Input.GetKey(KeyCode.A))
        {
            PlayerSkin.rota = 1;
            if (OnWall == false)
            {
                this.transform.position += new Vector3(-Speed * Time.deltaTime, 0, 0);
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            PlayerSkin.rota = -1;
            if (OnWall == false)
            {
                this.transform.position += new Vector3(Speed * Time.deltaTime, 0, 0);
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            PlayerSkin.rota = 0;
            if (JumpCount > 0)
            {
                rb.velocity = new Vector3(0, -JumpForce * 2, 0);
                Drop = true;
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
            PlayerSkin.rota = 0;
            OnGround = true;
            JumpCount = 0;
            Drop = false;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            OnWall = true;
            JumpCount = 0;
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            PlayerSkin.rota = 0;
            PlayerSkin.Rota = false;
            JumpCount = 0;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            JumpCount = 1;
        }
    }
}
