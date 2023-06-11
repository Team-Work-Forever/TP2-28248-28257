using UnityEngine;
using System;
using System.Collections.Generic;

public class ThatsHowWeRoll : MonoBehaviour
{

    public enum Axel
    {
        Front,
        Rear
    }

    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;
        public WheelCollider wheelCollider;
        public GameObject wheelEffectObj;
        public Axel axel;
    }

    public float maxAcceleration = 30.0f;
    public float brakeAcceleration = 50.0f;

    public float turnSensitivity = 1.0f;
    public float maxSteerAngle = 30.0f;

    public CheckpointsPlayer checkpointsPlayer;

    public Transform _centerOfMass;

    public List<Wheel> wheels;

    float moveInput;
    float steerInput;

    public float distanceTraveled;
    private Vector3 lastPosition;
    public float velocity;
    public float updateInterval = 0.4f;
    private float timeSinceLastUpdate = 0f;

    public Rigidbody playerRb;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.centerOfMass = _centerOfMass.position;

        checkpointsPlayer = GetComponentInChildren<CheckpointsPlayer>();

        PlayerManager.instance.RegisterPlayer(this, transform.position);

        lastPosition = transform.position;
    }

    public void Update()
    {
        GetInputs();
        AnimationWheels();
        WheelEffects();

        Vector3 currentPosition = transform.position;
        float distance = Vector3.Distance(currentPosition, lastPosition);
        float currentSpeed = distance / Time.deltaTime;
        distanceTraveled += distance;
        timeSinceLastUpdate += Time.deltaTime;
        if (timeSinceLastUpdate >= updateInterval)
        {


            lastPosition = currentPosition;
            velocity = Mathf.RoundToInt(currentSpeed / 15);

            timeSinceLastUpdate = 0f;
        }
    }

    void LateUpdate()
    {
        Roll();
        Steer();
        Brake();
    }

    void GetInputs()
    {
        int playerIndex = PlayerManager.instance.players.IndexOf(this);
        if (playerIndex == 0)
        {
            moveInput = Input.GetAxis("Vertical2");
        }
        else
        {
            moveInput = Input.GetAxis("Vertical");
        }

        if (playerIndex == 0)
        {
            steerInput = Input.GetAxis("Horizontal2");
        }
        else
        {
            steerInput = Input.GetAxis("Horizontal");
        }

    }

    void Roll()
    {
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = moveInput * 600 * maxAcceleration * Time.deltaTime;
        }
    }

    void Brake()
    {
        int playerIndex = PlayerManager.instance.players.IndexOf(this);
        if (playerIndex == 0)
        {

            if (Input.GetKey(KeyCode.Space))
            {
                FindObjectOfType<AudioManager>().Play("Brake");
                foreach (var wheel in wheels)
                {
                    wheel.wheelCollider.brakeTorque = 3000 * brakeAcceleration * Time.deltaTime;
                }
            }
            else
            {
                foreach (var wheel in wheels)
                {
                    wheel.wheelCollider.brakeTorque = 0;
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.RightControl))
            {
                FindObjectOfType<AudioManager>().Play("Brake");
                foreach (var wheel in wheels)
                {
                    wheel.wheelCollider.brakeTorque = 3000 * brakeAcceleration * Time.deltaTime;
                }
            }
            else
            {
                foreach (var wheel in wheels)
                {
                    wheel.wheelCollider.brakeTorque = 0;
                }
            }
        }
    }

    void Steer()
    {
        foreach (var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var _steerAngle = steerInput * turnSensitivity * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }

    void AnimationWheels()
    {
        foreach (var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rot * Quaternion.Euler(0, -90, 0);
        }
    }

    void WheelEffects()
    {
        foreach (var wheel in wheels)
        {
            int playerIndex = PlayerManager.instance.players.IndexOf(this);
            if (playerIndex == 0)
            {
                if (Input.GetKey(KeyCode.Space) && wheel.axel == Axel.Rear)
                {
                    wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = true;
                }
                else
                {
                    wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = false;

                }
            }
            else
            {
                if (Input.GetKey(KeyCode.RightControl) && wheel.axel == Axel.Rear)
                {
                    wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = true;
                }
                else
                {
                    wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = false;

                }
            }
        }
    }

}