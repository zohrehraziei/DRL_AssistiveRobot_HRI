using System;
using UnityEngine;

public class Axis : MonoBehaviour
{
   public Vector3 rotationAxis;
   public int AxisID;
   
   public float MinAngle;
   public float MaxAngle;

   public float angleChange = 0f;
   Quaternion _initialOrientation;
   private void Awake()
   {
        _initialOrientation = transform.localRotation;
   }

   public void InitAngle()
    {
        angleChange = 0;
        var change = Quaternion.AngleAxis(angleChange, rotationAxis);
        transform.localRotation = _initialOrientation * change;
    }

    float rx, ry;
    public void RotateAngle(float angle, float rSize)
    {
        //angleChange += angle;
        var change = Quaternion.AngleAxis(angle * rSize, rotationAxis);
        transform.localRotation = _initialOrientation * change;

        if(rotationAxis.x == 1)
        {
            rx = transform.localEulerAngles.x;
            if( rx < MinAngle || rx > MaxAngle)
            {

            }
        }
        
    }

   public float MinAngleRadians => Mathf.Deg2Rad * MinAngle;
   public float MaxAngleRandians => Mathf.Deg2Rad * MaxAngle;

   
}
