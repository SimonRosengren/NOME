using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovableObject : MonoBehaviour
{
    public Vector3 minLimit;
    public Vector3 maxLimit;
    public bool minLimitX = false, minLimitY = false, minLimitZ = false;
    public bool maxLimitX = false, maxLimitY = false, maxLimitZ = false;
    private float newX, newY, newZ;
    private int xOOB = 0, yOOB = 0, zOOB = 0;

    void Update()
    {
        /*Returns an int that describes how and if the object is OutOfBounds or not 
          0: not OOB 
          1: above max value 
          -1: below min value */
        xOOB = CheckOOB(minLimit.x, maxLimit.x, minLimitX, maxLimitX, transform.position.x);
        yOOB = CheckOOB(minLimit.y, maxLimit.y, minLimitY, maxLimitY, transform.position.y);
        zOOB = CheckOOB(minLimit.z, maxLimit.z, minLimitZ, maxLimitZ, transform.position.z);

        if (xOOB != 0 || yOOB != 0 || zOOB != 0)
        {
            /* Sets transform of object to min or max if they are outside min or max range respectively. */
            switch (xOOB)
            {                
                case -1:
                    newX = minLimit.x;
                    break;
                case 1:
                    newX = maxLimit.x;
                    break;
                default:
                    newX = transform.position.x;
                    break;
            }
            switch (yOOB)
            {
                case -1:
                    newY = minLimit.y;
                    break;
                case 1:
                    newY = maxLimit.y;
                    break;
                default:
                    newY = transform.position.y;
                    break;
            }
            switch (zOOB)
            {
                case -1:
                    newZ = minLimit.z;
                    break;
                case 1:
                    newZ = maxLimit.z;
                    break;
                default:
                    newZ = transform.position.z;
                    break;
            }
            transform.position = new Vector3(newX, newY, newZ);            
        }
    }
    /* Checks if object is out of bounds in max or min range */
    int CheckOOB(float minLimit, float maxLimit, bool isLimitMin, bool isLimitMax, float pos)
    {
        if (isLimitMin)
        {
            if (pos < minLimit)
            {
                return -1;
            }
        }
        if (isLimitMax)
        {
            if (pos > maxLimit)
            {
                return 1;
            }
        }
        return 0;        
    }
}
