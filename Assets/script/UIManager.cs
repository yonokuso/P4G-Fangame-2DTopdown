using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public PlayerAction playerAction;
    public Slider HPgage;
    public static UIManager instance;
    public GameObject UIReBtn;
    public GameObject UIQuitBtn;
    bool isHpZero;



    // Start is called before the first frame update
    void Start()
    {
        HPgage.value = 100;
        instance = this;        
    }

    // Update is called once per frame
    void Update()
    {
        // HP Gage
        if (HPgage.value > 0.0f)
        {
            HPgage.value -= Time.deltaTime;
        }
        else
        {
            //isHpZero = true;

            UIReBtn.SetActive(true);
            UIQuitBtn.SetActive(true);
        }
    }
}
