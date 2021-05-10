using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWithController : MonoBehaviour
{

    public void SetYawAngle(float yaw)
    {
        Vector3 angle = transform.eulerAngles;
        angle.y = yaw;
        transform.eulerAngles = angle;
    }

    public void Move(Vector3 velocity)
    {
        transform.Translate(velocity, Space.World);
    }
}
