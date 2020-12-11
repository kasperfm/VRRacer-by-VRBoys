// AGR2280 2012 - 2015
// Created by Vonsnake

using UnityEngine;
using System.Collections;

/// <summary>
/// Handles ship input and local race data such as lap times.
/// </summary>
public class ShipController : MonoBehaviour {

    // INPUT | Axis
    public float inputSteer;
    public float inputPitch;
    public float inputLeftAirbrake;
    public float inputRightAirbrake;

    // INPUT | Buttons
    public bool btnThruster;

    // INPUT | Sideshifting
    private int previousSSInput;
    private int ssTapAmount;
    private float ssTimer;
    private float ssCooler;

    private bool ssLeft;
    private bool ssLeftPressed;
    private bool ssRight;
    private bool ssRightPressed;

    // Controller
    public GlobalSettings.InputController controller;

    // Bools
    public bool bHasFinishedRace;

    void Update()
    {
       // Use a switch to check whether to get player or AI input
       switch(controller)
       {
            case GlobalSettings.InputController.Player:
                GetPlayerInput();
                break;

            case GlobalSettings.InputController.AI:
               GetAIInput();
               break;
       }
    }

    private void SideshiftInput()
    {
        // Opposite sideshift inputs cancel each other
        if ((previousSSInput == -1 && ssRight) || (previousSSInput == 1 && ssLeft))
        {
            ssTapAmount = 0;
            previousSSInput = 0;
        }

        // Double tap left airbrake
        if (ssLeft)
        {
            previousSSInput = -1;
            if (ssCooler > 0 && ssTapAmount == 1)
            {
                GetComponent<ShipSimulator>().StartSideShift(previousSSInput);
            } else
            {
                ssCooler = 0.2f;
                ssTapAmount++;
            }
        }

        // Double tap right airbrake
        if (ssRight)
        {
            previousSSInput = 1;
            if (ssCooler > 0 && ssTapAmount == 1)
            {
                GetComponent<ShipSimulator>().StartSideShift(previousSSInput);
            } else
            {
                ssCooler = 0.2f;
                ssTapAmount++;
            }
        }

        // Sideshift Cooler
        if (ssCooler > 0)
        {
            ssCooler -= 1 * Time.deltaTime;
        } else
        {
            ssTapAmount = 0;
        }
    }

    private void GetPlayerInput() {
        // Get the input axis
        inputSteer = Input.GetAxis("Horizontal");

        // Reset sideshift inputs
        ssLeft = false;
        ssRight = false;

        // Left airbrake tap
        if ((inputLeftAirbrake != 0 && inputRightAirbrake == 0) && !ssLeftPressed) {
            ssLeft = true;
            ssLeftPressed = true;
            ssRight = false;
        }

        // Right airbrake tap
        if ((inputRightAirbrake != 0 && inputLeftAirbrake == 0) && !ssRightPressed) {
            ssRight = true;
            ssRightPressed = true;
            ssLeft = false;
        }

        // No airbrake taps
        if (inputLeftAirbrake == 0 && inputRightAirbrake == 0) {
            ssLeftPressed = false;
            ssRightPressed = false;
        }

        SideshiftInput();
    }

    private void GetAIInput()
    {

    }

}
