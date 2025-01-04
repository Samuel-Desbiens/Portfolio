using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement
{
    public bool done { get; private set; } = false;
    public Sprite icon { get; private set; }
    public string name { get; private set; }
    public string desc { get; private set; }


    public Achievement(string _name, string _desc, Sprite _icon)
    {
        name = _name;
        desc = _desc;
        icon = _icon;
    }

  public void SetDone(bool _done) { done = _done; }

  public bool IsDone() { return done; }
}
