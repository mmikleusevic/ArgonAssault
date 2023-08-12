using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction _movement;

    void Update()
    {
        //Old System
        //float horizontalThrow = Input.GetAxis("Horizontal");

        //float verticalThrow = Input.GetAxis("Vertical");

        //New System
        float xThrow = _movement.ReadValue<Vector2>().x;
        float yThrow = _movement.ReadValue<Vector2>().y;

        float xOffset = 0.02f;
        float yOffset = 0.02f;

        float xPos = transform.localPosition.x + xOffset;
        float yPos = transform.localPosition.y + yOffset;


        transform.localPosition = new Vector3(xPos, yPos, transform.localPosition.z);


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
