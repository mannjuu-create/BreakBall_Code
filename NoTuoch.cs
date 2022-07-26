using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボールを邪魔する板を動かすスクリプト.
/// </summary>

public class NoTuoch : MonoBehaviour
{
    private Rigidbody rb;

    private Vector3 moveVec = Vector3.zero;     //移動ベクトル.
    private Vector3 nowPos = Vector3.zero;      //折り返し時に現在の位置を入れる為に使う.
    private float Speed = 50;                   //移動速度.
    private float moveLow = 0.3f;               //速度変更時の下限値.
    private float startPos = 25.0f;             //板の右側の折り返し地点.
    private float returnPos = -25.0f;           //板の左側の折り返し地点.

    private bool resetFrag = false;
    private float count = 0;                    //速度を変更してからの経過時間.
    private float resetTime = 30;               //速度変更までの時間.

    void Start()
    {
        //Rigidbodyの取得.
        rb = GetComponent<Rigidbody>();

        //最初にスピードセット.
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

        //速度変更までのカウント.
        count += Time.deltaTime;
        //一定の時間が経過したら
        if(count >= resetTime)
        {
            resetFrag = true;
        }
    }


    //板の速度をランダムに変更する.
    void SetSpeed()
    {
        float rand = Random.value;
        if (rand <= moveLow)
        {
            rand = moveLow;
        }
        //移動速度に代入.
        moveVec.x = rand;
    }
}
