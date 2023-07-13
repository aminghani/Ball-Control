using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Policies;
using UnityEngine;
using System.Linq;

public class CubeController : Agent
{
    public float speed = 5f;  // Rotation speed in degrees per second
    Transform ball;
    Rigidbody ballRb;


    void Start()
    {
        Transform parent = transform.parent;
        ball = parent.GetChild(1);
        ballRb = ball.GetComponent<Rigidbody>();
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        int[] discreteActions = actionBuffers.DiscreteActions.ToArray();

        int rotateUp = discreteActions[0];
        int rotateForward = discreteActions[1];
        
        if (rotateUp == 0)
        {
            transform.Rotate(Vector3.right, -speed * Time.deltaTime);
        }
        else if (rotateUp == 1)
        {
            transform.Rotate(Vector3.right, speed * Time.deltaTime);
        }

        if (rotateForward == 0)
        {
            transform.Rotate(Vector3.forward, -speed * Time.deltaTime);
        }
        else if (rotateForward == 1)
        {
            transform.Rotate(Vector3.forward, speed * Time.deltaTime);
        }
        
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        //4
        //sensor.AddObservation(this.transform.rotation);

        //3
        //sensor.AddObservation(this.transform.InverseTransformPoint(ball.transform.position));
        //3
        //sensor.AddObservation(ballRb.velocity);

        Vector3 ballLocalPos = transform.InverseTransformPoint(ball.position);
        Vector3 ballLocalVel = transform.InverseTransformDirection(ballRb.velocity);

        // Get the position and velocity of the cube in world space
        Vector3 cubeWorldPos = transform.position;
        Vector3 cubeWorldVel = transform.GetComponent<Rigidbody>().velocity;

        // Get the rotation of the cube in local space relative to its starting orientation

        // Get the angular velocity of the cube
        Vector3 cubeAngularVel = transform.GetComponent<Rigidbody>().angularVelocity;

        // Add the observations to the sensor
        sensor.AddObservation(ballLocalPos);
        sensor.AddObservation(ballLocalVel);
        sensor.AddObservation(cubeWorldPos);
        sensor.AddObservation(cubeWorldVel);
        sensor.AddObservation(cubeAngularVel);
    }

    //public override void Heuristic(in ActionBuffers actionsOut)
    //{
        
        
    //}

    private void tmp(in ActionBuffers actionsOut){
        var discreteActionsOut = actionsOut.DiscreteActions;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            
            discreteActionsOut[0] = 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            discreteActionsOut[0] = 2;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            discreteActionsOut[1] = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            discreteActionsOut[1] = 2;
        }
    }
}