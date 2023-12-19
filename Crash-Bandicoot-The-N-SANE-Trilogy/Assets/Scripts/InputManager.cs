using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
  //References
  PlayerMovement _playerMovement;
  MainMenu _mainMenu;

  //Controller
   PlayerController controls;
   [HideInInspector] public PlayerController.PlayerActionActions playerAction;

  Vector2 movementInput;
  Vector2 mouseInput;

  void Awake()
  {
    //References
    _playerMovement = GetComponent<PlayerMovement>();
    _mainMenu = GameObject.Find("Canvas").GetComponent<MainMenu>();

    controls = new PlayerController();
    playerAction = controls.PlayerAction;

    playerAction.MovementInput.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
    playerAction.MovementInput.started += ctx => _playerMovement.isWalking = true;
    playerAction.MovementInput.canceled += ctx => _playerMovement.isWalking = false;

    playerAction.Jump.performed += _ => _playerMovement.OnJumpPressed();

    playerAction.Pause.performed += _ => _mainMenu.MenuButton();
  }

  void Update()
  {
    _playerMovement.ReceiveInput(movementInput);
  }

  void OnEnable()
  {
    controls.Enable();
  }

  void OnDestroy()
  {
    controls.Disable();
  }
}