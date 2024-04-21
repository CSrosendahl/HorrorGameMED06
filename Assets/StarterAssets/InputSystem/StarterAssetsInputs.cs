using UnityEngine;
using Unity.Netcode;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
    public class StarterAssetsInputs : NetworkBehaviour
    {
       

        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;
        public bool sneak;
  

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

       
        public MonsterAbility monsterAbility;
        public HumanAbility humanAbility;




#if ENABLE_INPUT_SYSTEM

        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
           
        }

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }

        public void OnJump(InputValue value)
        {
            JumpInput(value.isPressed);
           
        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }
        public void OnSneak(InputValue value)
        {
            SneakInput(value.isPressed);
        }



        public void OnZ()
        {
          
            Debug.Log("Z pressed");

        }
        public void OnQ()
        {
            if(monsterAbility != null)
            {
                monsterAbility.MonsterReach();
            }
            if(humanAbility != null)
            {
                humanAbility.HumanThrow();
            }
           
            Debug.Log("Q pressed");

        }
        public void OnF()
        {
            if (monsterAbility != null)
            {
                monsterAbility.MonsterScream();
            }
            if (humanAbility != null)
            {
               //
            }

            Debug.Log("F pressed");

        }



#endif

        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }
        public void SneakInput(bool newSneakState)
        {
            sneak = newSneakState;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }

}
