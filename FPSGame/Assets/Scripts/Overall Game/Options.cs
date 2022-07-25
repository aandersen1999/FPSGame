using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options
{
    public Keybinds Ross = new Keybinds();

    public Keybinds keys = new Keybinds();

    
}

public struct Keybinds
{
    #region Keys
    KeyCode run;
    KeyCode attack;
    KeyCode reload;
    KeyCode drop;
    KeyCode interact;
    KeyCode jump;
    KeyCode crouch;
    KeyCode next;

    public KeyCode Run { get { return run; } }
    public KeyCode Attack { get { return attack; } }
    public KeyCode Reload { get { return reload; } }
    public KeyCode Drop { get { return drop; } }
    public KeyCode Interact { get { return interact; } }
    public KeyCode Jump { get { return jump; } }
    public KeyCode Crouch { get { return crouch; } }
    public KeyCode Next { get { return next; } }
    #endregion

    public Keybinds(KeyCode _run = KeyCode.LeftShift, KeyCode _attack = KeyCode.Mouse0,
                    KeyCode _reload = KeyCode.R, KeyCode _drop = KeyCode.Q, KeyCode _inter = KeyCode.E,
                    KeyCode _jump = KeyCode.Space, KeyCode _crouch = KeyCode.LeftControl,
                    KeyCode _next = KeyCode.Tab)
    {
        run = _run;
        attack = _attack;
        reload = _reload;
        drop = _drop;
        interact = _inter;
        jump = _jump;
        crouch = _crouch;
        next = _next;
    }
}

