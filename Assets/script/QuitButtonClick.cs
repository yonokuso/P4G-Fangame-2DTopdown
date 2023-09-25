using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButtonClick : MonoBehaviour
{
   

    /// <summary>
    /// 게임종료. 전처리기를 이용해 에디터 아닐때 종료.
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