using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyInteractionListener : MonoBehaviour, InteractionListenerInterface
{

    [Tooltip("Camera used for screen ray-casting. This is usually the main camera.  用于屏幕光线投射的照相机。这通常是主相机。")]
    public Camera screenCamera;

    //[Tooltip("Interaction manager instance, used to detect hand interactions. If left empty, it will be the first interaction manager found in the scene.")]
    [Tooltip("交互管理器实例，用于检测手交互。如果为空，它将是场景中发现的第一个交互管理器")]
    public InteractionManager interactionManager;

    // hand interaction variables   手的交互变量
    private InteractionManager.HandEventType lastHandEvent = InteractionManager.HandEventType.None;

    // normalized and pixel position of the cursor  鼠标光标的统一化和位置
    private Vector3 screenNormalPos = Vector3.zero;
    private Vector3 screenPixelPos = Vector3.zero;
    private Vector3 newObjectPos = Vector3.zero;

    public GameObject game;


    /*[Tooltip("Minimum Z-position of the dragged object, when moving forward and back.")]
    public float minZ = 0f;

    [Tooltip("Maximum Z-position of the dragged object, when moving forward and back.")]
    public float maxZ = 5f;*/


    /// <summary>
    /// UDP客户端
    /// </summary>


    // Use this for initialization
    void Start()
    {
        //默认情况下，将主相机设置为屏幕摄像机
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
        //如果交互管理器不为空，并且已经初始化
        if (interactionManager != null && interactionManager.IsInteractionInited())
        {

            // check if there is an underlying object to be selected    检查是否存在要选择的基础对象
            if (lastHandEvent == InteractionManager.HandEventType.Grip && screenNormalPos != Vector3.zero)
            {
                // convert the normalized screen pos to pixel pos   将标准化屏幕POS转换为像素POS
                screenNormalPos = interactionManager.IsLeftHandPrimary() ? interactionManager.GetLeftHandScreenPos() : interactionManager.GetRightHandScreenPos();

                screenPixelPos.x = (int)(screenNormalPos.x * (screenCamera ? screenCamera.pixelWidth : Screen.width));
                screenPixelPos.y = (int)(screenNormalPos.y * (screenCamera ? screenCamera.pixelHeight : Screen.height));
                Ray ray = screenCamera ? screenCamera.ScreenPointToRay(screenPixelPos) : new Ray();

                game.transform.position = Camera.main.ScreenToWorldPoint(screenPixelPos); 
                // check if there is an underlying objects  检查是否存在底层对象
                RaycastHit hit;

                Debug.Log("握拳");
                Debug.Log(screenPixelPos);
                //udpClient.SocketSend(screenPixelPos.x + "," + screenPixelPos.y);
                //udpClient.SocketSend(screenNormalPos.x + "," + screenNormalPos.y);
                //udpClient.SocketSend(screenNormalPos.x.ToString("f2") + "," + screenNormalPos.y.ToString("f2"));//f2保留两位小数
                //udpClient.SocketSend("anXia");
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("位置");
                    Debug.Log(hit.point);
                    /*foreach (GameObject obj in draggableObjects)
                    {
                        if (hit.collider.gameObject == obj)
                        {
                            // an object was hit by the ray. select it and start drgging
                            draggedObject = obj;
                            draggedObjectOffset = hit.point - draggedObject.transform.position;
                            draggedObjectOffset.z = 0; // don't change z-pos

                            draggedNormalZ = (minZ + screenNormalPos.z * (maxZ - minZ)) -
                                draggedObject.transform.position.z; // start from the initial hand-z

                            // set selection material
                            draggedObjectMaterial = draggedObject.GetComponent<Renderer>().material;
                            draggedObject.GetComponent<Renderer>().material = selectedObjectMaterial;

                            // stop using gravity while dragging object
                            draggedObject.GetComponent<Rigidbody>().useGravity = false;
                            break;
                        }
                    }*/
                }
            }
            else
            {

                // continue dragging the object 继续拖动对象
                screenNormalPos = interactionManager.IsLeftHandPrimary() ? interactionManager.GetLeftHandScreenPos() : interactionManager.GetRightHandScreenPos();

                // convert the normalized screen pos to 3D-world pos    将标准化的屏幕POS转换为3D世界POS
                screenPixelPos.x = (int)(screenNormalPos.x * (screenCamera ? screenCamera.pixelWidth : Screen.width));
                screenPixelPos.y = (int)(screenNormalPos.y * (screenCamera ? screenCamera.pixelHeight : Screen.height));
                /*//screenPixelPos.z = screenNormalPos.z + draggedObjectDepth;
                screenPixelPos.z = (minZ + screenNormalPos.z * (maxZ - minZ)) - draggedNormalZ -
                    (screenCamera ? screenCamera.transform.position.z : 0f);*/


                //Debug.Log("手掌");
                //Debug.Log(screenPixelPos);
                //udpClient.SocketSend(screenPixelPos.x + "," + screenPixelPos.y);
                //udpClient.SocketSend(screenNormalPos.x + "," + screenNormalPos.y);
                //udpClient.SocketSend(screenNormalPos.x.ToString("f2") + "," + screenNormalPos.y.ToString("f2"));
                //udpClient.SocketSend("taiQi");
                //newObjectPos = screenCamera.ScreenToWorldPoint(screenPixelPos) - draggedObjectOffset;

                // check if the object (hand grip) was released
                /*bool isReleased = lastHandEvent == InteractionManager.HandEventType.Release;

                if (isReleased)
                {
                    // restore the object's material and stop dragging the object
                    draggedObject.GetComponent<Renderer>().material = draggedObjectMaterial;

                    if (useGravity)
                    {
                        // add gravity to the object
                        draggedObject.GetComponent<Rigidbody>().useGravity = true;
                    }

                    draggedObject = null;
                }*/
            }
        }

    }
    public void HandGripDetected(long userId, int userIndex, bool isRightHand, bool isHandInteracting, Vector3 handScreenPos)
    {
        if (!isHandInteracting || !interactionManager)
            return;
        if (userId != interactionManager.GetUserID())
            return;

        lastHandEvent = InteractionManager.HandEventType.Grip;
        //isLeftHandDrag = !isRightHand;
        screenNormalPos = handScreenPos;
    }

    public void HandReleaseDetected(long userId, int userIndex, bool isRightHand, bool isHandInteracting, Vector3 handScreenPos)
    {
        if (!isHandInteracting || !interactionManager)
            return;
        if (userId != interactionManager.GetUserID())
            return;

        lastHandEvent = InteractionManager.HandEventType.Release;
        //isLeftHandDrag = !isRightHand;
        screenNormalPos = handScreenPos;
    }
    public bool HandClickDetected(long userId, int userIndex, bool isRightHand, Vector3 handScreenPos)
    {
        return true;
    }
}