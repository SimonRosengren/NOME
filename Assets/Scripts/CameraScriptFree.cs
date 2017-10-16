﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScriptFree : MonoBehaviour {

    public GameObject Target;
    public Vector3 cameraOffset = new Vector3(0,10,-20);
    public float cameraSpeed=10f;

    private Camera camera;

	// Use this for initialization
	void Start () {
        camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(camera != null && Target != null)
        {
            Vector3 targetPos = Target.transform.position;
            Vector3 offset = cameraOffset;

            float cameraAngle = camera.transform.eulerAngles.y;
            float targetAngle = Target.transform.eulerAngles.y;

            if (Input.GetAxisRaw("Vertical") < 0.2f)
            {
                targetAngle = cameraAngle;
            }

            targetAngle = Mathf.LerpAngle(cameraAngle, targetAngle, cameraSpeed * Time.deltaTime);
            offset = Quaternion.Euler(0, targetAngle, 0) * offset;


            camera.transform.position = Vector3.Lerp(camera.transform.position, targetPos + offset, cameraSpeed * Time.deltaTime);
            camera.transform.LookAt(targetPos);
        }
	}
}
