using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Apple : MonoBehaviour
{

    public UnityEvent OnGet;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        OnGet?.Invoke();
        Destroy(gameObject);
    }
}
