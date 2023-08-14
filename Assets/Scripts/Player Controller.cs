using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction _movement;
    [SerializeField] float _controlSpeed = 15f;
    [SerializeField] float _xRange = 5f;
    [SerializeField] float _yRange = 5f;
    [SerializeField] float _positionPitchFactor = -4f;
    [SerializeField] float _controlPitchFactor = -2.5f;
    [SerializeField] float _positionYawFactor = -1f;
    [SerializeField] float _controlRollFactor = -2.5f;

    float xThrow;
    float yThrow;

    void Update()
    {
        
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessTranslation()
    {
        //Old System
        //float horizontalThrow = Input.GetAxis("Horizontal");

        //float verticalThrow = Input.GetAxis("Vertical");

        //New System
        
        xThrow = _movement.ReadValue<Vector2>().x;
        yThrow = _movement.ReadValue<Vector2>().y;

        float xOffset = xThrow * Time.deltaTime * _controlSpeed;
        float yOffset = yThrow * Time.deltaTime * _controlSpeed;

        float xPos = transform.localPosition.x + xOffset;
        float yPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(xPos, -_xRange, _xRange);
        float clampedYPos = Mathf.Clamp(yPos, -_yRange, _yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * _positionPitchFactor;
        float pitchDueToControlY = yThrow * _controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlY;

        float yawDueToPosition = transform.localPosition.x * _positionYawFactor;
        float yaw = yawDueToPosition;

        float rollDueToControl = xThrow * _controlRollFactor;
        float roll = rollDueToControl;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void OnEnable()
    {
        _movement.Enable();
    }

    private void OnDisable()
    {
        _movement.Disable();
    }
}
