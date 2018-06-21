using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour 
{ 
    public float lerpRate = 2f;
    public Transform player;
    private Vector3 pos;
    private Camera camera;
    private void Start()
    {
        pos = transform.position;
        camera = GetComponent<Camera>();
    }
    private void LateUpdate()
    {
        transform.position = pos;
    }
    void FixedUpdate()
    {
        pos.x = Mathf.Lerp(
            transform.position.x,
            player.position.x,
            Time.deltaTime * lerpRate
        );
        pos.y = Mathf.Clamp(Mathf.Lerp(
            transform.position.y,
            player.position.y - 0.5f,
            Time.deltaTime * lerpRate),
            camera.orthographicSize - 0.5f ,
            float.MaxValue);
    }
}