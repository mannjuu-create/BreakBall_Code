using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Image�n��UI�Ȃ̂Œǉ�
using UnityEngine.UI;

/// <summary>
/// �X�y�[�X�{�^�����������獶�̃Q�[�W�𑝌�������X�N���v�g.
/// </summary>

public class ColorGage : MonoBehaviour
{
    // �^�[�Q�b�g�}�l�[�W���[.
    GameObject manager;
    TargetManager script;

    public float _powerGage = 0;
    public bool PowerUpFrag = true;

    private bool JumpFrag = false;
    private float JumpCount = 0;

    private float JumpBackTime = 0.5f;      // �W�����v���Ă���߂�܂ł̎���.
    private float maxLine = 0.6f;           // �W�����v�̍ő�l

    //Image�^�̕ϐ�_image��錾���Ă���
    public Image _image;

    void Start()
    {
        //this����Ȃ����ǁA�����̂���A�Ƃ������ƂŐ����̂��߂�
        _image = this.GetComponent<Image>();

        // �^�[�Q�b�g�}�l�[�W���[.
        manager = GameObject.Find("TargetManager");
        script = manager.GetComponent<TargetManager>();
    }

    void Update()
    {
        if (script.TimeOverFrag == false)
        {
            // �v���C���[���W�����v������Ȃ��Ȃ�
            if (JumpFrag == false)
            {
                if (Input.GetKey("space"))
                {
                    //�W�����v�͑�����.
                    if (PowerUpFrag == true)
                    {
                        _powerGage += Time.deltaTime;
                        if (_powerGage >= maxLine)
                        {
                            PowerUpFrag = false;
                        }
                    }
                    //�W�����v�͌�����.
                    else
                    {
                        _powerGage -= Time.deltaTime;
                        if (_powerGage <= 0)
                        {
                            PowerUpFrag = true;
                        }
                    }
                }
                //�W�����v�{�^���𗣂�����.
                if (Input.GetKeyUp("space"))
                {
                    JumpFrag = true;
                    _powerGage = 0;
                }
            }
            //�W�����v���Ȃ�߂��Ă���܂ł̎��Ԃ��J�E���g.
            else
            {
                JumpCount += Time.deltaTime;
                if (JumpCount > JumpBackTime)
                {
                    JumpCount = 0;
                    JumpFrag = false;
                }
            }

            //�ő傪500�Ȃ̂ŁA����Z���Ĕ䗦�œ˂�����
            _image.fillAmount = _powerGage / maxLine;
        }
    }
}
