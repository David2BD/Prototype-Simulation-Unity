using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Ball : MonoBehaviour
{
    public Vector3 gravitation = new Vector3(0f, -9.81f, 0f);
    public Vector3 acceleration = new Vector3(0f,0f,0f);
    public Vector3 randomSpeed = new Vector3(0f,0f,0f);
    public Vector3 randomForce = new Vector3(0f,0f,0f);
    public float masse = 1f;
    public GameLoop gameLoopScript;
    
    // Start is called before the first frame update
    void Start()
    {
        gameLoopScript = GameObject.FindObjectOfType(typeof(GameLoop)) as GameLoop;
        
        // genere une force aleatoire
        randomForce = new Vector3(UnityEngine.Random.Range(-5f, 5f),
            UnityEngine.Random.Range(-5f, 5f),
            UnityEngine.Random.Range(-5f, 5f));
        
        // genere une velocite initiale
        // semi aleatoire pour aider a avoir plus de collisions
        if (transform.position.x > 0)
        {
            randomSpeed = new Vector3(UnityEngine.Random.Range(-10f, 2f),
                UnityEngine.Random.Range(-10f, 5f),
                UnityEngine.Random.Range(-10f, 10f));
        }
        else
        {
            randomSpeed = new Vector3(UnityEngine.Random.Range(-2f, 10f),
                UnityEngine.Random.Range(-10f, 5f),
                UnityEngine.Random.Range(-10f, 10f));
        }
        
        
        acceleration += (randomForce + gravitation) / masse;
    }
    // Update is called once per frame
    void Update()
    {
        masse = UnityEngine.Random.Range(1f, 5f);
        transform.position += randomSpeed * Time.deltaTime + (0.5f * acceleration * (Time.deltaTime*Time.deltaTime));
        randomSpeed += acceleration * Time.deltaTime;
        acceleration = (randomForce + gravitation) / masse;
        
        
        if (transform.position.x < -20 || transform.position.y < -20 || transform.position.z < -20 ||
            transform.position.x > 20 || transform.position.y > 20 || transform.position.z > 20)
        {
            Destroy(gameObject);
        }
        
    }

}
