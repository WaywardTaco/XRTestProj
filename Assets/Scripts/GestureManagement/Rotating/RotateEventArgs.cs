using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RotateEventArgs : EventArgs
{
    private Touch[] _trackedFingers;
    private ERotateDirection _direction;
    private float _angleDelta;
    private GameObject _hitObject;

    public Touch[] Fingers {
        get { return this._trackedFingers; }
    }
    public ERotateDirection Direction {
        get { return this._direction; }
    }
    public float AngleDelta {
        get { return this._angleDelta;}
    }
    public GameObject HitObject {
        get { return this._hitObject;}
    }

    public RotateEventArgs(Touch[] trackedFingers, ERotateDirection rotateDirection, float angleDelta, GameObject hitObject = null) {
        this._trackedFingers = trackedFingers;
        this._direction = rotateDirection;
        this._angleDelta = angleDelta;
        this._hitObject = hitObject;
    }
    
}
