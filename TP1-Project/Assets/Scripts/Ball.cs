using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector3 acceleration = new Vector3(1.005f, -9.81f, 0f);
    public Vector3 movementSpeed = new Vector3(0f,0f,0f);
    public GameLoop gameLoopScript;
    
    // Start is called before the first frame update
    void Start()
    {
        gameLoopScript = GameObject.FindObjectOfType(typeof(GameLoop)) as GameLoop;
    }
    // Update is called once per frame
    void Update()
    {

        transform.position += movementSpeed * Time.deltaTime + (0.5f * acceleration * (Time.deltaTime*Time.deltaTime));
        movementSpeed += acceleration * Time.deltaTime;

        
        if (transform.position.x < -20 || transform.position.y < -20 || transform.position.z < -20 ||
            transform.position.x > 20 || transform.position.y > 20 || transform.position.z > 20)
        {
            Destroy(gameObject);
        }
        
    }
}
