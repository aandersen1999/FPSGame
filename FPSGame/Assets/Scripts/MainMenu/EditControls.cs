using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditControls : MonoBehaviour
{
    public delegate void Action();
    public event Action Edit;

    public static float mouseSensitity = 2.0f;
    #region Key binding stuff
    public static Dictionary<Keys, KeyCode> binds =
        new Dictionary<Keys, KeyCode>()
        {
            { Keys.Forward, KeyCode.W },
            { Keys.Backward, KeyCode.S },
            { Keys.Left, KeyCode.A },
            { Keys.Right, KeyCode.D },
            { Keys.Run, KeyCode.LeftShift },
            { Keys.Attack, KeyCode.Mouse0 },
            { Keys.Reload, KeyCode.R },
            { Keys.Drop, KeyCode.Q },
            { Keys.Use, KeyCode.E },
            { Keys.Jump, KeyCode.Space},
            { Keys.Crouch, KeyCode.LeftControl },
            { Keys.Swap, KeyCode.Tab },
            { Keys.Pause, KeyCode.Escape }
        };
    
    private readonly Dictionary<Keys, KeyCode> defaultBinds =
        new Dictionary<Keys, KeyCode>()
        {
            { Keys.Forward, KeyCode.W },
            { Keys.Backward, KeyCode.S },
            { Keys.Left, KeyCode.A },
            { Keys.Right, KeyCode.D },
            { Keys.Run, KeyCode.LeftShift },
            { Keys.Attack, KeyCode.Mouse0 },
            { Keys.Reload, KeyCode.R },
            { Keys.Drop, KeyCode.Q },
            { Keys.Use, KeyCode.E },
            { Keys.Jump, KeyCode.Space },
            { Keys.Crouch, KeyCode.LeftControl },
            { Keys.Swap, KeyCode.Tab },
            { Keys.Pause, KeyCode.Escape }
        };
    private readonly Dictionary<Keys, KeyCode> RossBinds =
        new Dictionary<Keys, KeyCode>()
        {
            { Keys.Forward, KeyCode.E },
            { Keys.Backward, KeyCode.D },
            { Keys.Left, KeyCode.S },
            { Keys.Right, KeyCode.F },
            { Keys.Run, KeyCode.LeftShift },
            { Keys.Attack, KeyCode.Mouse0 },
            { Keys.Reload, KeyCode.R },
            { Keys.Drop, KeyCode.Q },
            { Keys.Use, KeyCode.Space },
            { Keys.Jump, KeyCode.Mouse1 },
            { Keys.Crouch, KeyCode.Mouse2 },
            { Keys.Swap, KeyCode.T },
            { Keys.Pause, KeyCode.Escape }
        };
    #endregion
    public Slider mouseSensitivtySlider;
    public GameObject ContentHolder;
    public GameObject EditKeyPrefab;

    public Dictionary<Keys, KeyCode> MenuKeys = new Dictionary<Keys, KeyCode>();

    private const float ySeperation = 45;

    private void OnEnable()
    {
        mouseSensitivtySlider.value = mouseSensitity;
        MenuKeys = binds;

        float yValue = 0;
        foreach(Keys key in System.Enum.GetValues(typeof (Keys)))
        {
            GameObject newKey = Instantiate(EditKeyPrefab, ContentHolder.transform);
            EditKey classRef = newKey.GetComponent<EditKey>();
            classRef.keyReference = key;
            classRef.controls = this;
            newKey.transform.position += Vector3.down * yValue;

            yValue += ySeperation;
        }
    }

    public void Apply()
    {
        binds = MenuKeys;
        mouseSensitity = mouseSensitivtySlider.value;
        CustomInput.SetKeys(binds[Keys.Forward], binds[Keys.Backward], binds[Keys.Left], binds[Keys.Right]);
        gameObject.SetActive(false);
    }

    public void Discard()
    {
        gameObject.SetActive(false);
    }

    public void Default()
    {
        MenuKeys = defaultBinds;
        Edit?.Invoke();
    }

    public void RossMode()
    {
        MenuKeys = RossBinds;
        Edit?.Invoke();
    }
}

public enum Keys : byte
{
    Forward,
    Backward,
    Left,
    Right,
    Run,
    Attack,
    Reload,
    Drop,
    Use,
    Jump,
    Crouch,
    Swap,
    Pause
}