using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get vertical input
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement
        Vector3 movement = Vector3.up * verticalInput * speed * Time.deltaTime;

        // Apply movement
        transform.position += movement;
    }
}
