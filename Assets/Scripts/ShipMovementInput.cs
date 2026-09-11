using UnityEngine;

public class ShipMovementInput : MonoBehaviour
{

    [SerializeField] ShipInputManager.InputType inputType = ShipInputManager.InputType.HumanDesktop;

    public IMovementControls movementControls {  get; private set; }


   
    void Start()
    {
        movementControls = ShipInputManager.GetInputcontrols(inputType);
    }

    private void OnDestroy()
    {
        movementControls = null;
    }
}
