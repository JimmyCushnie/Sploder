using UnityEngine;
using CommandTerminalPlus;

namespace Splosions
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] new Camera camera;

        [CommandTerminalPlus.RegisterVariable("CameraSpeed")]
        public static float speed { get; set; } = 50f;
        public static float sensitivity = 0.01f;

        private void Move(Vector3 direction) => transform.position += direction * speed * Time.unscaledDeltaTime;
        private float AxisValue(string axis) => Input.GetAxis(axis);
        private bool Axis(string axis) => AxisValue(axis) > 0;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            Terminal.WhenTerminalOpens += Disable;
            Terminal.WhenTerminalCloses += Enable;
        }

        void Disable() => this.enabled = false;
        void Enable() => this.enabled = true;

        void Update()
        {
            var deltaX = AxisValue("MouseX") * sensitivity * Time.unscaledDeltaTime;
            var deltaY = AxisValue("MouseY") * sensitivity * Time.unscaledDeltaTime;
            transform.Rotate(new Vector3(-deltaY, deltaX, 0));

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
}