using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class UnGravityEnemy : MonoBehaviour
{
    //�d�͂��������A�v���C���[�Ɍ������Ĉړ�����G�l�~�[�ł�
    // Start is called before the first frame update
    [SerializeField] private GameObject m_target;
    [SerializeField] private int speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetVec();
    }

    
    void GetVec()
    {
        Vector3 target=m_target.transform.position-this.gameObject.transform.position;//Enmey����Player�ւ̃x�N�g��
        target=target.normalized;//�x�N�g���̐��K��
        this.transform.position += target * speed*Time.deltaTime;
    }
}
