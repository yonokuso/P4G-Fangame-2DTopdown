using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButtonClick : MonoBehaviour
{
   

    /// <summary>
    /// ��������. ��ó���⸦ �̿��� ������ �ƴҶ� ����.
    /// </summary>
    public void OnQuitButtonClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnReButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

}