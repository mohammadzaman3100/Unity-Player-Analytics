using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float jumpSpeed = 4.0f;
    public float gravity = 9.8f;
    public float terminalVelocity = 100f;

    private CharacterController _charCont;
    private Vector3 _moveDirection = Vector3.zero;


    private void Start()
    {
        _charCont = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (true)
            HandlePlayerMove();
        else
            HandlePlayerInactiveMove();
    }

    private void HandlePlayerMove()
    {
        var deltaX = Input.GetAxis("Horizontal") * moveSpeed;
        var deltaZ = Input.GetAxis("Vertical") * moveSpeed;
        _moveDirection = new Vector3(deltaX, _moveDirection.y, deltaZ);

        if (_charCont.isGrounded)
        {
            if (Input.GetButton("Jump"))
                _moveDirection.y = jumpSpeed;
            else
                _moveDirection.y = 0f;

            if (deltaX != 0 || deltaZ != 0)
            {
            }
        }

        ApplyMovement();
    }

    private void HandlePlayerInactiveMove()
    {
        _moveDirection = Vector3.zero;
        ApplyMovement();
    }

    private void ApplyMovement()
    {
        _moveDirection = transform.TransformDirection(_moveDirection);

        _moveDirection.y -= gravity * Time.deltaTime;

        _charCont.Move(_moveDirection * Time.deltaTime);
    }
}