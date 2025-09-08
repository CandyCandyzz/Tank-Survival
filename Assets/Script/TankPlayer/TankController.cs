using UnityEngine;

public class TankController : MonoBehaviour
{
    private float inputX;
    private float inputZ;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float maxSpeed;

    [SerializeField] private WheelCollider[] wheelColliders;
    [SerializeField] private WheelCollider[] frontWheelColliders;

    [SerializeField] private float motorTorque;
    [SerializeField] private float brakeTorque;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private Transform[] wheels;
    
    private GameManager gameManager;

    [Header("Effect")]
    [SerializeField] private PlayEffect effect;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        

        rb.centerOfMass = new Vector3(0, -0.1f, 0);
    }

    private void LateUpdate()
    {
        //UpdateWheels();
    }
    private void Update()
    {
        if (gameManager.state != GameState.Playing) { return; }
        inputZ = Input.GetAxis("Vertical");
        inputX = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        Movement();
        UpdateWheels();
    }

    private void Movement()
    {
        Turn();

        float currentSpeed = rb.linearVelocity.magnitude;
        float forwardSpeed = Vector3.Dot(rb.linearVelocity, transform.forward);

        if (inputZ != 0)
        {
            if ((inputZ > 0 && forwardSpeed >= 0) || (inputZ < 0 && forwardSpeed <= 0))
            {
                float realTorque = (currentSpeed < maxSpeed) ? inputZ * motorTorque : 0f;
                foreach (var wheel in wheelColliders)
                {
                    wheel.motorTorque = realTorque;
                    wheel.brakeTorque = 0;
                }
            }
            else
            {
                Brake();
            }
        }
        else
        {
            Brake();
        }
    }

    private void Turn()
    {
        foreach (var wheel in frontWheelColliders)
        {
            wheel.steerAngle = inputX * maxSteerAngle;
        }
    }


    private void Brake()
    {
        foreach (var wheel in wheelColliders)
        {
            wheel.brakeTorque = brakeTorque;
        }
    }

    public void Die()
    {
        effect.GetnPlay(transform, 0, 1);
        gameObject.SetActive(false);
        Destroy(gameObject,2);
    }

    private void UpdateWheels()
    {
        for (int i = 0; i < wheelColliders.Length; i++)
        {
            Vector3 pos;
            Quaternion rot;

            wheelColliders[i].GetWorldPose(out pos, out rot);

            wheels[i].position = pos;
            wheels[i].rotation = rot;
        }
    }
}
