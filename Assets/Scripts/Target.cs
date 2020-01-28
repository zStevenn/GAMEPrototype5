using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{
    private GameManager gameManager;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;
    public int pointValue;
    public ParticleSystem explosionParticle;
    private Rigidbody targetRb;
    // Start is called before the first frame update
    void Start()
    {
        // Get Rigidbody
        targetRb = GetComponent<Rigidbody>();
        // Add random force to rigidbody to launch it into the air
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        // Add random torque to make the object spin
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        // RandomSpawnPos to make the object spawn at random places
        transform.position = RandomSpawnPos();
        // Zoeken naar game manager
        gameManager = FindObjectOfType<GameManager>();
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
        
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
        
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
           Destroy(gameObject);
           Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
           gameManager.UpdateScore(pointValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }
}
