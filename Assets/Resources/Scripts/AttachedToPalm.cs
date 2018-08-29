using UnityEngine;
using System.Collections.Generic;
using Leap;
using Leap.Unity;

public class AttachedToPalm : MonoBehaviour
{
    LeapProvider provider;
    Vector3 vec = new Vector3(0,0,0);

    void Start()
    {
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;
    }

    void Update()
    {
        Frame frame = provider.CurrentFrame;

        foreach (Hand hand in frame.Hands)
        {
                transform.position = hand.PalmPosition.ToVector3()-vec;
                transform.rotation = hand.Basis.CalculateRotation();
        }
    }
}