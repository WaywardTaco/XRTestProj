using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RotateProperty {
    [Tooltip ("Minimum distance between fingers to be a rotation.")]
    [SerializeField] private float _minDistance = 0.75f;

    public float MinDistance {
        get { return _minDistance; }
    }

    [Tooltip ("Minimum angle change to be considered a rotation.")]
    [SerializeField] private float _minAngleChange = 0.4f;

    public float MinAngleChange {
        get {return this._minAngleChange;}
    }
}
