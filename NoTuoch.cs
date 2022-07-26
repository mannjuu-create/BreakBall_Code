using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �{�[�����ז�����𓮂����X�N���v�g.
/// </summary>

public class NoTuoch : MonoBehaviour
{
    private Rigidbody rb;

    private Vector3 moveVec = Vector3.zero;     //�ړ��x�N�g��.
    private Vector3 nowPos = Vector3.zero;      //�܂�Ԃ����Ɍ��݂̈ʒu������ׂɎg��.
    private float Speed = 50;                   //�ړ����x.
    private float moveLow = 0.3f;               //���x�ύX���̉����l.
    private float startPos = 25.0f;             //�̉E���̐܂�Ԃ��n�_.
    private float returnPos = -25.0f;           //�̍����̐܂�Ԃ��n�_.

    private bool resetFrag = false;
    private float count = 0;                    //���x��ύX���Ă���̌o�ߎ���.
    private float resetTime = 30;               //���x�ύX�܂ł̎���.

    void Start()
    {
        //Rigidbody�̎擾.
        rb = GetComponent<Rigidbody>();

        //�ŏ��ɃX�s�[�h�Z�b�g.
        SetSpeed();
    }

    void Update()
    {
        rb.velocity = moveVec * Speed;

        if(transform.position.x < returnPos - 1)
        {
            nowPos = transform.position;
            nowPos.x = returnPos;
            transform.position = nowPos;

            moveVec.x *= -1;

            if (resetFrag == true)
            {
                SetSpeed();
                count = 0;
                resetFrag = false;
            }
        }
        if(transform.position.x > startPos + 1)
        {
            nowPos = transform.position;
            nowPos.x = startPos;
            transform.position = nowPos;

            moveVec *= -1;
        }

        //���x�ύX�܂ł̃J�E���g.
        count += Time.deltaTime;
        //���̎��Ԃ��o�߂�����
        if(count >= resetTime)
        {
            resetFrag = true;
        }
    }


    //�̑��x�������_���ɕύX����.
    void SetSpeed()
    {
        float rand = Random.value;
        if (rand <= moveLow)
        {
            rand = moveLow;
        }
        //�ړ����x�ɑ��.
        moveVec.x = rand;
    }
}
