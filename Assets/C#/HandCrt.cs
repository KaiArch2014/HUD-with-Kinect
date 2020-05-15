using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCrt : MonoBehaviour
{
    public Camera screenCamera;
    private KinectManager manager;

    public InteractionManager interactionManager;

    public Transform right_cube;
    public Transform left_cube;

    private InteractionManager.HandEventType RightHandEvent = InteractionManager.HandEventType.None;
    private InteractionManager.HandEventType LeftHandEvent = InteractionManager.HandEventType.None;

    private Vector3 RightHandGrip_pos;
    private Vector3 LeftHandGrip_pos;


    private Vector3 RightHand_pos;
    private Vector3 LeftHand_pos;

    public HandCrt.HandGripHorizontalType RightHandHorizontalEvent = HandCrt.HandGripHorizontalType.None;
    public HandCrt.HandGripVerticalType RightHandVerticalEvent = HandCrt.HandGripVerticalType.None;

    public HandCrt.HandGripHorizontalType LeftHandHorizontalEvent = HandCrt.HandGripHorizontalType.None;
    public HandCrt.HandGripVerticalType LeftHandVerticalEvent = HandCrt.HandGripVerticalType.None;

    public GameCtr game_ctr;

    public bool handHover;
    float t;

    public bool standby;
    float s;

   

    public bool close;
    float c;

    public enum HandGripHorizontalType : int
    {
        None = 0,
        Left = 1,
        Right = 2,        
    }

    public enum HandGripVerticalType : int
    {
        None = 0,
        Up = 1,
        Down = 2,
    }

    // Start is called before the first frame update
    void Start()
    {
        s = 0;
        c = 0;

          manager = KinectManager.Instance;
        standby = false;
        handHover = false;
        if (screenCamera == null)
        {
            screenCamera = Camera.main;
        }
        // 获取交互管理器实例
        if (interactionManager == null)
        {
            interactionManager = InteractionManager.Instance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (interactionManager != null && interactionManager.IsInteractionInited()&& manager.IsUserDetected(0))
        {
      
        
            right_cube.position = ScreenToPixel(interactionManager.GetRightHandScreenPos())/100;

            left_cube.position = ScreenToPixel(interactionManager.GetLeftHandScreenPos()) / 100;

            RightHandEvent = interactionManager.GetRightHandEvent();
            LeftHandEvent = interactionManager.GetLeftHandEvent();

            RightHand_pos =  interactionManager.GetRightHandScreenPos();
            LeftHand_pos = interactionManager.GetLeftHandScreenPos();

            if (RightHandEvent != InteractionManager.HandEventType.Grip && RightHandEvent != InteractionManager.HandEventType.None)
            {
                RightHandGrip_pos = interactionManager.GetRightHandScreenPos();
                RightHandVerticalEvent = HandGripVerticalType.None;
                RightHandHorizontalEvent = HandGripHorizontalType.None;
            }
            else {
                if (RightHand_pos.x < RightHandGrip_pos.x&&Mathf.Abs(RightHand_pos.x- RightHandGrip_pos.x)>0.1f)
                {
                    RightHandHorizontalEvent = HandGripHorizontalType.Left;
                }
                else if (RightHand_pos.x > RightHandGrip_pos.x && Mathf.Abs(RightHand_pos.x - RightHandGrip_pos.x) > 0.1f)
                {
                    RightHandHorizontalEvent = HandGripHorizontalType.Right;
                }
                else if (Mathf.Abs(RightHand_pos.x - RightHandGrip_pos.x) < 0.1f) {
                    RightHandHorizontalEvent = HandGripHorizontalType.None;
                }

                if (RightHand_pos.y > RightHandGrip_pos.y && Mathf.Abs(RightHand_pos.y - RightHandGrip_pos.y) > 0.1f)
                {
                    RightHandVerticalEvent = HandGripVerticalType.Up;
                }
                else if (RightHand_pos.y < RightHandGrip_pos.y && Mathf.Abs(RightHand_pos.y - RightHandGrip_pos.y) > 0.1f)
                {
                    RightHandVerticalEvent = HandGripVerticalType.Down;
                }
                else if ( Mathf.Abs(RightHand_pos.y - RightHandGrip_pos.y) < 0.1f)
                {
                    RightHandVerticalEvent = HandGripVerticalType.None;
                }
            }


            if (LeftHandEvent != InteractionManager.HandEventType.Grip&& LeftHandEvent!= InteractionManager.HandEventType.None)
            {
                LeftHandGrip_pos = interactionManager.GetLeftHandScreenPos();
                LeftHandHorizontalEvent = HandGripHorizontalType.None;
                LeftHandVerticalEvent = HandGripVerticalType.None;
            }
            else {
                if (LeftHand_pos.x < LeftHandGrip_pos.x && Mathf.Abs(LeftHand_pos.x - LeftHandGrip_pos.x) > 0.1f)
                {
                    LeftHandHorizontalEvent = HandGripHorizontalType.Left;
                }
                else if (LeftHand_pos.x > LeftHandGrip_pos.x && Mathf.Abs(LeftHand_pos.x - LeftHandGrip_pos.x) > 0.1f)
                {
                    LeftHandHorizontalEvent = HandGripHorizontalType.Right;
                }
                else if (  Mathf.Abs(LeftHand_pos.x - LeftHandGrip_pos.x) < 0.1f)
                {
                    LeftHandHorizontalEvent = HandGripHorizontalType.None;
                }

                if (LeftHand_pos.y > LeftHandGrip_pos.y && Mathf.Abs(LeftHand_pos.y - LeftHandGrip_pos.y) > 0.1f)
                {
                    LeftHandVerticalEvent = HandGripVerticalType.Up;
                }
                else if (LeftHand_pos.y < LeftHandGrip_pos.y && Mathf.Abs(LeftHand_pos.y - LeftHandGrip_pos.y) > 0.1f)
                {
                    LeftHandVerticalEvent = HandGripVerticalType.Down;
                }
                else if (Mathf.Abs(LeftHand_pos.y - LeftHandGrip_pos.y) < 0.1f)
                {
                    LeftHandVerticalEvent = HandGripVerticalType.None;
                }
            }

            if (LeftHandEvent == InteractionManager.HandEventType.Grip&& RightHandEvent == InteractionManager.HandEventType.Grip && RightHandVerticalEvent == HandGripVerticalType.None && RightHandHorizontalEvent == HandGripHorizontalType.None && LeftHandHorizontalEvent == HandGripHorizontalType.None && LeftHandVerticalEvent == HandGripVerticalType.None)
            {
                t += Time.deltaTime;

                if (t > 2)
                {
                    handHover = true;
                }
            }
            else {
                handHover = false;
                t = 0;
            }

            if (LeftHandEvent == InteractionManager.HandEventType.Release && RightHandEvent == InteractionManager.HandEventType.Release&& game_ctr.UISTATE!=GameCtr.UIState.Close)
            {
                s += Time.deltaTime;
                c += Time.deltaTime;

                if (s > 6)
                {
                    if (!standby)
                    {
                        standby = true;
                        s = 0;
                    }
                    else {
                        standby = false;
                        s = 0;
                    }
                   
                }

                if (c > 10)
                {
                    c = 0;
                    s = 0;
                    close = true;
                    
                }
                else {
                    close = false;
                  
                }
            }
            else {
                
                //standby = false;
                s = 0;
                c = 0;
                close = false;
            }


            //Debug.Log("Left:" + LeftHandEvent + "  "+ LeftHandHorizontalEvent + "   " + LeftHandVerticalEvent);
        }
      

        
    }
    Vector3 pixel_pos;
    public Vector3 ScreenToPixel(Vector3 screnn_pos) {
     

        pixel_pos.x = (int)(screnn_pos.x * (screenCamera ? screenCamera.pixelWidth : Screen.width));
        pixel_pos.y = (int)(screnn_pos.y * (screenCamera ? screenCamera.pixelHeight : Screen.height));

        return pixel_pos;
    }
}
