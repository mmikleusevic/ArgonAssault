using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("General Setup Setting")]
    [SerializeField] InputAction _movement;
    [SerializeField] InputAction _fire;
    [Tooltip("Add all player lasers here")][SerializeField] GameObject[] _lasers;
    [Tooltip("How fast player moves up and down")][SerializeField] float _controlSpeed = 15f;
    [Tooltip("How far player can move on x axis")][SerializeField] float _xRange = 5f;
    [Tooltip("How far player can move on y axis")][SerializeField] float _yRange = 5f;

    [Header("Screen position based tuning")]
    [SerializeField] float _positionPitchFactor = -4f;
    [SerializeField] float _controlPitchFactor = -2.5f;

    [Header("Player input based tuning")]
    [SerializeField] float _positionYawFactor = -1f;
    [SerializeField] float _controlRollFactor = -2.5f;

    float _xThrow;
    float _yThrow;

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessTranslation()
    {
        //Old System
        //float horizontalThrow = Input.GetAxis("Horizontal");

        //float verticalThrow = Input.GetAxis("Vertical");

        //New System

        _xThrow = _movement.ReadValue<Vector2>().x;
        _yThrow = _movement.ReadValue<Vector2>().y;

        float xOffset = _xThrow * Time.deltaTime * _controlSpeed;
        float yOffset = _yThrow * Time.deltaTime * _controlSpeed;

        float xPos = transform.localPosition.x + xOffset;
        float yPos = transform.localPosition.y + yOffset;

        float clampedXPos = Mathf.Clamp(xPos, -_xRange, _xRange);
        float clampedYPos = Mathf.Clamp(yPos, -_yRange, _yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * _positionPitchFactor;
        float pitchDueToControlY = _yThrow * _controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlY;

        float yawDueToPosition = transform.localPosition.x * _positionYawFactor;
        float yaw = yawDueToPosition;

        float rollDueToControl = _xThrow * _controlRollFactor;
        float roll = rollDueToControl;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring()
    {
        //Old system
        //if (Input.GetButton("Fire1"))
        //{
        //    Debug.Log("Shooting");
        //}
        //else
        //{
        //    Debug.Log("Not shooting");
        //}

        //New system
        //if pushing fire button then fire
        bool isActive = _fire.ReadValue<float>() > 0.5;

        SetLasersActive(isActive);
    }

    void SetLasersActive(bool isActive)
    {
        //foreach of the lasers that we have activate them
        foreach (GameObject laser in _lasers)
        {
            ParticleSystem.EmissionModule emissionModule = laser.GetComponent<ParticleSystem>().emission;

            emissionModule.enabled = isActive;
        }
    }

    void OnEnable()
    {
        _fire.Enable();
        _movement.Enable();
    }

    void OnDisable()
    {
        SetLasersActive(false);
        _fire.Disable();
        _movement.Disable();
    }
}
