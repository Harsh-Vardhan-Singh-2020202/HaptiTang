using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShotStones : Interactable
{
    public Transform slingPullbackPoint;
    public Transform drawFrom;

    public float shotForceMultiplier;

    [HideInInspector] public bool released;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        released = false;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if(!released)
            slingPullbackPoint.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.1f);
    }

    override public void Interact()
    {
        released = true;
        Vector3 projectileDirection = drawFrom.transform.position - transform.position;
        
        transform.parent = null;
        Rigidbody projectileRigidBody = GetComponent<Rigidbody>();
        projectileRigidBody.AddForce(projectileDirection * shotForceMultiplier, ForceMode.Impulse);
        projectileRigidBody.isKinematic = false;
        projectileRigidBody.useGravity = true;
        slingPullbackPoint.position = drawFrom.position;
    }
}
