using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalibratedAccel : MonoBehaviour
{
    Vector3 accelOffset;
    bool calibrated = false;

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Calibrate();

        if (calibrated)
        {
            //Calibration
            Vector3 calibratedAccel = Input.acceleration - accelOffset;

            //Registration 
            Vector3 unityCalibratedAccel = new Vector3(calibratedAccel.x, calibratedAccel.z, calibratedAccel.y);

            //Interaction
            Vector3 force = new Vector3(unityCalibratedAccel.x, 0, unityCalibratedAccel.z);
            this.GetComponent<Rigidbody>().AddForce(force * 20);
        }
    }
    void Calibrate()
    {
        //We ensure that we are receiving data from the sensor before calibrate
        if (!calibrated && Input.acceleration.magnitude > 0)
        {
            accelOffset = Input.acceleration;
            calibrated = true;
        }

    }
}
