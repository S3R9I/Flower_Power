﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastMouse : MonoBehaviour {
    public Camera _camera;
    RaycastHit _hit;
    Ray _ray;
    Vector3 _mousePos, _smoothPoint;
    public float _radius, _softness, _smoothSpeed, _scaleFactor;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.UpArrow)) {
            _radius += _scaleFactor * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            _radius -= _scaleFactor * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            _softness += _scaleFactor * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow)) {
            _softness += _scaleFactor * Time.deltaTime;
        }
        Mathf.Clamp(_radius, 0, 100);
        Mathf.Clamp(_softness, 0, 100);

        _mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        _ray = _camera.ScreenPointToRay(_mousePos);

        if(Physics.Raycast(_ray, out _hit)) {
            _smoothPoint = Vector3.MoveTowards(_smoothPoint, _hit.point, _smoothSpeed * Time.deltaTime);
            Vector4 pos = new Vector4(-2.86f, -1.49f, 1.26f, 0);
            Shader.SetGlobalVector("GLOBALmask_Position", pos);
        }
        Shader.SetGlobalFloat("GLOBALmask_Radius", _radius);
        Shader.SetGlobalFloat("GLOBALmask_Softness", _softness);
    }
}
