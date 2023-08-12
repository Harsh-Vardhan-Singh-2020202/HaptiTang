using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [HideInInspector] public int num1;
    [HideInInspector] public int num2;

    public Dropdown dropdown1;
    public Dropdown dropdown2;
    public Transform interactables;

    public Transform[] sliders;
    public Transform[] rotors;

    private void Start()
    {
        // Add a listener to the dropdowns to detect value changes
        dropdown1.onValueChanged.AddListener(OnDropdownValueChanged1);
        dropdown2.onValueChanged.AddListener(OnDropdownValueChanged2);

        num1 = 0;
        num2 = 0;
        UpdateInteractables();
    }

    // These methods will be called when the value of the dropdowns change
    private void OnDropdownValueChanged1(int index)
    {
        num1 = index;
        UpdateInteractables();
    }
    private void OnDropdownValueChanged2(int index)
    {
        num2 = index;
        UpdateInteractables();
    }

    // Method to update the interactables based on num1 and num2 values
    private void UpdateInteractables()
    {
        foreach (Transform slider in sliders)
            slider.localPosition = new Vector3(0.0f, slider.localPosition.y, slider.localPosition.z);

        foreach (Transform rotor in rotors)
            rotor.localRotation = Quaternion.Euler(0.0f, 0.0f, 5.0f);

        int i = 0;
        foreach (Transform interactable in interactables)
        {
            if (i == num1-1)
            {
                interactable.gameObject.SetActive(true);
                int j = 0;
                foreach (Transform interactable_child in interactable)
                {
                    if (j == num2-1)
                        interactable_child.gameObject.SetActive(true);
                    else
                        interactable_child.gameObject.SetActive(false);
                    j++;
                }
            }
            else
                interactable.gameObject.SetActive(false);
            i++;
        }
    }
}
