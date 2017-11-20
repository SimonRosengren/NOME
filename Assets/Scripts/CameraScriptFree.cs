using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScriptFree : MonoBehaviour
{

    Vector3 offset;
    Transform target;

    public GameObject Target;
    public Vector3 cameraOffset = new Vector3(0, 0, 0);
    public float cameraSpeed = 10f;
    public bool lookLock;
    private Camera camera;

    private RaycastHit hit;
    private Vector3 newCameraPos;
    float distance = 5.0f;
    float distanceOffset;

    // Use this for initialization
    void Start()
    {
        camera = GetComponent<Camera>();
        lookLock = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (camera != null && Target != null)
        {
            target = Target.transform;
            offset = cameraOffset;

            float cameraAngle = camera.transform.eulerAngles.y;
            float targetAngle = Target.transform.eulerAngles.y;

            if (Input.GetAxisRaw("Vertical") < 0.2f)
            {
                targetAngle = cameraAngle;
            }

            targetAngle = Mathf.LerpAngle(cameraAngle, targetAngle, cameraSpeed * Time.deltaTime);
            offset = Quaternion.Euler(0, targetAngle, 0) * offset;

            //New Code
            if (Physics.Raycast(target.transform.position, offset, out hit, distance + 0.5f) && hit.collider.gameObject.tag == "wall")
            {
                Debug.Log("hit");
                //Debug.DrawLine(target.transform.position, hit.point, Color.red);
                distanceOffset = distance - hit.distance + 0.5f;
                distanceOffset = Mathf.Clamp(distanceOffset, 0, distance);

                newCameraPos = new Vector3(0, 0, -distanceOffset);
                newCameraPos = Quaternion.Euler(0, targetAngle, 0) * newCameraPos;
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


            camera.transform.LookAt(target.position);
            Debug.DrawLine(target.position, camera.transform.position, Color.blue);
        }
    }

    public void InstantFocusCamera()
    {
        camera.transform.position = target.position - target.forward * 10;
    }
}
