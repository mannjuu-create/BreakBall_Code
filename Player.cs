using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ボールの移動、ジャンプ、スコア加算をするスクリプト.
/// </summary>

public class Player : MonoBehaviour
{
    // ジャンプ中
    private bool JumpFrag = false;       // ジャンプしているかのフラグ.
    private bool BurstFrag = false;
    private float JumpCount = 0;            // ジャンプ時間.
    private float JumpBackTime = 0.5f;      // ジャンプしてから戻るまでの時間.

    // ジャンプ関係.
    private float JumpPower = 0;           // ジャンプ力を決定するための変数.
    private bool PowerUpFrag = true;       // ジャンプ力の上昇と下降を管理するためのフラグ.
    private Vector3 direction;
    private float middleLine = 0.2f;        // 中ジャンプのライン
    private float upLine = 0.4f;            // 上ジャンプのライン
    private float maxLine = 0.6f;           // ジャンプの最大値

    // 左右の位置関係.
    private Vector3 CenterPos = new Vector3(0, 5, 0);            // 真ん中の位置.
    private Vector3 LeftPos = new Vector3(-10, 5, 0);            // 左の位置.
    private Vector3 RightPos = new Vector3(10, 5, 0);            // 右の位置.
    private int PositionNum = 1;                                 // 左右位置を管理するための変数.
    private bool InputFrag  = false;                             // 左右の入力があったかのフラグ.

    // 移動速度.
    private float Speed = 125.0f;

    // 当たり判定
    private Rigidbody rb;
    private Vector3 localGravity = new Vector3(0, -100, 0);

    // スコア.
    public Text ScoreText;
    public int score = 0;

    // サウンド
    public AudioClip sound1;
    AudioSource audioSource;

    // ターゲットマネージャー.
    GameObject manager;
    TargetManager script;

    void Start()
    {
        //rigitbodyの取得.
        rb = this.GetComponent<Rigidbody>();

        //サウンドのComponentを取得
        audioSource = GetComponent<AudioSource>();

        // ターゲットマネージャー.
        manager = GameObject.Find("TargetManager");
        script = manager.GetComponent<TargetManager>();
    }

    void Update()
    {
        if(script.TimeOverFrag == false)
        {
            ////////////////////////////////////////
            // ジャンプ中じゃないなら
            ////////////////////////////////////////
            if (JumpFrag == false)
            {
                ////////////////////////////////////////
                // 入力.
                ////////////////////////////////////////
                if (Input.GetKeyDown("right"))
                {
                    if (PositionNum < 2)
                    {
                        PositionNum++;
                        InputFrag = true;
                    }
                }
                if (Input.GetKeyDown("left"))
                {
                    if (PositionNum > 0)
                    {
                        PositionNum--;
                        InputFrag = true;
                    }
                }

                ////////////////////////////////////////
                // 入力から位置変更.
                ////////////////////////////////////////
                if (InputFrag == true)
                {
                    switch (PositionNum)
                    {
                        case 0:
                            transform.position = LeftPos;
                            InputFrag = false;
                            break;
                        case 1:
                            transform.position = CenterPos;
                            InputFrag = false;
                            break;
                        case 2:
                            transform.position = RightPos;
                            InputFrag = false;
                            break;
                    }
                }

                ////////////////////////////////////////
                // ジャンプ力.
                ////////////////////////////////////////
                if (Input.GetKey("space"))
                {
                    if (PowerUpFrag == true)
                    {
                        JumpPower += Time.deltaTime;
                        if (JumpPower >= maxLine)
                        {
                            PowerUpFrag = false;
                        }
                    }
                    else
                    {
                        JumpPower -= Time.deltaTime;
                        if (JumpPower <= 0)
                        {
                            PowerUpFrag = true;
                        }
                    }
                }
                // ジャンプボタンを離したらジャンプ力決定.
                if (Input.GetKeyUp("space"))
                {
                    // 目的地設定.
                    Vector3 pos = transform.position;
                    if (JumpPower < middleLine)
                    {
                        pos.y = 6;
                    }
                    else if (JumpPower < upLine)
                    {
                        pos.y = 17.5f;
                    }
                    else
                    {
                        pos.y = 29;
                    }
                    pos.z = 17.5f;

                    // 目的地に向かうベクトル.
                    direction = (pos - transform.position).normalized;
                    direction *= Speed;
                    // 目的地とベクトルを設定したのでJumpPowerをリセット.
                    JumpPower = 0;
                    // ジャンプフラグを立てる.
                    JumpFrag = true;
                }
            }
            ////////////////////////////////////////
            // ジャンプ中なら
            ////////////////////////////////////////
            else
            {
                // 目的地に向かう.
                if (BurstFrag == false)
                {
                    BurstFrag = true;
                    rb.AddForce(direction, ForceMode.VelocityChange);
                }
                // ジャンプ中の経過時間をプラス.
                JumpCount += Time.deltaTime;

                // 一定時間が経過したら元の位置に戻る.
                if (JumpCount > JumpBackTime)
                {
                    rb.velocity = Vector3.zero;

                    switch (PositionNum)
                    {
                        case 0:
                            transform.position = LeftPos;
                            break;
                        case 1:
                            transform.position = CenterPos;
                            break;
                        case 2:
                            transform.position = RightPos;
                            break;
                    }

                    BurstFrag = false;
                    JumpCount = 0;
                    JumpFrag = false;
                }
            }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Target")
        {
            score++;
            ScoreText.text = string.Format("Score:{0}", score);

            //音(sound1)を鳴らす
            audioSource.PlayOneShot(sound1);
        }
    }
}
