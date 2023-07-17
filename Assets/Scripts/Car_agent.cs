using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class Car_agent : Agent
{
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentbreakForce;
    private bool isBreaking;
    private int a;
    public GameObject car;
    // GameObject car1;
    //Getting parking location
    [SerializeField] private Road road;
    Renderer renderer1;
    [SerializeField] private Parking_spot parking_Spot;
    //Settings
    [SerializeField] private float motorForce, breakForce, maxSteerAngle;

    // Wheel Colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    // Wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;
    public string teeemp_name = "teemp";
    private float Score = 0;
    public float dist = 0;
    [SerializeField] env env;
    public override void OnEpisodeBegin()
    {
        // Debug.Log(Score);
        renderer1 = road.GetComponent<Renderer>();
        renderer1.material.color = new Color(0.2196078f,0.2196078f,0.2196078f);
        Score = 0;
        Rigidbody te = GetComponent<Rigidbody>();
        te.velocity = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        transform.localPosition = new Vector3(0.0f,0.0f,-12.1f);
        env.ResetEnv();
        te.velocity = Vector3.zero;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        float dist1 = Vector3.Distance(parking_Spot.transform.localPosition,transform.localPosition);
        Vector3 distance = parking_Spot.transform.localPosition-transform.localPosition;
        float angle = Vector3.Angle(parking_Spot.transform.localEulerAngles,transform.localEulerAngles);
        sensor.AddObservation(dist); // Adding distance
        sensor.AddObservation(distance);
        if(dist1<1){
            Score+=50;
            AddReward(50.0f);
            renderer1.material.color = Color.green;
        }
        Score += dist - dist1 - 0.1f;
        AddReward(dist-dist1);
        dist = dist1;
        sensor.AddObservation(angle); //Adding angle
    }
    void OnCollisionEnter(Collision collison){
        Score -=50;
        AddReward(-50);
    }
    void OnTriggerEnter(Collider collider){
        if(collider.gameObject.name=="Plane (1)"){
            EndEpisode();
        }
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        // Acceleration Input
        continuousActions[0] = Input.GetAxis("Vertical");
        
        //Steering Input
        continuousActions[1] = Input.GetAxis("Horizontal");

        // Breaking Input
        discreteActions[0] = Input.GetKey(KeyCode.Space) ? 1 : 0; //Converting bool to int
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        isBreaking = actions.DiscreteActions[0] != 0; //Converting int to bool
        verticalInput = actions.ContinuousActions[0];
        horizontalInput = actions.ContinuousActions[1];
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }
    private void HandleMotor() {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
    }

    private void ApplyBreaking() {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering() {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels() {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform) {
        Vector3 pos;
        Quaternion rot; 
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
