using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �N���b�N����ƃQ�[����ʂɑJ�ڂ���X�N���v�g.
/// </summary>

public class ClickScript : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("GameScene");
    }
}
