using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MxM;

public class PlayerSpeedControl : MonoBehaviour
{
    public GameObject chararcter;
    public float walkSpeed;
    public float walkPosBias;
    public float walkDirBias;

    public float runSpeed;
    public float runPosBias;
    public float runDirBias;
    public float sprintSpeed;
    public float sprintPosBias;
    public float sprintDirBias;

    public MxMAnimator mmAnimator;
    public MxMTrajectoryGenerator mmTrajGen;

    void Start()
    {
        mmAnimator = chararcter.GetComponentInChildren<MxMAnimator>();
        mmTrajGen = chararcter.GetComponentInChildren<MxMTrajectoryGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Sprint"))
        {
            mmTrajGen.MaxSpeed = sprintSpeed;
            mmTrajGen.PositionBias = sprintPosBias;
            mmTrajGen.DirectionBias = sprintDirBias;
            mmAnimator.SetCalibrationData("Sprint");

        }
        else if (Input.GetButton("Walk"))
        {
            mmTrajGen.MaxSpeed = walkSpeed;
            mmTrajGen.PositionBias = walkPosBias;
            mmTrajGen.DirectionBias = walkDirBias;
            mmAnimator.SetCalibrationData("Walk");

        }
        else 
        {
            mmTrajGen.MaxSpeed = runSpeed;
            mmTrajGen.PositionBias = runPosBias;
            mmTrajGen.DirectionBias = runDirBias;
            mmAnimator.SetCalibrationData("run");

        }
    }
}
