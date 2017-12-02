using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScriptFree : MonoBehaviour
{

    public GameObject Target;
    public Vector3 cameraOffset = new Vector3(0, 0, 0);
    public float cameraSpeed = 10f;
    float targetAngleH;
    float targetAngleV;
    public bool lookLock;
    public bool controlCamera;
    private Camera camera;

    private RaycastHit hit;
    private Vector3 newCameraPos;
    private Vector3 offset;

    float distanceOffset;

    public float distance;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float resetCameraTimer;

    Quaternion rotation;
    // Use this for initialization
    void Start()
    {
        camera = GetComponent<Camera>();
        lookLock = false;
        controlCamera = false;
        resetCameraTimer = 2;

    }

    // Update is called once per frame
    private void Update()
    {

    }
    void FixedUpdate()
    {
        if (camera != null && Target != null)
        {


            //Debug.Log(cameraOffset);
            Transform target = Target.transform;
            offset = cameraOffset;


            float cameraAngle = camera.transform.eulerAngles.y;
            targetAngleH = Target.transform.eulerAngles.y;
            //targetAngleV = Target.transform.eulerAngles.x;

            if (Input.GetAxisRaw("Vertical") < 0.2f)
            {
                targetAngleH = cameraAngle;
            }

            targetAngleH = Mathf.LerpAngle(cameraAngle, targetAngleH, cameraSpeed * Time.deltaTime);

            targetAngleH += Input.GetAxis("RightJoyH") * Time.deltaTime * 700;
            targetAngleV += Input.GetAxis("RightJoyV") * Time.deltaTime * 70;

            if (targetAngleV > 60)
            {
                targetAngleV = 60;
            }

            if (targetAngleV < -95)
            {
                targetAngleV = -95;
            }

            offset = Quaternion.Euler(targetAngleV, targetAngleH, 0) * offset;

            //New Code
            if (Physics.Raycast(target.transform.position, offset, out hit, distance + 0.5f) && hit.collider.gameObject.tag == "wall")
            {
                Debug.DrawLine(target.transform.position, hit.point, Color.yellow);
                //Debug.Log("hit");
                distanceOffset = distance - hit.distance + 0.5f;
                distanceOffset = Mathf.Clamp(distanceOffset, 0, distance);

                newCameraPos = new Vector3(0, 0, -distanceOffset);
                newCameraPos = Quaternion.Euler(targetAngleV, targetAngleH, 0) * newCameraPos;
            }
            else
            {
                distanceOffset = 0.5f;
                newCameraPos = Vector3.zero;

            }

            if (Input.GetKey(KeyCode.J) || Input.GetButton("CameraFocus"))
            {
                //camera.transform.position = Vector3.Lerp(camera.transform.position, target.position - target.forward + offset, cameraSpeed * 10 * Time.deltaTime);
                camera.transform.position = Vector3.Lerp(camera.transform.position, target.position - target.forward - newCameraPos + offset, cameraSpeed * 10 * Time.deltaTime);

            }
            else
            {
                //camera.transform.position = Vector3.Lerp(camera.transform.position, target.position + offset, cameraSpeed * Time.deltaTime);
                camera.transform.position = Vector3.Lerp(camera.transform.position, target.position - newCameraPos + offset, cameraSpeed * Time.deltaTime);

            }

            //if (Input.GetAxis("RightJoyH") >0||Input.GetAxis("RightJoyV") >0)
            //{
            //    controlCamera = true;
            //    resetCameraTimer = 2;
            //    currentX = 0;
            //    currentY = 0;
            //}
            camera.transform.LookAt(target.position);
            //Debug.Log(camera.transform.position - Target.transform.position);

        }
    }
}
