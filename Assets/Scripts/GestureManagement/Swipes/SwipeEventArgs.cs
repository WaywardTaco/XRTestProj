using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeEventArgs : EventArgs
{
    private ESwipeDirection _direction;
    private Vector2 _rawDirection;
    private Vector2 _position;
    private GameObject _hitObject;
    public ESwipeDirection Direction {
        get { return this._direction; } 
    }

    public SwipeEventArgs(ESwipeDirection direction, Vector2 rawDirection, Vector2 position, GameObject hitObject = null){
        this._direction = direction;
        this._rawDirection = rawDirection;
        this._position = position;
        this._hitObject = hitObject;
    }

}
