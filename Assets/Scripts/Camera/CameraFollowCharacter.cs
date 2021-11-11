using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowCharacter : MonoBehaviour
{
    [SerializeField] Vector3 offsetXYZ;
    [SerializeField] float smoothSpeed;
    [SerializeField] Transform target;

    Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.transform.position + offsetXYZ;

        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed, Mathf.Infinity, 10 * Time.fixedDeltaTime);

        transform.LookAt(transform);
    }

}
