using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadEventArgs : EventArgs
{
    private Touch[] _trackedFingers;
    private float _distanceDelta;
    private GameObject _hitObject;

    public Touch[] Fingers {
        get { return this._trackedFingers; }
    }
    public float Delta {
        get { return this._distanceDelta;}
    }
    public GameObject HitObject {
        get { return this._hitObject;}
    }

    public SpreadEventArgs(Touch[] trackedFingers, float distanceDelta, GameObject hitObject = null) {
        this._trackedFingers = trackedFingers;
        this._distanceDelta = distanceDelta;
        this._hitObject = hitObject;
    }
    
}
