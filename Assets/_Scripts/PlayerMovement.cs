using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] Joystick joystick;
    [SerializeField] Transform playerSprite;
    [SerializeField] Animator animator;

    bool Movement;
    public float movementSpeed = 6;
    // Start is called before the first frame update
    void Start()
    {
        playerSprite.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TouchInput();
    }
    void TouchInput()
    {
        if (Mathf.Abs(joystick.Direction.x) > 0f || Mathf.Abs(joystick.Direction.y) > 0f)
        {
            playerSprite.gameObject.SetActive(true);

            playerSprite.position = new Vector3(joystick.Horizontal + transform.position.x, playerSprite.position.y, joystick.Vertical + transform.position.z);

            transform.LookAt(new Vector3(playerSprite.position.x, 0, playerSprite.position.z));

            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);

            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }
        else
        {
            playerSprite.gameObject.SetActive(false);
        }
    }
}
