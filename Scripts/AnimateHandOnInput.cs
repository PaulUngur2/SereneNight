using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    public InputActionProperty pinchInputAction; // Reference to pinch input action from Input System
    public InputActionProperty gripInputAction; // Reference to grip input action from Input System
    
    public Animator handAnimator; // Reference to the Animator component of the hand model
    
    void Update()
    {
        // Read pinch input value from Input System
        float triggerValue = pinchInputAction.action.ReadValue<float>();
        
        // Set "Trigger" parameter in Animator controller based on pinch input value
        handAnimator.SetFloat("Trigger", triggerValue);
        
        // Read grip input value from Input System
        float gripValue = gripInputAction.action.ReadValue<float>();
        
        // Set "Grip" parameter in Animator controller based on grip input value
        handAnimator.SetFloat("Grip", gripValue);
    }
}