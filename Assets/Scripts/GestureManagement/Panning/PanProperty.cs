using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PanProperty {
    [Tooltip ("Maximum distance between panning fingers.")]
    [SerializeField] private float _maxDistance = 0.7f;

    public float MaxDistance {
        get {return this._maxDistance;}
    }
}
