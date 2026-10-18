using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxCamara : MonoBehaviour
{
    public delegate void ParallaxCamaraDelegate(float deltaMovement);
    public ParallaxCamaraDelegate onCameraTranslate;

    private float oldPosition;

    void Start()
    {
        oldPosition = transform.position.x + transform.position.y;
    }

    void Update()
    {
        if (transform.position.x + transform.position.y != oldPosition)
        {
            if (onCameraTranslate != null)
            {
                float delta = oldPosition - (transform.position.x + transform.position.y);
                onCameraTranslate(delta);
            }

            oldPosition = transform.position.x + transform.position.y;
        }
    }
}
