using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

[Serializable]
struct Bounds
{
    public float leftOffset;
    public float rightOffset;
    public float upOffset;
    public float bottomOffset;
  
    
    //public float pixelsInUnit;
    [HideInInspector]
    public Vector2 center;
    public Bounds(Vector3 center,float leftOffset, float rightOffset, float upOffset, float bottomOffset)
    {
        this.leftOffset = leftOffset;
        this.rightOffset = rightOffset;
        this.upOffset = upOffset;
        this.bottomOffset = bottomOffset;
        this.center = center;
    }
}
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Bounds _bounds;
    private Ground _ground;
    private Camera _camera;
    private Vector2 _worldUnitsInCamera;
    private void Awake()
    {
        _ground = FindObjectOfType<Ground>();
        _camera = GetComponentInChildren<Camera>();
        _worldUnitsInCamera = new Vector2();
    }

    void Start()
    {
        if (_ground != null)
        {
           
            _worldUnitsInCamera.y = _camera.GetComponent<Camera>().orthographicSize * 2;
            _worldUnitsInCamera.x = _worldUnitsInCamera.y * Screen.width / Screen.height;
            _bounds.center = _ground.GroundCenter;
            transform.position = new Vector3(_bounds.center.x,0f,_bounds.center.y) ;
            //_bounds.size = new Vector3(0.6f * _ground.GroundWidth,0f, 0.6f * _ground.GroundHeight) ;

        }
        else
        {
            Debug.LogError("CAMERA FailToInitialize: Cannot find groundObject");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        var posOfCamera = transform.GetChild(0).position;
        
        var leftTop = transform.position+ transform.right * _bounds.leftOffset + transform.up * _bounds.upOffset;
        var rightTop = transform.position+ transform.right * _bounds.rightOffset + transform.up * _bounds.upOffset;
        var rightBottom = transform.position+ transform.right * _bounds.rightOffset + transform.up * _bounds.bottomOffset;
        var leftBottom = transform.position+ transform.right * _bounds.leftOffset + transform.up * _bounds.bottomOffset;
        Gizmos.DrawLine(leftBottom,leftTop);
        Gizmos.DrawLine(leftTop,rightTop);
        Gizmos.DrawLine(rightTop,rightBottom);
        Gizmos.DrawLine(rightBottom,leftBottom);
       
    }

    private Vector3 GetMouseBoxCoordinate()
    {
        Vector2 screenPos = Input.mousePosition;
        Vector2 centerOfBox = new Vector2( _camera.GetComponent<Camera>().orthographicSize,
            _camera.GetComponent<Camera>().orthographicSize*Screen.width / Screen.height);
        float pixelToUnit = _worldUnitsInCamera.x /Screen.currentResolution.width;
      
        Vector2 relPoint = (screenPos - centerOfBox);
        Debug.Log(_camera.GetComponent<Camera>().orthographicSize);
        Debug.LogWarning(_camera.pixelHeight); // x
        Debug.LogError(_camera.pixelWidth); // y
        float mouseBoxCoordX = Vector2.Dot(relPoint, _camera.transform.right);
        float mouseBoxCoordY = Vector2.Dot(relPoint, _camera.transform.up);
        return new Vector3(mouseBoxCoordX*pixelToUnit, mouseBoxCoordY*pixelToUnit, 0);
    }
    void Update()
    {
        //GetMouseBoxCoordinate();
            GetMouseBoxCoordinate();
    }
}
