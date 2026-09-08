using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAssign : MonoBehaviour
{
    [Header("配置されたプレイヤー")]
    [SerializeField] private PlayerInput firstInput;
    [SerializeField] private PlayerInput secondInput;

    private void Start()
    {
        var gamepads = Gamepad.all;

        firstInput.neverAutoSwitchControlSchemes = true;
        secondInput.neverAutoSwitchControlSchemes = true;

        if (gamepads.Count > 0)
        {
            firstInput.SwitchCurrentControlScheme("Gamepad", gamepads[0]);
        }
        else
        {
            Debug.LogWarning("1Pのコントローラーが接続されていません");
        }

        if (gamepads.Count > 1)
        {
            secondInput.SwitchCurrentControlScheme("Gamepad", gamepads[1]);
        }
        else
        {
            Debug.LogWarning("2Pのコントローラーが接続されていません");
        }
    }
}
