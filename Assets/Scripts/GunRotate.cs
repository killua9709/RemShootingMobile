using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotate : MonoBehaviour
{
    public void Gunrotate()
    {
        transform.Rotate(new Vector3(1000, 0, 0) * Time.deltaTime );
    }
}
