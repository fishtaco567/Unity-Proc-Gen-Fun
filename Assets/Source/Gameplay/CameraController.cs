using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public float speed = 5;
    public float turnSpeed = 60;

    private float yRot;
    private float xRot;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float vert = Input.GetAxis("Vertical");
        float horiz = Input.GetAxis("Horizontal");
        float cHoriz = Input.GetAxis("CHorizontal");
        float cVert = -Input.GetAxis("CVertical");

        yRot += cHoriz * turnSpeed * Time.deltaTime;
        xRot += cVert * turnSpeed * Time.deltaTime;

        xRot = Mathf.Clamp(xRot, -50, 50);

        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        transform.Translate(vert * transform.forward * speed * Time.deltaTime, Space.World);
        transform.Translate(horiz * transform.right * speed * Time.deltaTime, Space.World);
    }
}
