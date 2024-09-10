using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D mRigidbody2D;

    public float HorizontalMovementSpeed = 5;
    public float JumpSpeed = 12;

   
    public float GravityMultiplier = 2;

   public float FallMultiplier = 1;

    public UnityEvent OnJump;
    public UnityEvent OnLand;


    public float MinJumpTime = 0.2f; //最小跳跃时间
    public float MaxJumpTime = 0.5f; //最大跳跃时间

    private float mHorizontalInput = 0; //横轴输入
    private float mCurrentJumpTime = 0; //当前跳跃时间
    private bool mJumpPressed = false; //是否按压跳跃键 

    // Start is called before the first frame update
    void Start()
    {
        mRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mHorizontalInput = Input.GetAxis("Horizontal");
        float y;
        if (Input.GetKeyDown(KeyCode.K) && collisionObjectCount > 0)
        {
            OnJump?.Invoke(); //运行事件
            mJumpPressed = true; //按键拿下
            if (JumpState == JumpStates.NotJump) //判断当前是否没有跳跃
            {
                JumpState = JumpStates.MinJump; //设置为最小跳跃
                mCurrentJumpTime = 0; //跳跃时间归零
            }
        }

        if (Input.GetKeyUp(KeyCode.K))
        {
            mJumpPressed = false;
        }

        mCurrentJumpTime += Time.deltaTime;
    }

    public enum JumpStates
    {
        NotJump,
        MinJump,
        ControlJump
    }

    public JumpStates JumpState = JumpStates.NotJump;

    private void FixedUpdate()
    {
        if (JumpState == JumpStates.MinJump)
        {
            mRigidbody2D.velocity = new Vector2(mRigidbody2D.velocity.x, JumpSpeed);
            if (mCurrentJumpTime >= MinJumpTime)
            {
                JumpState = JumpStates.ControlJump;
            }
        }
        else if (JumpState == JumpStates.ControlJump) //给一个缓冲的上升力
        {
            mRigidbody2D.velocity = new Vector2(mRigidbody2D.velocity.x, JumpSpeed);
            if (!mJumpPressed || mJumpPressed && mCurrentJumpTime >= MaxJumpTime)
            {
                JumpState = JumpStates.NotJump;
            }
        }

        mRigidbody2D.velocity = new Vector2(mHorizontalInput * HorizontalMovementSpeed, mRigidbody2D.velocity.y);

        if (mRigidbody2D.velocity.y > 0 && JumpState != JumpStates.NotJump)
        {
            var progress = mCurrentJumpTime / MaxJumpTime;

            float jumpGravityMultiplier = GravityMultiplier;

            if (progress > 0.5f)
            {
                jumpGravityMultiplier = GravityMultiplier * (1 - progress);
            }

            mRigidbody2D.velocity +=
                Physics2D.gravity * jumpGravityMultiplier * Time.deltaTime; // Physics2D.gravity是一个世界重力（可以Unity通过去设置）
        }
        else if (mRigidbody2D.velocity.y < 0)
        {
            mRigidbody2D.velocity += Physics2D.gravity * FallMultiplier * Time.deltaTime;
        }
    }

    public int collisionObjectCount = 0;

    private void OnCollisionEnter2D(Collision2D other)
    {
        OnLand?.Invoke();
        collisionObjectCount++;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        collisionObjectCount--;
    }
}