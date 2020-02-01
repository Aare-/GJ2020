using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RoomManager))]
public class PositionHoldManager : MonoBehaviour {

    public struct FrameData {
        public Quaternion LevelRotation;

        public List<Quaternion> BlockingObjectsRotation;

        public List<Vector3> BlockingObjectsPosition;
    }    
    
    private List<FrameData> _CapturedData = new List<FrameData>();

    [Header("Config")]
    [SerializeField]
    [Range(1, 8)]
    protected int _CacheSize;
    
    protected RoomManager _Manager;
    
    protected List<StaticObjectBehaviour> _StaticObjects = new List<StaticObjectBehaviour>();

    protected void Awake() {
        _Manager = GetComponent <RoomManager>();
    }
    
    public void RegisterStaticObject(StaticObjectBehaviour obj) {
        Debug.Log("REGISTER!");
        
        if (_StaticObjects.Contains(obj))
            return;
        
        _StaticObjects.Add(obj);
    }

    public void ResetPositions() {
        var resetData = _CapturedData[0];
        _CapturedData.Clear();
        _CapturedData.Add(resetData);

        _Manager.transform.rotation = resetData.LevelRotation;
        _Manager.Body.angularVelocity = Vector3.zero;
        _Manager.Rotation = 0.0f;

        var i = 0;
        _StaticObjects.ForEach(x => {
            var rPos = resetData.BlockingObjectsPosition[i];
            
            x.transform.position =
                new Vector3(rPos.x, x.transform.position.y, rPos.z);
            x.transform.rotation = 
                resetData.BlockingObjectsRotation[i];
            
            x.Body.velocity = new Vector3(0, 0.0f, 0);
            x.Body.angularVelocity = Vector3.zero;

            i++;
        });
    }
    
    protected void LateUpdate() {
        RecordData(new FrameData() {
            LevelRotation = _Manager.transform.rotation,
            
            BlockingObjectsRotation = _StaticObjects.ConvertAll(x => x.transform.rotation),
            BlockingObjectsPosition = _StaticObjects.ConvertAll(x => x.transform.position),
        });
    }

    private  void RecordData( FrameData data) {
        if(_CapturedData.Count > _CacheSize)
            _CapturedData.RemoveAt(0);
        
        _CapturedData.Add(data);
    }
}
