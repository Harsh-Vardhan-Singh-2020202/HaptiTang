using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteract : Interactable
{
    public GameObject Explosion;
    public Transform TNT;
    public float interactionCooldown = 2f; // Cooldown time in seconds

    private bool isOnCooldown = false;
    private float cooldownTimer = 0f;

    override public void Interact()
    {
        // Check if the interaction is on cooldown
        if (isOnCooldown)
            return;

        if (animator != null)
            animator.SetTrigger("Pressed");

        // Set the flag to indicate that the interaction is on cooldown
        isOnCooldown = true;

        // Instantiate the explosion at the position and rotation of TNT
        GameObject explosionInstance = Instantiate(Explosion, TNT.position, TNT.rotation);

        // Destroy the explosion after the specified duration
        Destroy(explosionInstance, 2f);

        // Start the cooldown timer
        cooldownTimer = interactionCooldown;
    }

    void Update()
    {
        // Update the cooldown timer if the interaction is on cooldown
        if (isOnCooldown)
        {
            TNT.gameObject.SetActive(false);

            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                TNT.gameObject.SetActive(true);

                // Reset the cooldown flag after the cooldown duration
                isOnCooldown = false;
            }
        }
    }
}