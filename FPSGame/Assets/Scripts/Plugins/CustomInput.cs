using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomInput
{
    private static KeyCode upKey = KeyCode.W;
    private static KeyCode downKey = KeyCode.S;
    private static KeyCode leftKey = KeyCode.A;
    private static KeyCode rightKey = KeyCode.D;

    public static float GetHorizontal()
    {
        float value = 0.0f;
        if (Input.GetKey(leftKey))
        {
            value += -1.0f;
        }
        if (Input.GetKey(rightKey))
        {
            value += 1.0f;
        }
        return value;
    }

    public static float GetVertical()
    {
        float value = 0.0f;
        if (Input.GetKey(downKey))
        {
            value += -1.0f;
        }
        if (Input.GetKey(upKey))
        {
            value += 1.0f;
        }
        return value;
    }

    public static void SetKeys(KeyCode upKey, KeyCode downKey, KeyCode leftKey, KeyCode rightKey)
    {
        CustomInput.upKey = upKey;
        CustomInput.downKey = downKey;
        CustomInput.leftKey = leftKey;
        CustomInput.rightKey = rightKey;
    }
}
