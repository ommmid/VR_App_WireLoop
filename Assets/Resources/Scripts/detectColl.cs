using UnityEngine;
using System.Collections.Generic;
using Leap;
using Leap.Unity;
using Leap.Unity.Attributes;
using Leap.Unity.Interaction;
using System;

/* If you are trying to do some calculations that should correlate with the Unity hand models, you should get 
 * the data from either the HandModel object itself, or from LeapProvider.CurrentFrame this data is already transformed from the
 * Leap Motion coordinate system to the Unity coordinate system relative to the Unity LeapHandController object. 
 * You don't need to use ToUnity() or ToUnityScaled() on the Vectors in this case, since the transformation has 
 * already been done -- just use ToVector3() to create a Unity Vector3 object when necessary. For matrices that contain rotations,
 * you can use the Matrix.Rotation() extension to get a Unity Quaternion object. So for the hand rotation,
 * something like hand.Basis.Rotation() should work. 
 * Also, our Unity HandModel class, which all the asset hands use provide functions
 * like GetPalmPosition() and GetPalmRotation() which give you that hands attributes as Unity Vector3 and Quaternion objects. */

public class detectColl : MonoBehaviour
{
    LeapProvider provider;
    bool collided = false;
    GameObject ringParent;

    void Start()
    {
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;

        // the ring is not placed in (0,0,0) of the mesh. So I make the ring to be the child of another object and put work with parent instead
        ringParent = transform.parent.gameObject;
    }

    void Update()
    {
        Frame frame = provider.CurrentFrame;

        foreach (Hand hand in frame.Hands)
        {
             if (collided == true)
               {
                // the ring is not in the origin (pivot) of the mesh. So I have to translate it so it can be on the palm position. but the
                // point is that I can not use vectors like "new Vector3(1f,1f,1f)" because this is a constant vector in the world frame
                // while I need a vector on the hand frame which changes with the changes of the hand (the leap is mounted so there is 
                // another transformation there). Therefor, I should use the vectors in the hand frame like Direction or PalmNormal 
                // which move according to the hand and head moves
                Vector3 vecDir = hand.Direction.ToVector3()*(-0.03f);
                Vector3 vecNor = hand.PalmNormal.ToVector3()*(0.05f);
                Vector3 vecPla = CrossProduct(vecNor, vecDir) *(50.0f); // the vector lying on the palm plane
                transform.position = hand.PalmPosition.ToVector3() + vecDir + vecNor + vecPla;
                //ringParent.transform.position = hand.PalmPosition.ToVector3() + hand.PalmNormal.ToVector3() * (transform.localScale.y*0.05f);
                //transform.position = UnityMatrixExtension.GetLeapMatrix(provider.transform).TransformPoint(hand.PalmPosition).ToVector3();

                // transform.localPosition = hand.PalmPosition.ToVector3()+ new Vector3(0.001f, 0.001f, 0.001f);
                //transform.position = (hand.PalmPosition + new Vector(-0.1f, -0.1f, -0.1f)).ToVector3();

                transform.rotation = hand.Basis.CalculateRotation();
                //transform.rotation = hand.Rotation.ToQuaternion();
            }
        }
    }

 /*   void OnTriggerEnter(Collider other) 
        // this function should be attached to the object that the hand is going to touch. The 
        // hand is the collider. Actually colliders are the object that make the collision happen
 {
    } */

     void OnCollisionEnter(Collision other) 
        // this function should be attached to the object that the hand is going to touch. Collision
        // gives the physics information like point of impact, its velocity ,...
     {
        Debug.Log("The hand collided the Ring. The ring is going to be attahed to the hand");

        if (other.collider.gameObject.name == "BrushHand_R" || other.collider.gameObject.name == "BrushHand_L")
        {
            collided = true;
            //Debug.Log("-------->>>>>>" + other.collider.GetComponent<HandModel>().GetPalmPosition());
            Debug.Log("----->" + other.collider.gameObject.name);
        }

        /*if (other.collider.transform.parent && other.collider.transform.parent.parent && other.collider.transform.parent.parent.GetComponent<HandModel>())
        {
            collided = true;
            //Debug.Log("-------->>>>>>" + other.collider.GetComponent<HandModel>().GetPalmPosition());
            Debug.Log("yayyyyyy");
        }*/

    }

    Vector3 CrossProduct(Vector3 v1, Vector3 v2)
    {
        float x, y, z; 
        x = v1.y * v2.z - v2.y * v1.z;
        y = (v1.x * v2.z - v2.x * v1.z) * -1;
        z = v1.x * v2.y - v2.x * v1.y;

        return new Vector3(x, y, z);
    }
}
