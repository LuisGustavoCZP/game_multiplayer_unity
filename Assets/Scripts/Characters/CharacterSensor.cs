using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class CharacterSensor : MonoBehaviour
{
    [HideInInspector]
    public SensorManager manager;

    public List<SensorData> sensorDatas = new List<SensorData>();
    public List<SensorSense> senses = new List<SensorSense>();
    HashSet<string> sensesSet = new HashSet<string>();
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Sensor Manager").GetComponent<SensorManager>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CheckVision();
    }

    void CheckVision ()
    {
        sensesSet.Clear();
        senses.Clear();
        foreach (var sensorData in sensorDatas)
        {
            var sensibility = sensorData.sensibility;
            var sensor = sensorData.sensor;
            var sensorSenses = sensor.Execute(this, sensibility);
            foreach (var sense in sensorSenses)
            {
                var senseName = sense.target.name;
                if(sensesSet.Contains(senseName)) continue;
                sensesSet.Add(senseName);
                senses.Add(sense);
            }
        }
    }

    public int detail = 1;
    public Color senseColor = new Color(1f, 1f, 1f, .3f);
    void OnDrawGizmos ()
    {
        var bkpColor = Gizmos.color;

        var point = transform.position;

        var pointCenter = point + Vector3.up;
        foreach (var sensorData in sensorDatas)
        {
            var sensor = sensorData.sensor;
            var segments = Mathf.Max(Mathf.CeilToInt(sensor.segments*detail), 1);
            DrawWireCircle(
                pointCenter,
                sensor.radius,
                segments,
                sensor.color
            );
        }

        DrawSenses(point);
        
        Gizmos.color = bkpColor;
    }

    void DrawSenses(Vector3 center)
    {
        var bkpColor = Gizmos.color;
        Gizmos.color = senseColor;

        foreach (var sense in senses)
        {
            //var go = GameObject.Find(target.name);
            var dir = Vector3.Normalize(center + Vector3.up - sense.target.position) * .3f;
            Gizmos.DrawLine(center + Vector3.up - dir, sense.target.position + Vector3.up + dir);
        }

        Gizmos.color = bkpColor;
    }

    void DrawWireCircle(Vector3 center, float radius, int segments, Color color)
    {
        var bkpColor = Gizmos.color;
        Gizmos.color = color;
        float angle = 2 * Mathf.PI / segments;
        Vector3 startPoint = center + new Vector3(radius, 0, 0);
        Vector3 previousPoint = startPoint;

        for (int i = 1; i <= segments; i++)
        {
            float x = radius * Mathf.Cos(angle * i);
            float z = radius * Mathf.Sin(angle * i);
            Vector3 nextPoint = center + new Vector3(x, 0, z);
            Gizmos.DrawLine(previousPoint, nextPoint);
            previousPoint = nextPoint;
        }

        // Connect the last segment with the first point to complete the circle
        Gizmos.DrawLine(previousPoint, startPoint);
        
        Gizmos.color = bkpColor;
    }
}
