using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背景を動かすスクリプト.
/// </summary>

public class RotationSky : MonoBehaviour
{
    float curRot = 50.0f;

    void Start()
    {
        //フレームレート設定.
        Application.targetFrameRate = 60;

        RenderSettings.skybox.SetFloat("_Rotation", curRot);
    }
}
