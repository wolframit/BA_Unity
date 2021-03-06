﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InputSystem
{
    /// <summary>
    /// 1st Level Module
    /// 
    /// Captures all raw input.
    /// </summary>

    [CreateAssetMenu(fileName = "RawInput", menuName = "Input/RawInput", order = 1)]
    public class BA_RawInput : ScriptableObject, IUpdatable
    {

        [SerializeField]
        private KeyCode Keyboard_0;
        [SerializeField]
        private KeyCode Keyboard_1;
        [SerializeField]
        private float MaxSwipeTime;
        [SerializeField]
        private float MinSwipeDist;

        #region Input Delegates

        //Base
        public delegate void InputDelegate();
        public delegate void InputDelegateVector3(Vector3 v);
        public delegate void InputDelegateVectorSwipe(Vector2 start, Vector2 end);
        public delegate void InputDelegateTouch(Touch t);
        public delegate void InputDelegateFloat(float f);

        //Mouse
        public InputDelegate Mouse_0_Down;
        public InputDelegate Mouse_0_Up;
        public InputDelegateVector3 Mouse_Position;

        //Keyboard 0
        public InputDelegate Keyboard_0_Down;
        public InputDelegate Keyboard_0_Up;

        //Keyboard 1
        public InputDelegate Keyboard_1_Down;
        public InputDelegate Keyboard_1_Up;

        //Touch 0
        public InputDelegate Touch_0_Down;
        public InputDelegate Touch_0_Up;
        public InputDelegateTouch Touch_0;
        public InputDelegateVectorSwipe Swipe_0;

        //Touch 1
        public InputDelegate Touch_1_Down;
        public InputDelegate Touch_1_Up;
        public InputDelegateTouch Touch_1;

        //Gamepad Axis Left
        public InputDelegateFloat Gamepad_Axis_Left_X;
        public InputDelegateFloat Gamepad_Axis_Left_Y;

        //Gamepad Axis Right
        public InputDelegateFloat Gamepad_Axis_Right_X;
        public InputDelegateFloat Gamepad_Axis_Right_Y;

        //Gamepad 0
        public InputDelegate Gamepad_0_Down;
        public InputDelegate Gamepad_0_Up;

        //Gamepad 1
        public InputDelegate Gamepad_1_Down;
        public InputDelegate Gamepad_1_Up;

        #endregion

        public static float TickTime;

        #region Private Cached Values

        private float _cachedX_Left;
        private float _cachedY_Left;
        private float _cachedX_Right;
        private float _cachedY_Right;
        private Vector2 _cachedTouchStart0;
        private float _cachedSwipeStartTime0;


        #endregion

        public void Update()
        {

            #region Get Mouse Input
            if (Input.GetMouseButtonDown(0))
            {
                //TickTime = Time.time;
                //Debug.Log("raw" + Time.time);
                Mouse_0_Down();
            }

            if (Input.GetMouseButtonUp(0))
            {                
                Mouse_0_Up();
                //DataPipe.instance.PlayerReferences.MainTransform.GetComponent<PlayerMovement>().MoveVector3(Input.mousePosition);
            }

            Mouse_Position(Input.mousePosition);
            #endregion

            #region Get Keyboard Input

            if (Input.GetKeyDown(Keyboard_0))
                Keyboard_0_Down();
            else if (Input.GetKeyUp(Keyboard_0))
                Keyboard_0_Up();

            if (Input.GetKeyDown(Keyboard_1))
                Keyboard_1_Down();
            else if (Input.GetKeyUp(Keyboard_1))
                Keyboard_1_Up();

            #endregion

            #region Get Touch Input

            //Touch 0
            if (Input.touchCount > 0)
            {
                Touch_0(Input.GetTouch(0));
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Touch_0_Down();
                    _cachedTouchStart0 = Input.GetTouch(0).position;
                    _cachedSwipeStartTime0 = Time.time;
                }
                else if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    if ((Input.GetTouch(0).position - _cachedTouchStart0).sqrMagnitude > MinSwipeDist)
                    {
                        if (Time.time - _cachedSwipeStartTime0 < MaxSwipeTime)
                        {                            
                            Swipe_0(_cachedTouchStart0, Input.GetTouch(0).position);
                            _cachedTouchStart0 = Vector2.zero;
                        }
                    }
                    else Touch_0_Up();
                }                
            }

            //Touch 1
            if (Input.touchCount > 1)
            {
                Touch_1(Input.GetTouch(1));
                if (Input.GetTouch(1).phase == TouchPhase.Began)
                {
                    Touch_1_Down();
                }
                else if (Input.GetTouch(1).phase == TouchPhase.Ended)
                {
                    Touch_1_Up();
                }
            }

            #endregion

            #region Gamepad Input

            Gamepad_Axis_Left_X(Input.GetAxis("Horizontal"));
            Gamepad_Axis_Left_Y(Input.GetAxis("Vertical"));

            Gamepad_Axis_Right_X(Input.GetAxis("Horizontal_2"));
            Gamepad_Axis_Right_Y(Input.GetAxis("Vertical_2"));

            if (Input.GetKeyDown("joystick button 0"))
            {
                Gamepad_0_Down();
            }
            else if (Input.GetKeyUp("joystick button 0"))
                Gamepad_0_Up();

            if (Input.GetKeyDown("joystick button 5"))
                Gamepad_1_Down();
            else if (Input.GetKeyUp("joystick button 5"))
                Gamepad_1_Up();

            #endregion

        }
    }

}
