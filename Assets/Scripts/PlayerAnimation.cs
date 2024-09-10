using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private PlayerMovement _playerMovement;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        var velocity = _rigidbody2D.velocity;

        var scaleXOffset = Mathf.Abs(velocity.x / _playerMovement.HorizontalMovementSpeed);

        var scale = new Vector3(1 + scaleXOffset, 0.7f + 0.3f, 1);

        transform.localScale = scale;
    }
}