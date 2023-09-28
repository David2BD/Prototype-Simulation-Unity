using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector3 acceleration = new Vector3(1.005f, -9.81f, 0f);
    public Vector3 movementSpeed = new Vector3(0f,0f,0f);
    private bool _moving = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_moving == false)
            {
                _moving = true;
            }
            else
            {
                _moving = false;
            }
        }
        if (_moving == true)
        {
            transform.position += movementSpeed * Time.deltaTime + (0.5f * acceleration * (Time.deltaTime*Time.deltaTime));
            movementSpeed += acceleration * Time.deltaTime;
        }

        if (transform.position.x > 30 || transform.position.y > 30 || transform.position.z > 30)
        {
            Destroy(gameObject);
        }
    }
}
