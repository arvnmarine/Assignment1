using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class zom : MonoBehaviour
{
    
    private Transform target;
    private NavMeshAgent zomb;

   
    void Start()
    {

        zomb = GetComponent<NavMeshAgent>();

        target = Camera.main.transform;//zombie will aim player which is the main camera
        zomb.destination = target.position;
        
        GetComponent<Animation>().Play("walk"); // start walking with animation
    }


    
    void OnTriggerEnter(Collider projectile)
    {
        
        GetComponent<CapsuleCollider>().enabled = false;//turn off zombie's capsule collision so he does not get shot again
        
        Destroy(projectile.gameObject);//clear projectile
        
        zomb.destination = gameObject.transform.position;//send zombie back to his original position to simulate killing him
        
        GetComponent<Animation>().Stop();//change animation
        GetComponent<Animation>().Play("back_fall");
        
        Destroy(gameObject, 6);//delete dead zombie
        
        GameObject newZombie = Instantiate(Resources.Load("zombie", typeof(GameObject))) as GameObject;//create new zombie

       
        //set the zombies position
        newZombie.transform.position = new Vector3(UnityEngine.Random.Range(-12f, 12f), 0.01f, UnityEngine.Random.Range(-13f, 13f));

        // prevent zombie from spawning too close to player
        while (Vector3.Distance(newZombie.transform.position, Camera.main.transform.position) <= 3) {
            newZombie.transform.position = new Vector3(UnityEngine.Random.Range(-12f, 12f), 0.01f, UnityEngine.Random.Range(-13f, 13f));
        }

    }

}