using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �N���b�N����ƃ^�C�g����ʂɑJ�ڂ���X�N���v�g.
/// </summary>

public class ClickGoTitle : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
