using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Flocking : MonoBehaviour
{
    public FlockingManager myManager;
    float intervalTime;
    float speed;
    Vector3 direction;

    //wander
    //public float radius = 25;
    //public float offset = 10;

    void Start()
    {
        StartCoroutine(NewHeading());
    }


    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation,
                                           Quaternion.LookRotation(direction),
                                           myManager.rotationSpeed * Time.deltaTime);
        transform.Translate(0.0f, 0.0f, Time.deltaTime * speed);
    }

    Vector3 Cohesion()
    {
        Vector3 cohesion = Vector3.zero;
        int num = 0;
        foreach (GameObject go in myManager.allFish)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position,
                                                  transform.position);
                if (distance <= myManager.neighbourDistance)
                {
                    cohesion += go.transform.position;
                    num++;
                }
            }
        }
        if (num > 0) { cohesion = (cohesion / num - transform.position).normalized * speed; }

        return cohesion;
    }

    Vector3 Align()
    {
        Vector3 align = Vector3.zero;
        int num = 0;
        foreach (GameObject go in myManager.allFish)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position,
                                                  transform.position);
                if (distance <= myManager.neighbourDistance)
                {
                    align += go.GetComponent<Flocking>().direction;
                    num++;
                }
            }
        }
        if (num > 0)
        {
            align /= num;
            speed = Mathf.Clamp(align.magnitude, myManager.minSpeed, myManager.maxSpeed);
        }
        return align;
    }

    Vector3 Separation()
    {
        Vector3 rand = UnityEngine.Random.insideUnitCircle * 10;
        Vector3 separation = Vector3.zero;
        foreach (GameObject go in myManager.allFish)
        {
            if (go != this.gameObject)
            {
                float distance = Vector3.Distance(go.transform.position,
                                                  transform.position);
                if (distance <= myManager.neighbourDistance)
                    separation -= (transform.position - go.transform.position) /
                                  (distance * distance);
                separation -= rand;
            }
        }
        return separation;
    }

    //Vector3 wander()
    //{
    //    Vector3 localTarget = UnityEngine.Random.insideUnitCircle * radius;
    //    localTarget += new Vector3(offset, offset, offset);
    //    Vector3 worldTarget = transform.TransformPoint(localTarget);
    //    return worldTarget;

    //}

    IEnumerator NewHeading()
    {
        //while (true)
        //{
        //    foreach (GameObject go in myManager.allFish)
        //    {
        //        intervalTime = Random.Range(0.0f, 1.0f);

        //        direction = wander();
        //        //if (myManager.bounded == true && myManager.bound.Contains(transform.position) == true)
        //        //{
        //        //    direction = leaderdir;
        //        //}
        //        //else
        //        //{
        //        //    direction = (myManager.bound.center - transform.position).normalized;
        //        //}
        //    }
        //    yield return new WaitForSeconds(intervalTime);
        //}


        //if (myManager.followLeader == true)
        //{
        //    while (true)
        //    {
        //        Vector3 leaderdir = (Cohesion() + Align() + Separation()).normalized * speed;
        //        foreach (GameObject go in myManager.allFish)
        //        {
        //            intervalTime = Random.Range(0.0f, 1.0f);
        //            if (myManager.bounded == true && myManager.bound.Contains(transform.position) == true)
        //            {
        //                direction = leaderdir;
        //            }
        //            else
        //            {
        //                direction = (myManager.bound.center - transform.position).normalized;
        //            }
        //        }
        //        yield return new WaitForSeconds(intervalTime);
        //    }
        //}
        //else
        //{
        foreach (GameObject go in myManager.allFish)
        {

            intervalTime = Random.Range(0.0f, 1.0f); // Calculate a new interval for each GameObject
            while (true)
            {

                if (myManager.bounded == true && myManager.bound.Contains(transform.position) == true)
                {
                    direction = (Cohesion() + Align() + Separation()).normalized * speed;
                }
                else
                {
                    direction = (myManager.bound.center - transform.position).normalized;
                }
                yield return new WaitForSeconds(intervalTime);
            }
        }
        //}
    }

    
}

//public class Wander : MonoBehaviour
//{
//    public float radius = 8;
//    public float offset = 10;
//    public NavMeshAgent agent;
//    public int intervalTime = 1;
//    Vector3 target;

//    void Start()
//    {
//        StartCoroutine(NewHeading());
//    }
//    // Update is called once per frame
//    void Update()
//    {
//        agent.destination = target;
//    }

//    Vector3 wander()
//    {
//        Vector3 localTarget = UnityEngine.Random.insideUnitCircle * radius;
//        localTarget += new Vector3(0, 0, offset);
//        Vector3 worldTarget = transform.TransformPoint(localTarget);
//        worldTarget.y = 0f;
//        return worldTarget;

//    }

//    IEnumerator NewHeading()
//    {
//        while (true)
//        {
//            target = wander();
//            yield return new WaitForSeconds(intervalTime);
//        }
//    }
//}