using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Thought I was gonna use this... Guess not.
public class WaitForUpdateFrames : CustomYieldInstruction
{
    private int currentFrame = 0;
    private int duration;

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
