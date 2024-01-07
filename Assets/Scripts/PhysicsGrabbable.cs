using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PhysicsGrabbable : Grabbable
{
    private Rigidbody _rigidbody;
    private Collider _collider;
    //[SerializeField] private bool _upDown = true;
    //private GameObject rif;

    protected override void Start()
    {
        base.Start();
        _collider = GetComponent<Collider>();
        _rigidbody = GetComponent<Rigidbody>();
        //rif = myObject = GameObject.Find("carro");
    }

    public override void Grab(GameObject grabber)
    {
        _collider.enabled = false;
        _rigidbody.isKinematic = true;
    }
   /* public override void Grab_(GameObject grabber)
    {
        _collider.enabled = false;
        _rigidbody.isKinematic = true;
    }*/

    public override void Drop()
    {
        _collider.enabled = true;
        _rigidbody.isKinematic = false;
    }
}
