using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPaddleController : PaddleController
{
    protected override float GetInput()
    {
        return Input.GetAxis("RightPaddle");
    }
}
