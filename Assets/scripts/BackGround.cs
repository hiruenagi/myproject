using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    // Start is called before the first frame update
    public float parallaxFactor = 0.1f;
    public float framesParallaxFactor = 0.3f;
    public float smoothX = 4;
    public Transform[] background;

    private Transform cam;
    private Vector3 camPrePos;

    private void Awake()
    {
        cam = Camera.main.transform;
        camPrePos = cam.position;
    }
    void Start()
    {
        
    }

    void bkParallax()
    {
        float fparallax = (camPrePos.x - cam.position.x)*parallaxFactor;
        for(int i = 0; i < background.Length; i++)
        {
            float bkNewX = background[i].position.x + fparallax * (1 + i * framesParallaxFactor);
            Vector3 bkNewPos = new Vector3(bkNewX, background[i].position.y, background[i].position.z);
            background[i].position = Vector3.Lerp(background[i].position, bkNewPos, Time.deltaTime * smoothX);
        }
        camPrePos = cam.position;
    }
    // Update is called once per frame
    void Update()
    {
        bkParallax();
    }
}
