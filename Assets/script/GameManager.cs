using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI �۾��� �� �ʼ�!
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

public class UseUnityEditorClass : MonoBehaviour
{
    public void Function()
    {
#if UNITY_EDITOR
        // ����Ƽ �����Ϳ��� Debug �����θ� �۵��ؾ� �ϴ� �ڵ�...
#endif
    }
}


// �������� ����, ���ŷ� ����, ��ȭâ ���� ���..

public class GameManager : MonoBehaviour
{
    //public TalkManager talkManager;
    //public QuestManager questManager;
    public PlayerAction player;
    public GameObject talkPanel;
    public GameObject scanObject;
    public Image portraitImg;
    public Text talkText;
    public bool isAction;
    public int talkIndex;

    public int stageIndex;
    public GameObject[] Stages;


    void Update()
    {
    }

    public void Action(GameObject scanObj)
    {
        scanObject = scanObj;
        ObjData objData = scanObject.GetComponent<ObjData>();
        //Talk(objData.id, objData.isNPC);

        //talkPanel.SetActive(isAction);

    }

    // Next Stage
    public void NextStage()
    {
        if (stageIndex < Stages.Length - 1)
        {
            // Change Stage
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
        }
        else
        {
            Time.timeScale = 0;
            // ���� ��ư �����
        }
    }


}
