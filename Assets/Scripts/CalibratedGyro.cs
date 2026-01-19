using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalibratedGyro : MonoBehaviour
{
    Quaternion gyroOffset;
    Quaternion calibratedGyro;

    bool calibrated = false;

    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        Calibrate();

        if (calibrated)
        {
            //Calibration
            calibratedGyro = Quaternion.Inverse(gyroOffset) * Input.gyro.attitude;

            //Registration
            Quaternion unityCalibratedGyro = new Quaternion(calibratedGyro.x, calibratedGyro.y, -calibratedGyro.z, -calibratedGyro.w);

            //Interaction
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Input.gyro.attitude, Time.deltaTime * 4);
        }
    }

    void Calibrate()
    {
        if (!calibrated && (Input.gyro.attitude.x != 0 || Input.gyro.attitude.y != 0 || Input.gyro.attitude.z != 0))
        {
            Debug.Log("Calibrated:" + Input.gyro.attitude);
            gyroOffset = Input.gyro.attitude;
            calibrated = true;
        }
    }

}
