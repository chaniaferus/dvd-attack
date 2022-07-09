using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingBall : MonoBehaviour
{
    public float vectorLength;
    public float lifeTime;
    
    private Rigidbody _rigidBody;
    private float _timePassed;
    private Renderer _renderer;

    public delegate void EventHandler();
    public event EventHandler ObjectDestroyed;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _timePassed = 0;

        var randomVector = Random.insideUnitCircle * vectorLength;
        _rigidBody.AddForce(randomVector, ForceMode.Acceleration);
    }

    // Update is called once per frame
    void Update()
    {
        _timePassed += Time.deltaTime;
        var newColor = Color.Lerp(Color.red, Color.black, _timePassed / lifeTime);
        _renderer.material.SetColor("_Color", newColor);

        if (_timePassed >= lifeTime)
        {
            ObjectDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }
}
