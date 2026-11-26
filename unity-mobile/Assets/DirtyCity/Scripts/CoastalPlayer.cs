using UnityEngine;
using UnityEngine.EventSystems;

namespace DirtyCity
{
    // Attach to a rigged character root with CharacterController.
    // This component deliberately has no primitive or placeholder artwork.
    [RequireComponent(typeof(CharacterController))]
    public sealed class CoastalPlayer : MonoBehaviour
    {
        [SerializeField] Camera followCamera;
        [SerializeField] Animator animator;
        [SerializeField] LayerMask cameraObstacles;
        [SerializeField] float walkSpeed = 3.1f, runSpeed = 6.0f;
        [SerializeField] float cameraDistance = 3.6f, shoulderOffset = .42f;
        [SerializeField] float lookSensitivity = .14f, jumpHeight = .85f;
        CharacterController controller;
        Vector2 move, stickOrigin;
        int moveFinger = -1, lookFinger = -1;
        float yaw, pitch = 12f, verticalVelocity;
        bool sprint, jumpRequested;
        Vector3 cameraVelocity;
        readonly int speedId = Animator.StringToHash("Speed");
        readonly int groundId = Animator.StringToHash("Grounded");

        void Awake()
        {
            controller = GetComponent<CharacterController>();
            if (!followCamera) followCamera = Camera.main;
            if (!animator) animator = GetComponentInChildren<Animator>();
            yaw = transform.eulerAngles.y;
            Application.targetFrameRate = 30;
        }

        public void SetSprint(bool value) => sprint = value;
        public void RequestJump() => jumpRequested = true;

        void OnDisable() => ResetInput();
        void OnApplicationFocus(bool focused) { if (!focused) ResetInput(); }
        void ResetInput()
        {
            moveFinger = lookFinger = -1;
            move = Vector2.zero;
            sprint = jumpRequested = false;
        }

        void ReadInput()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            move = Vector2.ClampMagnitude(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")), 1f);
            sprint = Input.GetKey(KeyCode.LeftShift);
            if (Input.GetKeyDown(KeyCode.Space)) RequestJump();
            if (Input.GetMouseButton(1))
            {
                yaw += Input.GetAxis("Mouse X") * 4f;
                pitch -= Input.GetAxis("Mouse Y") * 4f;
            }
#endif
            foreach (Touch touch in Input.touches)
            {
                int id = touch.fingerId;
                if (touch.phase == TouchPhase.Began)
                {
                    if (EventSystem.current && EventSystem.current.IsPointerOverGameObject(id)) continue;
                    if (touch.position.x < Screen.width * .45f && moveFinger < 0)
                    {
                        moveFinger = id; stickOrigin = touch.position;
                    }
                    else if (touch.position.x >= Screen.width * .45f && lookFinger < 0) lookFinger = id;
                }
                bool ended = touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended;
                if (id == moveFinger)
                {
                    move = ended ? Vector2.zero : Vector2.ClampMagnitude((touch.position - stickOrigin) / Mathf.Max(45f, Screen.height * .14f), 1f);
                    if (move.magnitude < .12f) move = Vector2.zero;
                    if (ended) moveFinger = -1;
                }
                if (id == lookFinger)
                {
                    if (!ended)
                    {
                        float scale = 1080f / Mathf.Max(1, Screen.height);
                        yaw += touch.deltaPosition.x * lookSensitivity * scale;
                        pitch -= touch.deltaPosition.y * lookSensitivity * scale;
                    }
                    else lookFinger = -1;
                }
            }
            pitch = Mathf.Clamp(pitch, -18f, 48f);
        }

        void Update()
        {
            ReadInput();
            float dt = Time.deltaTime;
            Quaternion facing = Quaternion.Euler(0, yaw, 0);
            Vector3 direction = facing * new Vector3(move.x, 0, move.y);
            if (direction.sqrMagnitude > .01f)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 1f - Mathf.Exp(-12f * dt));

            if (controller.isGrounded)
            {
                verticalVelocity = -2f;
                if (jumpRequested) verticalVelocity = Mathf.Sqrt(2f * 18f * jumpHeight);
            }
            jumpRequested = false;
            verticalVelocity -= 18f * dt;
            float targetSpeed = sprint ? runSpeed : walkSpeed;
            controller.Move((direction * targetSpeed + Vector3.up * verticalVelocity) * dt);
            if (animator)
            {
                animator.SetFloat(speedId, move.magnitude * targetSpeed, .12f, dt);
                animator.SetBool(groundId, controller.isGrounded);
            }
        }

        void LateUpdate()
        {
            if (!followCamera) return;
            Vector3 target = transform.position + Vector3.up * 1.55f;
            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
            Vector3 offset = rotation * new Vector3(shoulderOffset, .12f, -cameraDistance);
            float distance = offset.magnitude;
            if (Physics.SphereCast(target, .18f, offset.normalized, out RaycastHit hit, distance, cameraObstacles, QueryTriggerInteraction.Ignore))
                offset = offset.normalized * Mathf.Max(.15f, hit.distance - .10f);
            Transform cam = followCamera.transform;
            Vector3 desired = target + offset;
            cam.position = Vector3.SmoothDamp(cam.position, desired, ref cameraVelocity, .065f);
            Vector3 ray = cam.position - target;
            if (ray.sqrMagnitude > .001f && Physics.SphereCast(target, .15f, ray.normalized, out RaycastHit clamp, ray.magnitude, cameraObstacles, QueryTriggerInteraction.Ignore))
                cam.position = target + ray.normalized * Mathf.Max(.15f, clamp.distance - .08f);
            cam.rotation = Quaternion.LookRotation(target + rotation * Vector3.forward * 1.4f - cam.position);
        }
    }
}
