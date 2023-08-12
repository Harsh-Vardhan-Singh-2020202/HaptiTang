using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [HideInInspector]
    public AudioSource audioSrc;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public InteractPC interactor;

    protected Renderer[] objectRenderer;
    protected Material[] normalMaterial;

    public GameObject[] glowObjects;
    public GameObject Info;
    public Material glowMaterial;

    protected virtual void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        interactor = GetComponent<InteractPC>();

        objectRenderer = new Renderer[glowObjects.Length];
        normalMaterial = new Material[glowObjects.Length];

        for (int i = 0; i < glowObjects.Length; i++)
        {
            objectRenderer[i] = glowObjects[i].GetComponent<Renderer>();
            normalMaterial[i] = objectRenderer[i].material;
        }

        if (Info != null)
            Info.SetActive(false);
    }

    protected virtual void Update()
    {
        if (interactor != null)
        {
            if (interactor.PC_CAMERA != null)
            {
                if (interactor.pointing)
                    ChangeColourOnEnter();
                else if (!interactor.pointing)
                    ChangeColourOnExit();

                if (interactor.interacted == true)
                    Interact();
            }
        }
    }

    public void ChangeColourOnEnter()
    {
        if (Info != null)
            Info.SetActive(true);

        if (glowObjects != null)
            for (int i = 0; i < glowObjects.Length; i++)
                objectRenderer[i].material = glowMaterial;
    }

    public void ChangeColourOnExit()
    {
        if (Info != null)
            Info.SetActive(false);

        if (objectRenderer != null)
            for (int i = 0; i < glowObjects.Length; i++)
                objectRenderer[i].material = normalMaterial[i];
    }

    public virtual void Interact()
    {
        // To be coded in child class
        // This is usually used for PC testing as interaction in most cases in VR are managed by XR 
    }
}