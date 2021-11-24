using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForUpdateFrames : CustomYieldInstruction
{
    int currentFrame = 0;
    int duration;

    public override bool keepWaiting
    {
        get
        {
            if(currentFrame >= duration) { return false; }
            else
            {
                currentFrame++;
                return true;
            }
        }
    }

    public WaitForUpdateFrames(int _duration) { duration = _duration; }
}
