using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera camera;

    public static float speed = 50f;
    public static float sensitivity = 0.01f;

    private void Move(Vector3 direction) => transform.position += direction * speed * Time.unscaledDeltaTime;
    private float AxisValue(string axis) => Input.GetAxis(axis);
    private bool Axis(string axis) => AxisValue(axis) > 0;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        var deltaX = AxisValue("MouseX") * sensitivity * Time.unscaledDeltaTime;
        var deltaY = AxisValue("MouseY") * sensitivity * Time.unscaledDeltaTime;
        transform.Rotate(new Vector3(-deltaY, deltaX, 0));

        //transform.localEulerAngles = new Vector3(deltaX, deltaY, transform.localEulerAngles.z);

        if (Axis("MoveUp"))
            Move(transform.up);
        if (Axis("MoveDown"))
            Move(-transform.up);
        if (Axis("MoveForward"))
            Move(transform.forward);
        if (Axis("MoveBack"))
            Move(-transform.forward);
        if (Axis("MoveRight"))
            Move(transform.right);
        if (Axis("MoveLeft"))
            Move(-transform.right);
    }
}
