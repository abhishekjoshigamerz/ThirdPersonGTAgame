using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

   Vector2 moveDirection;
   [SerializeField] float moveSpeed = 2;
   [SerializeField] float maxForwardSpeed = 8;
   float desiredForwardSpeed;
   float forwardSpeed;
   const float groundAccel = 5;
   const float groundDecel = 25; 

    Animator anim;

   bool IsMoveInput
   {
       get { return !Mathf.Approximately(moveDirection.sqrMagnitude, 0f); }
   }

   public void OnMove(InputAction.CallbackContext context)
   {
       moveDirection = context.ReadValue<Vector2>();
       
   }

   void Move(Vector2 direction)
   {
       if(direction.sqrMagnitude > 1f){
           direction.Normalize();
       }

        desiredForwardSpeed = direction.magnitude * maxForwardSpeed; 
        float acceleration = IsMoveInput ? groundAccel : groundDecel;

        forwardSpeed = Mathf.MoveTowards(forwardSpeed, desiredForwardSpeed, acceleration * Time.deltaTime);
        anim.SetFloat("ForwardSpeed",forwardSpeed);
       
   }

   void Start()
   {
       anim = this.GetComponent<Animator>();
   }

   void Update()
   {
       Move(moveDirection);
   }
}
