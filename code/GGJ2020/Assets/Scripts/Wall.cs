using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Wall : MonoBehaviour {
    [SerializeField] protected List<Vector2> _AngleRanges = new List<Vector2>();

    private MeshRenderer _Mesh;

    protected void Awake() {
        _Mesh = GetComponent<MeshRenderer>();
    }

    protected void Update() {
        if (Camera.main == null) return;
        
        var cameraRotationY = Camera.main.transform.rotation.eulerAngles.y;

        while (cameraRotationY < 0)
            cameraRotationY += 360;
        cameraRotationY %= 360;

        _Mesh.enabled = _AngleRanges.Exists(x => cameraRotationY >= x.x && cameraRotationY < x.y);
    }
}
