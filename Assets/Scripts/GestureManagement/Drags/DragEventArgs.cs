using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragEventArgs : EventArgs
{
    private Touch _trackedFinger;
    private GameObject _hitObject;

    public Touch Finger {
        get { return this._trackedFinger; }
    }

    public GameObject HitObject {
        get { return this._hitObject; }
    }

    public DragEventArgs(Touch finger, GameObject hitObject = null) {
        this._trackedFinger = finger ;
        this._hitObject = hitObject ;
    }
    
}
