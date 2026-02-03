using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftPaddleController : PaddleController
{
    protected override float GetInput()
    {
        return Input.GetAxis("LeftPaddle");
    }
}