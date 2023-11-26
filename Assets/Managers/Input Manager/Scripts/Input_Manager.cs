using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Manager : MonoBehaviour
{
    public static Input_Manager _INPUT_MANAGER;

    private GameGeneral gameGeneral;

    private BoatInputActions inputActions;

    private Vector2 steerValue;
    private Vector2 cameraValue;
    private Vector2 moveValue;

    private bool accelerateValue = false;
    private bool brakeValue = false;
    private bool pauseValue = false;
    private bool actionChangeValue = false;
    private void Awake()
    {
        //Check if it can be created this singleton
        if (_INPUT_MANAGER != null && _INPUT_MANAGER != this)
        {
            Destroy(gameObject);
        }
        else
        {
            //Create this singleton class
            _INPUT_MANAGER = this;
            DontDestroyOnLoad(gameObject);

            inputActions = new BoatInputActions();

            inputActions.BoatController.Accelerate.started += AccelerateValue;
            inputActions.BoatController.Brake.started += BrakeValue;
            inputActions.BoatController.Accelerate.canceled += AccelerateValue;
            inputActions.BoatController.Brake.canceled += BrakeValue;
            inputActions.BoatController.Steer.performed += SteerValue;
            inputActions.BoatController.Rotate.performed += RotateCameraValue;
            inputActions.BoatController.ActionChange.performed += ActionChangeValue;

            inputActions.Player.Move.performed += LeftAxisValue;
            inputActions.Player.Rotate.performed += RotateCameraValue;
            inputActions.Player.Pause.performed += PauseDone;
            inputActions.Player.ActionChange.performed += ActionChangeValue;
        }
    }

    private void Update()
    {
        InputSystem.Update();
        gameGeneral = Game_Manager._GAME_MANAGER.GetCurrentGeneral();
        switch (gameGeneral) 
        {
            case GameGeneral.PLAYER:
                ChangePlayerInputs();
                break;
            case GameGeneral.BOAT:
                ChangeBoatInputs();
                break;
            case GameGeneral.MENU:
                ChangeMenuInpts();
                break;
        }
    }

    private void ChangeMenuInpts()
    {
        inputActions.MenuMovement.Enable();
        inputActions.Player.Disable();
        inputActions.BoatController.Disable();
    }

    private void ChangePlayerInputs()
    {
        inputActions.MenuMovement.Disable();
        inputActions.Player.Enable();
        inputActions.BoatController.Disable();
    }

    private void ChangeBoatInputs()
    {
        inputActions.BoatController.Enable();
        inputActions.Player.Disable();
        inputActions.MenuMovement.Disable();
    }
    private void AccelerateValue(InputAction.CallbackContext context)
    {
        accelerateValue = !accelerateValue;
    }

    private void BrakeValue(InputAction.CallbackContext context)
    {
        brakeValue = !brakeValue;
    }

    private void SteerValue(InputAction.CallbackContext context)
    {
        steerValue = context.ReadValue<Vector2>();
    }

    private void RotateCameraValue(InputAction.CallbackContext context)
    {
        cameraValue = context.ReadValue<Vector2>();
    }

    private void LeftAxisValue(InputAction.CallbackContext context)
    {
        moveValue = context.ReadValue<Vector2>();
    }

    private void PauseDone(InputAction.CallbackContext context)
    {
        pauseValue = !pauseValue;
    }

    private void ActionChangeValue(InputAction.CallbackContext context)
    {
        actionChangeValue = !actionChangeValue;
    }

    public bool GetAccelerateValue()
    {
        return accelerateValue;
    }

    public bool GetBrakeValue()
    {
        return brakeValue;
    }

    public Vector2 GetSteerValue()
    {
        return steerValue;
    }

    public Vector2 GetCameraRotationValue()
    {
        return cameraValue;
    }

    public Vector2 GetMovement()
    {
        return moveValue;
    }

    public bool GetPauseValue()
    {
        return pauseValue;
    }

    public bool GetActionChangeValue()
    {
        return actionChangeValue;
    }
}
