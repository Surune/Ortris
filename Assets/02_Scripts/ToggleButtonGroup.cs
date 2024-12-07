using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButtonGroup : MonoBehaviour {
    private ToggleGroup toggleGroup;

    private void Awake() {
        // Get the ToggleGroup component
        toggleGroup = GetComponent<ToggleGroup>();

        // Add event listeners for each toggle button
        Toggle[] toggleButtons = GetComponentsInChildren<Toggle>();
        foreach (Toggle toggle in toggleButtons)
        {
            toggle.onValueChanged.AddListener((value) =>
            {
                if (value)
                {
                    // Handle the toggle button click
                    OnToggleButtonClicked(toggle);
                }
            });
        }
    }

    private void OnToggleButtonClicked(Toggle clickedToggle) {
        // Deselect the clicked toggle button if it is already selected
        if (clickedToggle.isOn)
        {
            toggleGroup.SetAllTogglesOff();
        }
    }
}
