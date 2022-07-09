using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private List<Color> healthIndicatorColors;
    
    private int _maxHealth;
    private int _currentHealth;
    private bool _jumpKeyPressed;
    private float _horizontalInput;
    private Rigidbody _playerRigidBody;
    private Renderer _renderer;
    private int _currentColorIndicator;
    
    // Start is called before the first frame update
    void Start()
    {
        _maxHealth = healthIndicatorColors.Count;
        _currentHealth = _maxHealth;
        _playerRigidBody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpKeyPressed = true;
        }

        _horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        _playerRigidBody.velocity = new Vector3(_horizontalInput * 7, _playerRigidBody.velocity.y, 0);
       
        const int maxColliders = 10;
        var result = new Collider[maxColliders];
        if (Physics.OverlapSphereNonAlloc(groundCheckTransform.position, 0.1f, result, playerMask) == 0)
        {
            return;
        }
        
        if (_jumpKeyPressed)
        {
            _playerRigidBody.AddForce(Vector3.up * 10, ForceMode.VelocityChange);
            _jumpKeyPressed = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 7)
        {
            return;
        }

        LowerHealth();
    }

    private void LowerHealth()
    {
        _currentHealth--;

        var currentColorIndex = _maxHealth - _currentHealth;
        if (currentColorIndex < 0)
        {
            return;
        }
        
        if (_currentHealth <= 0)
        {
            Debug.Log("GameOver");
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
            return;
        }
        
        _renderer.material.SetColor("_Color", healthIndicatorColors[currentColorIndex]);
    }
}
