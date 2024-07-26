using UnityEngine;

[System.Serializable]
public struct SensorTarget {
    public Vector3 forward;
    public Vector3 position;
    public Vector3 direction;
    public Vector3 velocity;
    public Vector3 acceleration;
    public float areaCost;
    public string name;
    public string type;
    public string[] tags;
}

[System.Serializable]
public struct SensorSense {
    public float angle;
    public float distance;
    public SensorTarget target;
}