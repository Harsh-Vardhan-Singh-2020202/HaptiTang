using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initial_Instructions : Interactable
{
    private bool open;

    public GameObject Instructions;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        open = true;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (open)
            Instructions.SetActive(true);
        else
            Instructions.SetActive(false);
    }

    override public void Interact()
    {
        if (!open)
            open = true;
        else
            open = false;
    }

    public void close()
    {
        open = false;
    }
}
