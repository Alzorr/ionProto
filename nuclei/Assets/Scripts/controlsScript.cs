using UnityEngine;
using System.Collections;

public class controlsScript : MonoBehaviour
{

    public Transform nucleus;
    public Camera mainCamera;

    //CAMERA
    public float smoothTime = 1f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 screenPoint;
    private Vector3 offset;

    //CONTROLS
    private bool draggingItem = false;
    private GameObject draggedObject;
    private Vector2 touchOffset;



    void Start()
    { 
        nucleus = GameObject.FindWithTag("nucleus").transform;
    }

    void Update()
    {

        if (HasInput)
            DragOrPickUp();
        else if (draggingItem)
                DropItem();

        //CAMERA
        Vector3 goalPos = nucleus.position;
        goalPos.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, goalPos, ref velocity, smoothTime);
    }

    Vector2 CurrentTouchPosition
    {
        get
        {
            Vector2 inputPos;
            inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            return inputPos;
        }
    }

    private void DragOrPickUp()
    {
        var inputPosition = CurrentTouchPosition;

        if (draggingItem)
            draggedObject.transform.position = inputPosition + touchOffset;
        else
        {
            RaycastHit2D[] touches = Physics2D.RaycastAll(inputPosition, inputPosition, 0.5f);
            if (touches.Length > 0)
            {
                var hit = touches[0];
                if (hit.transform != null && hit.transform.gameObject.tag == "nucleus")
                {
                    draggingItem = true;
                    draggedObject = hit.transform.gameObject;
                    touchOffset = (Vector2)hit.transform.position - inputPosition;
                    draggedObject.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                }
            }
        }
    }

    private bool HasInput
    {
        // returns true if either the mouse button is down or at least one touch is felt on the screen
        get{ return Input.GetMouseButton(0);}
    }

    void DropItem()
    {
        draggingItem = false;
        draggedObject.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    }

}

