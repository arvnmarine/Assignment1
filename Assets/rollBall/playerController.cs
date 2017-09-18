using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour {

    public float spd;
    public Text countText;
    private Rigidbody rbody;
    private int count;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody>();
        count = 0;
        countText.text = "Score: " + count.ToString();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX,0.0f,moveY);

        rbody.AddForce(move*spd);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coin")) {
            other.gameObject.SetActive(false);
            count++;
            countText.text = "Score: " + count.ToString();
        }
    }
}
