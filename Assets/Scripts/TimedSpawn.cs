using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TimedSpawn : MonoBehaviour
{
    [SerializeField] private bool stopSpawning;
    [SerializeField] private float spawnTime;
    [SerializeField] private float spawnDelay;
    [SerializeField] private List<Transform> spawnTransforms;
   
    private int _currentVectorLength;
    private int _spawnCounter;
    private BouncingBallsManager _ballsManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _ballsManager = GetComponent<BouncingBallsManager>();
        _currentVectorLength = 700;
        _spawnCounter = 0;
        InvokeRepeating(nameof(SpawnObject), spawnTime, spawnDelay);
    }

    private void SpawnObject()
    {
        if (stopSpawning)
        {
            CancelInvoke(nameof(SpawnObject));
        }
        
        var spawnTransform = spawnTransforms[Random.Range(0, spawnTransforms.Count)];
        var spawned = _ballsManager.TrySpawnBall(spawnTransform, _currentVectorLength);
        
        if (!spawned)
        {
            return;
        }
        
        _spawnCounter++;

        if (_spawnCounter % 5 == 0)
        {
            _ballsManager.currentLifespan += 1;
            _currentVectorLength += 100;
        }
    }
}
