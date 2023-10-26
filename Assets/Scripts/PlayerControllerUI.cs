using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer Renderer;
    [SerializeField] private bool bFacingRight = true;

    //int iWay is either -1 or 1
    public void FlipPlayerBody(int iWay)
    {
        if (iWay == -1)
        {
            bFacingRight = false;
        }
        else if (iWay == 1)
        {
            bFacingRight = true;
        }
        else
        {
            return;
        }

        Renderer.flipX = !bFacingRight;
    }
}
