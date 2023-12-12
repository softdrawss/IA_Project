using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingManager : MonoBehaviour
{
    Flocking flock;
    public GameObject fishprefab;
    public int numFish = 50;
    public GameObject[] allFish;
    public Vector3 swimLimits;
    public bool bounded = true;
    public bool randomize = true;
    public bool followLeader = true;
    public Bounds bound;

    [Header("Fish Settings")]
    public float minSpeed = 5;
    public float maxSpeed = 10;
    public float neighbourDistance;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        allFish = new GameObject[numFish];
        bound = new Bounds(fishprefab.transform.position, swimLimits);

        for (int i = 0; i < numFish; ++i)
        {

            Vector3 pos = this.transform.position + Random.insideUnitSphere * 10;
            Vector3 randomize = Random.insideUnitSphere;

            allFish[i] = (GameObject)Instantiate(fishprefab, pos,
                              Quaternion.LookRotation(randomize));
            allFish[i].GetComponent<Flocking>().myManager = this;
        }
    }


}
