using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public CubeController agent;
    private Vector3 initialPosition;
    private Vector3 agentInitialRotation;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        agentInitialRotation = agent.transform.eulerAngles;
    }

    // Update is called once per frame
    void Update(){
        tmp();
        float ballHeight = transform.position.y;
        float reward = (float)System.Math.Pow(ballHeight * 0.01f, 2.0f);
        agent.AddReward(reward);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("OutOfBounds"))
        {
            agent.AddReward(-2f);
            agent.EndEpisode();
            ResetScence();
        }
    }

    private void ResetScence()
    {
        agent.transform.eulerAngles = agentInitialRotation;
        this.transform.position = initialPosition;
    }

    private void MoveTheBall(){
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput);

        transform.position += direction * 4 * Time.deltaTime;
    }

    private void tmp(){
        int randomNumber = Random.Range(0, 100);
        if (randomNumber < 10){
            float speedX = Random.Range(-1f, 1f) * 6f;
            GetComponent<Rigidbody>().AddForce(new Vector3(speedX, 0, 0));
        }
        else if (randomNumber >=10 && randomNumber < 20){
            float speedZ = Random.Range(-1f, 1f) * 6f;
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, speedZ));
        }

        if (transform.position.y > 0){
            agent.AddReward(0.002f);
        }
    }
}
