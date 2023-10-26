using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class GrappleHandler : MonoBehaviour
{
    [SerializeField] private DistanceJoint2D _distanceJoint2D;

    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private int iMouseButtonToFire = 0;
    [SerializeField] private float fDistanceToMutate = 0.1f;
    [SerializeField] private float fMaxDistance = 15f;
    private bool bIsConnected = false;
    private Camera mainCamera;

    private bool bRbConnected = false;
    private Rigidbody2D ConnectedRb;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;

        if (TryGetComponent<DistanceJoint2D>(out _distanceJoint2D))
        {
        }

        if (TryGetComponent<LineRenderer>(out _lineRenderer))
        {
            _lineRenderer.positionCount = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //input to lower or increase the length
        switch (Input.mouseScrollDelta.y)
        {
            case >= 1 when bIsConnected:
                if (bRbConnected)
                {
                    Vector2 Dir = (ConnectedRb.position - (Vector2)transform.position ).normalized;
                    ConnectedRb.AddForce(Dir * 1.5f,ForceMode2D.Force);
                    break;
                }
                _distanceJoint2D.distance += fDistanceToMutate;
                if (_distanceJoint2D.distance > fMaxDistance) 
                    _distanceJoint2D.distance = fMaxDistance;  
                break;
            case <= -1 when bIsConnected:
                if (bRbConnected)
                {
                    Vector2 Dir = ( ConnectedRb.position - (Vector2)transform.position ).normalized;
                    ConnectedRb.AddForce(Dir /1.5f,ForceMode2D.Force);
                    break;
                }
                _distanceJoint2D.distance -= fDistanceToMutate;
                if (_distanceJoint2D.distance < 1) 
                    _distanceJoint2D.distance = 1;  
                break;
        }
        
        //input to fire
        if (Input.GetMouseButtonDown(iMouseButtonToFire))
        {
            //check if the hit location is valid
            Vector2 _MouseToWorldPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            Collider2D[] cols = Physics2D.OverlapCircleAll(_MouseToWorldPoint, 0.01f);

            Collider2D col = cols.ToList().Find((x) => x.CompareTag("GrapplePoint") );
            
            if (col)
            {
                bIsConnected = true;
                //fire
                //set enabled
                _lineRenderer.enabled = true;
                _distanceJoint2D.enabled = true;
                //set the line render point
                _lineRenderer.SetPosition(1, _MouseToWorldPoint);
                //set the connected anchor
                _distanceJoint2D.connectedAnchor = _MouseToWorldPoint;

                _distanceJoint2D.distance = math.distance((Vector2)transform.position, _MouseToWorldPoint);
                //if hit a rigidbody connect assign it to the distance joint
                if (col.TryGetComponent<Rigidbody2D>(out ConnectedRb))
                {
                    bRbConnected = true;
                    _distanceJoint2D.connectedBody = ConnectedRb;
                }

            }
            else
            {
                //if fire and hit nothing
                //turn off
                bIsConnected = false;
                bRbConnected = false;

                _distanceJoint2D.connectedBody = null;
                _distanceJoint2D.connectedAnchor = Vector2.zero;
                
                _lineRenderer.enabled = false;
                _distanceJoint2D.enabled = false;
                ConnectedRb = null;


            }

        }
        
        //if the player is connected to something, set first index of the linerenderer
        if(bIsConnected)
            _lineRenderer.SetPosition(0,transform.position);
        
        if(bRbConnected && ConnectedRb)
            _lineRenderer.SetPosition(1,ConnectedRb.transform.position);

     
    }
}
