using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BouncingLogo : MonoBehaviour
{
    private Rigidbody _rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();

        var randomVector = Random.insideUnitCircle * 500;
        _rigidBody.AddForce(randomVector, ForceMode.Acceleration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
