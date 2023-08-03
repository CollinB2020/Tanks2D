using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerInputController : MonoBehaviour
{

    //Component on player with a reference to the playerInputActions
    private PlayerInput playerInput;

    //Action for the tap triggerred button input actions
    private InputAction tap0Action;
    private InputAction tap1Action;

    private Vector2 joystickDirection;

    //Reference to tank controller
    public TankController tankController;

    void Awake()
    {
        EnhancedTouchSupport.Enable();

        playerInput = this.GetComponent<PlayerInput>();
        tap0Action = playerInput.actions["Touch0Tap"];
        tap1Action = playerInput.actions["Touch1Tap"];

        joystickDirection = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {

        //Update with the current joystick input
        joystickDirection = playerInput.actions["JoystickMovement"].ReadValue<Vector2>();

        if (joystickDirection != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(joystickDirection.y, joystickDirection.x) * 180 / Mathf.PI; //-179 through 180

            //Set the direction the tracks are pointing
            tankController.SetTargetTrackRotation(targetRotation);

            //Try moving the tank
            bool success = tankController.TryMove(new Vector2(Mathf.Cos(tankController.GetTankRotation() * Mathf.PI / 180), Mathf.Sin(tankController.GetTankRotation() * Mathf.PI / 180)));

            if (!success)
            {
                success = tankController.TryMove(new Vector2(joystickDirection.x, 0));
            }

            if (!success)
            {
                success = tankController.TryMove(new Vector2(0, joystickDirection.y));
            }
        }
        else tankController.StopRotation(); //Stop rotating even if the tank hasnt reached the direction that the joystick was previously pointed
    }


    //Triggers when touch 0 is detected as a tap
    private void Tap0Action(InputAction.CallbackContext context)
    {
        Vector2 tapLocationScreen = playerInput.actions["Touch0"].ReadValue<Vector2>();
        TapAction(tapLocationScreen);
    }

    //Triggers when touch 1 is detected as a tap
    private void Tap1Action(InputAction.CallbackContext context)
    {
        Vector2 tapLocationScreen = playerInput.actions["Touch1"].ReadValue<Vector2>();
        TapAction(tapLocationScreen);
    }

    //Called by the tap triggers and is given the tap location on screen
    private void TapAction(Vector2 tapLocationScreen)
    {
        Vector3 tapLocationWorld = Camera.main.ScreenToWorldPoint(new Vector3(tapLocationScreen.x, tapLocationScreen.y, 0));

        //Convert the world space tap location to a direction from the player in degrees and send to the tank controller
        Vector3 dir = tapLocationWorld - tankController.GetTankPosition();
        float angle = Mathf.Atan2(dir.y, dir.x) * 180 / Mathf.PI;
        tankController.SetTowerRotation(angle);
        tankController.ShootGun();
    }

    private void OnEnable()
    {
        tap0Action.performed += Tap0Action;
        tap1Action.performed += Tap1Action;
    }

    private void OnDisable()
    {
        tap0Action.performed -= Tap0Action;
        tap1Action.performed -= Tap1Action;
    }
}
