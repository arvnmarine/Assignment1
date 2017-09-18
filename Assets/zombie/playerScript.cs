using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour
{

    
    private GameObject gun;
    private GameObject spawnPoint;
    private bool isShooting;

    
    void Start()
    {

        

        
        gun = gameObject.transform.GetChild(0).gameObject;
        spawnPoint = gun.transform.GetChild(0).gameObject;

        
        isShooting = false;
    }

    
    IEnumerator Shoot()
    {
        //set is shooting to true so we can't shoot continuosly
        isShooting = true;
        //instantiate the bullet
        GameObject bullet = Instantiate(Resources.Load("bullet", typeof(GameObject))) as GameObject;
        //Get the bullet's rigid body component and set its position and rotation equal to that of the spawnPoint
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        bullet.transform.rotation = spawnPoint.transform.rotation;
        bullet.transform.position = spawnPoint.transform.position;

        //add force to the bullet in the direction of the spawnPoint's forward vector
        rb.AddForce(spawnPoint.transform.forward * 500f);
        //play the gun shot sound and gun animation
        GetComponent<AudioSource>().Play();
        gun.GetComponent<Animation>().Play();
        //destroy the bullet after 1 second
        Destroy(bullet, 1);
        //wait for 1 second and set isShooting to false so we can shoot again
        yield return new WaitForSeconds(1f);
        isShooting = false;
    }

    
    void Update()
    {

        //Draw a line (named "Hit") come out from muzzle of the gun, if the line hit the zombie, bullet will be shot and collide with zombie.

        RaycastHit hit;
        Debug.DrawRay(spawnPoint.transform.position, spawnPoint.transform.forward, Color.green);

        
        if (Physics.Raycast(spawnPoint.transform.position, spawnPoint.transform.forward, out hit, 500))
        {

            
            if (hit.collider.name.Contains("zombie"))
            {
                if (!isShooting)
                {
                    StartCoroutine("Shoot");
                }

            }

        }

    }
}