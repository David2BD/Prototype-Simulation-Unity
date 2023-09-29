using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Ball : MonoBehaviour
{
    public Vector3 movementSpeed = new Vector3(0f,0f,0f);
    public Vector3 gravitation = new Vector3(0f, -9.81f, 0f);
    public Vector3 randomForce = new Vector3(0f,0f,0f);
    public int masse = 1;
    public GameLoop gameLoopScript;
    
    // Start is called before the first frame update
    void Start()
    {
        gameLoopScript = GameObject.FindObjectOfType(typeof(GameLoop)) as GameLoop;
        
        // genere une force initiale
        randomForce = new Vector3(UnityEngine.Random.Range(-6f, 6f),
        UnityEngine.Random.Range(-6f, 6f),
        UnityEngine.Random.Range(-6f, 6f));
    }
    // Update is called once per frame
    void Update()
    {
        /*
        // gravite est opposee
        if (movementSpeed.y > 0)
        {
            transform.position += movementSpeed * Time.deltaTime + (0.5f * -gravitation * (Time.deltaTime*Time.deltaTime));
            movementSpeed += gravitation * Time.deltaTime;
        }
        // gravite est avec
        else
        {
            transform.position += movementSpeed * Time.deltaTime + (0.5f * gravitation * (Time.deltaTime*Time.deltaTime));
            movementSpeed += gravitation * Time.deltaTime;
        }
        */
        
        transform.position += movementSpeed * Time.deltaTime + (0.5f * randomForce * (Time.deltaTime*Time.deltaTime));
        movementSpeed += randomForce * Time.deltaTime;
        
        if (transform.position.x < -20 || transform.position.y < -20 || transform.position.z < -20 ||
            transform.position.x > 20 || transform.position.y > 20 || transform.position.z > 20)
        {
            Destroy(gameObject);
        }
        
    }

}
