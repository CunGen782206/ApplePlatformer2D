using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelPass : MonoBehaviour
{
    public TextMeshProUGUI textMeshProUGUI;

    public UnityEvent OnLevelPassed;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        var sceneName = SceneManager.GetActiveScene();
        textMeshProUGUI.gameObject.SetActive(true);
        OnLevelPassed.Invoke();
        StartCoroutine(Delay(2, () => { SceneManager.LoadScene(sceneName.name); }));
    }


    IEnumerator Delay(float time, Action callback)
    {
        
        yield return new WaitForSeconds(1);
        callback();
    }
}