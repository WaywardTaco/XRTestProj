using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanEventArgs : EventArgs
{
    private Touch[] _trackedFingers;
    private GameObject _hitObject;
    public Touch[] Fingers {
        get { return this._trackedFingers; }
    }
    public GameObject HitObject {
        get { return this._hitObject;}
    }

    public PanEventArgs(Touch[] trackedFingers, GameObject hitObject = null) {
        this._trackedFingers = trackedFingers;
        this._hitObject = hitObject;
    }
    
}
