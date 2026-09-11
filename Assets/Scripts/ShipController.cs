using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] ShipMovementInput _movementinput;
    [SerializeField][Range(1000f, 10000f)]
    float _thrustForce = 7500f,
         _pitchForce = 6000f,
         _rollForce = 1000f,
         _yawForce = 2000f;


    Rigidbody _rigidbody;
    [Range(-1f , 1f)]
    [SerializeField]
    float _thrustAmount, _pitchAmount, _rollAmount, _yawAmount = 0f;

    IMovementControls ControlInput => _movementinput.movementControls;


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _thrustAmount = ControlInput.thrustAmount;
        _pitchAmount = ControlInput.pitchAmount;
        _rollAmount = ControlInput.rollAmount;
        _yawAmount = ControlInput.yawAmount;
    }


    private void FixedUpdate()
    {
        if (!Mathf.Approximately(0f, _pitchAmount))
        {
            _rigidbody.AddTorque(transform.right * (_pitchForce * _pitchAmount * Time.fixedDeltaTime));
        }

      if (!Mathf.Approximately(0f, _rollAmount))
        {  
            _rigidbody.AddTorque(transform.forward * (_rollForce * _rollAmount * Time.fixedDeltaTime));
        }


      
      if (!Mathf.Approximately(0f, _yawAmount))
        {  
            _rigidbody.AddTorque(transform.up * (_yawAmount * _yawForce * Time.fixedDeltaTime));
        }



        if (!Mathf.Approximately(0f, _thrustAmount))
        {
            _rigidbody.AddForce(transform.forward * (_thrustForce * _thrustAmount * Time.fixedDeltaTime));
        }


    }
}

