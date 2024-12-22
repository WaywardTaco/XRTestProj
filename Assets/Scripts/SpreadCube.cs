using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadCube : MonoBehaviour, ISpreadable
{
    [SerializeField] private float _scaleRate = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSpread(SpreadEventArgs args){
        float delta = args.Delta;

        this.gameObject.transform.localScale *= (delta * this._scaleRate);
    }
}
