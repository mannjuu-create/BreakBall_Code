using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �w�i�𓮂����X�N���v�g.
/// </summary>

public class RotationSky : MonoBehaviour
{
    float curRot = 50.0f;

    void Start()
    {
        //�t���[�����[�g�ݒ�.
        Application.targetFrameRate = 60;

        RenderSettings.skybox.SetFloat("_Rotation", curRot);
    }
}
