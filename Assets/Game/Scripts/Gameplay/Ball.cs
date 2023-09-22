using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private ColorProperty ballColor;
    private Rigidbody rb;


    private void SetRigidBodiesProperties(RigidBodyProperties properties)
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = properties.drag;
        rb.angularDrag = properties.angularDrag;
        rb.collisionDetectionMode = properties.collisionDetectionMode;
        rb.interpolation = properties.interpolation;
        rb.constraints = properties.constraints;
    }

    private void SetColor(Color color)
    {
        ballColor = GetComponentInChildren<ColorProperty>();
        ballColor.ChangeColor(color);
    }


    public void SetData(Color clr, RigidBodyProperties properties)
    {
        SetColor(clr);
        SetRigidBodiesProperties(properties);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unparent"))
            this.transform.SetParent(null);

        if (other.CompareTag("BucketDestroyTrigger"))
            Destroy(this.gameObject);


        if (other.CompareTag("WorldDestroyer"))
        {
            LevelManager.instance.CheckLevelFail();
            Destroy(this.gameObject);
        }
    }
}