using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Camera goCamera;
    public float walkSpeed = 8f;
    public float runSpeed = 12f;
    public float stamina = 100f;
    public float staminaDecreaseRate = 10f;
    public float staminaRecoveryRate = 5f;
    public Slider staminaBar;

    private bool isRunning;

    private Rigidbody rb;

    private float speedScale = 8f,
                    jumpForce = 8f,
                    turnSpeed = 200f;

    private float mouseX = 0f,
                  mouseY = 0f,
                  currentAngleX = 0f;

    
public bool canJump = true;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        staminaBar.value = stamina;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            speedScale = runSpeed;
            isRunning = true;
            stamina -= staminaDecreaseRate * Time.deltaTime;

            if (stamina <= 0)
            {
                stamina = 0;
                isRunning = false;
            }
        }
        else
        {
            isRunning = false;
            if (stamina < 100)
            {
                stamina += staminaRecoveryRate * Time.deltaTime;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speedScale = walkSpeed;
        }
        RotateCharacter();
        MoveCharacter();
        staminaBar.value = stamina;
    }

    private void RotateCharacter()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(new Vector3(0f, mouseX * turnSpeed * Time.deltaTime, 0f));

        currentAngleX += mouseY * turnSpeed * Time.deltaTime * -1f;
        currentAngleX = Mathf.Clamp(currentAngleX, -60f, 60f);

        goCamera.transform.localEulerAngles = new Vector3(currentAngleX, 0f, 0f);
    }

    private void MoveCharacter()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        direction = transform.TransformDirection(direction) * speedScale;
         rb.velocity = new Vector3(direction.x, rb.velocity.y, direction.z);
        if(canJump)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            }

        

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        canJump = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        canJump = false;
    }
}
