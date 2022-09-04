using UnityEngine;

public class Drag : MonoBehaviour
{
    public float dragSpeed = 0.1f;
    private Vector3 dragOrigin;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void Start()
    {

        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void LateUpdate()
    {   
        
        if (Camera.main.transform.position.y == 15)
        {
            if (Input.GetMouseButton(0))
            {
                Camera.main.transform.position -= new Vector3(Input.GetAxis("Mouse X") * dragSpeed, 0, 0);
            }
            
            /*
            if (Input.GetMouseButtonDown(0) && dragOrigin == new Vector3(0, 0, 0))
            {
                dragOrigin = Input.mousePosition;
                return;
            }

            if (!Input.GetMouseButton(0))
            {
                dragOrigin = new Vector3(0, 0, 0);
                return;

            }


            if (dragOrigin != new Vector3(0, 0, 0))
            {
                Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
                Vector3 move = new Vector3(-1 * pos.x * dragSpeed, 0, 0);

                transform.Translate(move, Space.World);

                dragOrigin = new Vector3(0, 0, 0);
            } */
        }

        if (Camera.main.transform.position.x < 0)
        {
            Camera.main.transform.position = new Vector3(0, 15, Camera.main.transform.position.z);
        }
        if (Camera.main.transform.position.x > 93.12006f)
        {
            Camera.main.transform.position = new Vector3(93.12006f, 15, Camera.main.transform.position.z);
        }


    }


}