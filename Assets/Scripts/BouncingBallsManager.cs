using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BouncingBallsManager : MonoBehaviour
{

    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private int spawnedObjectsNumber;
    [SerializeField] private int maxSpawnedObjects;
    public int currentLifespan;
    
    // Start is called before the first frame update
    void Start()
    {
        currentLifespan = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool TrySpawnBall(Transform spawnTransform, float vectorLength)
    {
        if (spawnedObjectsNumber >= maxSpawnedObjects)
        {
            return false;
        }
        
        var spawnedObject = Instantiate(objectToSpawn, spawnTransform.position, spawnTransform.rotation);
        var bouncingBall = spawnedObject.GetComponent<BouncingBall>();
        bouncingBall.vectorLength = vectorLength;
        bouncingBall.lifeTime = currentLifespan;
        bouncingBall.ObjectDestroyed += delegate { spawnedObjectsNumber--; };

        spawnedObjectsNumber++;
        return true;
    }
}
