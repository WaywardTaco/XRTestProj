using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOMover : MonoBehaviour
{
    [SerializeField] private float moveForce;
    [SerializeField] private Vector3 targetLocation;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        this.targetLocation = this.gameObject.transform.parent.transform.parent.position;
        this.targetLocation.y = this.transform.position.y;

        this.rb = GetComponent<Rigidbody>();
        Vector3 forceDir = Vector3.Normalize(this.targetLocation - this.transform.position);
        rb.AddForce(forceDir * moveForce);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position == this.targetLocation)
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision other) {
        Destroy(other.gameObject);
        Destroy(this.gameObject);
    }
}
