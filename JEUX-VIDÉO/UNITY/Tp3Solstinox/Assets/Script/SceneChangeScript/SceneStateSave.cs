using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SceneStateSave : ScriptableObject
{
    private bool _doorState;
    private bool _ghostState;
    private bool _keyState;
    private bool _tokenState;

    public bool DoorState
    {
        get { return _doorState; }
        set { _doorState = value; }
    }

    public bool GhostState
    {
        get { return _ghostState; }
        set { _ghostState = value; }
    }
     public bool KeyState
    {
        get { return _keyState; }
        set { _keyState = value; }
    }
    public bool TokenState
    {
        get { return _tokenState; }
        set { _tokenState = value; }
    }
    private void OnEnable()
    {
        hideFlags = HideFlags.DontUnloadUnusedAsset;
        _doorState = false;
        _ghostState = false;
        _keyState = false;
        _tokenState = false;
    }

}
