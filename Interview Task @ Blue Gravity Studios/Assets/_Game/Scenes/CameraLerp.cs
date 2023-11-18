using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLerp : MonoBehaviour
{
    public Transform lookAt;
    public Vector2 innerBounds;
    public Vector2 outerBounds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        float deltaX = lookAt.position.x - transform.position.x;

        if (deltaX > innerBounds.x || deltaX < -innerBounds.x)
        {
            if (transform.position.x < lookAt.position.x)
                delta.x = deltaX - innerBounds.x;
            else
                delta.x = deltaX + innerBounds.x;
        }

        float deltaY = lookAt.position.y - transform.position.y;

        if (deltaY > innerBounds.y || deltaY < -innerBounds.y)
        {
            if (transform.position.y < lookAt.position.y)
                delta.y = deltaY - innerBounds.y;
            else
                delta.y = deltaY + innerBounds.y;
        }

        delta.z = 0;

        transform.position += delta;
        
        //transform.position = Vector3.Lerp(transform.position, delta, Time.deltaTime);
    }
}
