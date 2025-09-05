using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Test : MonoBehaviour
{
    [SerializeField] private float distance = 1f;

    private void Start()
    {
        
        
    }

    private void Update()
    {
        XRGrabInteractable[] xrGrabInteractable = FindObjectsOfType<XRGrabInteractable>();
        foreach (XRGrabInteractable xrGrab in xrGrabInteractable)
        {
            if (Vector3.Distance(xrGrab.transform.position, transform.position) < distance)
            {
                xrGrab.enabled = false;
            }
            if (Vector3.Distance(xrGrab.transform.position, transform.position) > distance)
            {
                xrGrab.enabled = true;Debug.Log(xrGrab.gameObject.name);
            }
        }
    }
}
