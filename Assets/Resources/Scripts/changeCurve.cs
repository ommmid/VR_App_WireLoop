using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeCurve : MonoBehaviour
{
    private int numCollision = 0;
    void Start()
    {
        
    }

    void Update()
    {
     //   this.transform.position = new Vector3(0.0f, 0.0F, 0.0F);
      //  this.transform.rotation = Quaternion.identity;
    }

    void OnCollisionEnter(Collision col)
    {

        Debug.Log("An object collided the curve: " + col.collider.name);

        if (col.collider.tag == "Bar" ) // tags: Bar/Ring
        {
            col.collider.GetComponent<Renderer>().material.color = Color.red;
            numCollision++;
            Debug.Log("collison number --------> " + numCollision);
        }
    }

    void OnCollisionExit(Collision col)
    {
      //  Debug.Log("the collider name is: " + col.collider.name);

        if (col.collider.tag == "Bar") // the tag can be Bar or Ring based on our purpose
        {
            col.collider.GetComponent<Renderer>().material.color = Color.blue;
        }
    }
}
