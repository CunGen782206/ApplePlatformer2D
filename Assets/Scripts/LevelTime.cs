using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTime : MonoBehaviour
{

    public TextMeshProUGUI TextMeshProUGUI;


    private float mCurrentSeconds = 0;
    // Update is called once per frame
    void Update()
    {
        mCurrentSeconds += Time.deltaTime;
        if (Time.frameCount % 20 == 0)
        {
            TextMeshProUGUI.text = ((int)mCurrentSeconds).ToString();
        }
    }
}
