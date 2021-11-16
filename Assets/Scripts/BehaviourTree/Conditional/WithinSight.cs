using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using System;

public class WithinSight : Conditional
{
    // How wide of an angle the object can see
    public float horizontalfieldOfViewAngle;
    // How wide of an angle the object can see
    public float verticalfieldOfViewAngle;
    // How far the enemy can see
    public float range;
    // The tag of the targets
    public string targetTag;
    // The Layer on which check for colliders
    public LayerMask playerLayerMask;
    // Set the target variable when a target has been found so the subsequent tasks know which object is the target
    public SharedTransform target;
    public SharedBool inSight;

    public GameObject raycastOrigin;

    // A cache of all of the possible targets
    private Transform possibleTarget;
    private GameObject targets;

    public override void OnAwake()
    {
        // Cache all of the transforms that have a tag of targetTag
        targets = GameObject.FindGameObjectWithTag(targetTag);
        possibleTarget = targets.transform;
    }

    public override TaskStatus OnUpdate()
    {

        Collider[] listOfCollider = Physics.OverlapSphere(transform.position, range, playerLayerMask);
        if (listOfCollider.Length != 0)
        {
            // Return success if a target is within sight
            if (VerticalInSight(possibleTarget, verticalfieldOfViewAngle) || HorizontalInSight(possibleTarget, horizontalfieldOfViewAngle))
            {
                if (CheckForObstacles())
                {
                // Set the target so other tasks will know which transform is within sight
                target.Value = possibleTarget;
                inSight.Value = true;
                return TaskStatus.Success;
                }

                else 
                {
                    inSight.Value = false;
                    return TaskStatus.Failure;
                }
            }

            else
            {
                inSight.Value = false;
                return TaskStatus.Failure;
            }   
        }
        else
        {
            inSight.Value = false;
            return TaskStatus.Failure;
        }
    }

    // Returns true if targetTransform is within sight of current transform
    public bool VerticalInSight(Transform targetTransform, float fieldOfViewAngle)
    {
        Vector3 direction = targetTransform.position - (transform.position + transform.right);
        // An object is within sight if the angle is less than field of view
        return Vector3.Angle(direction, transform.forward) < fieldOfViewAngle;
    }

    public bool HorizontalInSight(Transform targetTransform, float fieldOfViewAngle)
    {
        Vector3 direction = targetTransform.position - transform.position;
        // An object is within sight if the angle is less than field of view
        return Vector3.Angle(direction, transform.forward) < fieldOfViewAngle;
    }

    public bool CheckForObstacles()
    {
        bool canSee = false;
        RaycastHit hitInfo;
        Physics.Linecast(raycastOrigin.transform.position, possibleTarget.position + new Vector3(0, 1.4f, 0), out hitInfo);
        Debug.DrawLine(raycastOrigin.transform.position, possibleTarget.position + new Vector3(0, 1.4f, 0), Color.green);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.transform.gameObject.CompareTag("Player"))
                canSee = true;
            else
                canSee = false;
        }
        return canSee;
    }
}