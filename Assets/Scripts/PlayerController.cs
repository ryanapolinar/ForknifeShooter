using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 100f;
    public Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        /*
		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        Vector2 direction = input.normalized;
        Vector2 velocity = direction * speed;
        Vector2 moveAmount = velocity * Time.deltaTime;

        transform.Translate(moveAmount);
        */
        
        //rigidbody
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 tempVect = new Vector2(h, v);
        tempVect = tempVect.normalized * speed * Time.deltaTime;
        transform.Translate(tempVect);
        //rb.MovePosition(rb.position + (tempVect * speed) * Time.deltaTime);
    }
}
