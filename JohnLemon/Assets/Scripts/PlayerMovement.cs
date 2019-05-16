using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 m_Movement;
    Animator m_Animator;
    Quaternion m_Rotation = Quaternion.identity;
    Rigidbody m_Rigidbody;
    public float turnSpeed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        //Normalizes diagonial movement to make it equal to vertical/ horizontal
        m_Movement.Normalize();

        //If these two parameters are not approx equal
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);

        //if either of these are true...
        bool isWalking = hasHorizontalInput || hasVerticalInput;

        m_Animator.SetBool("IsWalking", isWalking);

        //turnSpeed * Time.deltaTime = change in angle, last param is magnitude 
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

    }


    // This method allows you to apply root motion as you want. AKA movement and rotation can be applied separatly.
    private void OnAnimatorMove()
    {

        // New position starts off at Rigidbody's current position and then add a change - 
        //the movement vector multiplied by the magnitude of the Animator's deltaPosition
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);

        //apply rotation
        m_Rigidbody.MoveRotation(m_Rotation);

    }



}