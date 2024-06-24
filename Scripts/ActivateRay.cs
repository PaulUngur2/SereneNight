using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateRay : MonoBehaviour
{
    public GameObject leftTeleporterRay; // Reference to the left teleporter ray game object
    public GameObject rightTeleporterRay; // Reference to the right teleporter ray game object
    
    public InputActionProperty leftTeleport; // Reference to the left teleport action from Input System
    public InputActionProperty rightTeleport; // Reference to the right teleport action from Input System

    void Update()
    {
        // Check if the left teleport action value is greater than 0.1
        // If true, activate the leftTeleporterRay; otherwise, deactivate it.
        leftTeleporterRay.SetActive(leftTeleport.action.ReadValue<float>() > 0.1f);

        // Check if the right teleport action value is greater than 0.1
        // If true, activate the rightTeleporterRay; otherwise, deactivate it.
        rightTeleporterRay.SetActive(rightTeleport.action.ReadValue<float>() > 0.1f);
    }
}