using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DragProperty {
    [Tooltip ("Minimum allowable time for a Drag.")]
    [SerializeField] private float _time = 0.8f;
    
    public float Time {
        get {return this._time;}
        set {this._time = value;}
    }
}
