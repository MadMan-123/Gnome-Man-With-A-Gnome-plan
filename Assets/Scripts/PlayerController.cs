using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float fSpeed = 0;
    [SerializeField] private float fWalkSpeed = 5f;
    [SerializeField] private float fRunSpeed = 7.5f;
    [SerializeField] private float fJumpForce = 10f;
    [SerializeField] Collider2D[] cols;

    
    private BoxCollider2D _collider2D;
    private Rigidbody2D _rigidbody;
    [SerializeField] private bool bIsGrounded = false;
    private bool bCanMove = true;
    void Start()
    {
        fSpeed = fWalkSpeed;
        //caching collider and rigidbody
        if (TryGetComponent<BoxCollider2D>(out _collider2D))
        {
            
        }

        if (TryGetComponent<Rigidbody2D>(out _rigidbody))
        {
            _rigidbody.drag = 2.5f;
            _rigidbody.angularDrag = 1;
        }

        if (TryGetComponent<HealthHandler>(out var HH))
        {
            HH.OnDeath += Death;
        }
    }

    private void Death(HealthHandler obj)
    {
        bCanMove = true;
    }

    void Death()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        bIsGrounded = GroundCheck();
        Move();
    }

    private bool GroundCheck()
    {
        //ground check 
        Vector3 position = transform.position;

        cols = Physics2D.OverlapBoxAll(
            new Vector2(
                position.x,
                position.y - transform.lossyScale.y / 1.6f
            ),
            new Vector2(
                .5f,
                .25f),
            0);

         return cols.Length > 0;
    }

    private void Move()
    {
        if(!bCanMove) return;
        
        float fXAxis = 0, fYAxis = 0;

        //handle moving on x axis
        fXAxis = Input.GetAxis("Horizontal");
        
        //handle sprinting
        if (Input.GetKeyDown(KeyCode.LeftShift) && bIsGrounded)
        {
            //accelerate
            fSpeed = fRunSpeed;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift) && bIsGrounded)
        {
            //set to normal speed
            fSpeed = fWalkSpeed;
        }
        

        //jump on y axis
        //use get key down so the jump will happen once
        if ( Input.GetKeyDown(KeyCode.Space) && bIsGrounded) 
        { 
            fYAxis = Input.GetAxis("Jump");
            _rigidbody.AddForce(new Vector2(fXAxis * fSpeed,fYAxis * fJumpForce), ForceMode2D.Force);
        }
        else
        {
            _rigidbody.AddForce(new Vector2(fXAxis * fSpeed,0), ForceMode2D.Force);

        }

        int iTemp = 0;
        if (fXAxis > 0)
            iTemp = 1;
        else if ( fXAxis < 0)
            iTemp = -1;

        if (TryGetComponent<PlayerControllerUI>(out var UI))
        {
            UI.FlipPlayerBody(iTemp);
        }



    }

    private void OnDrawGizmos()
    {
        Gizmos.color = bIsGrounded ? Color.green : Color.red;
        
        var position = transform.position;
        Gizmos.DrawWireCube(new Vector3( 
            position.x, 
            position.y - transform.lossyScale.y /1.6f
        ),
            new Vector2(
            .5f,
            .1f)
            );

       
    }
}
