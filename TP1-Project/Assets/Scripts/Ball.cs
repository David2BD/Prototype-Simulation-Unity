using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Ball : MonoBehaviour
{
    public Vector3 gravitation = new Vector3(0f, -9.81f, 0f);
    public Vector3 acceleration = new Vector3(0f,0f,0f);
    public Vector3 speed = new Vector3(0f,0f,0f);
    public Vector3 force = new Vector3(0f,0f,0f);
    public float masse = 1f;
    private float radius = 1f;
    //public GameLoop gameLoopScript;
    
    // Start is called before the first frame update
    void Start()
    {
        
        // genere une force aleatoire
        if (transform.position.x > 0)
        {
            force = new Vector3(UnityEngine.Random.Range(-30f, -20f),
                UnityEngine.Random.Range(-25f, 25f),
                UnityEngine.Random.Range(-25f, 25f));
        }
        else
        {
            force = new Vector3(UnityEngine.Random.Range(20f, 30f),
                UnityEngine.Random.Range(-15f, 15f),
                UnityEngine.Random.Range(-15f, 15f));
        }
        
        acceleration += (force + gravitation) / masse;
    }
    // Update is called once per frame
    void Update()
    {
        //masse = UnityEngine.Random.Range(1f, 5f);
        
        transform.position += speed * Time.deltaTime + (0.5f * acceleration * (Time.deltaTime*Time.deltaTime));
        speed += acceleration * Time.deltaTime;
        acceleration = (force + gravitation) / masse;
        
        if (transform.position.x < -20 || transform.position.y < -20 || transform.position.z < -20 ||
            transform.position.x > 20 || transform.position.y > 20 || transform.position.z > 20)
        {
            Destroy(gameObject);
        }
        collideWithWall();
    }

    void collideWithWall()
    {
        if (
                transform.position.x - radius <= 1 && transform.position.x + radius >= 0
                && transform.position.y - radius <= 10 && transform.position.y + radius >= 0
                && transform.position.z - radius <= 10 && transform.position.z + radius >= 0
           )
        {
            force = new Vector3(0, 0, 0);
            speed.x = 0;
            acceleration = gravitation / masse;
        }
        
    }
}
