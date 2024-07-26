using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "NewSensor", menuName = "Sensors/Create")]
public class Sensor : ScriptableObject
{
    public int segments = 20;
    public Color color = new Color(1f, 1f, 1f, .3f);
    public float radius = 20;
    public float angle = 360;
    public LayerMask layers;

    public virtual SensorSense[] Execute(CharacterSensor sensor, float sensibility)
    {
        var sensorTransform = sensor.transform;
        var sensorPosition = sensorTransform.position;
        var sensorName = sensor.gameObject.name;
        var sensorSenses = new List<SensorSense>();

        foreach (var target in sensor.manager.targets)
        {
            if(target.name == sensorName) continue;

            var positionDiff = target.position - sensorPosition;
            var distance = Vector3.Magnitude(positionDiff);
            if(distance > radius*sensibility) continue;

            var direction = Vector3.Normalize(positionDiff);
            var colliderAngle = Vector3.SignedAngle(sensorTransform.forward, direction, Vector3.up);
            if(Mathf.Abs(colliderAngle) > angle) continue;

            var sensorSense = new SensorSense()
            {
                distance = distance,
                angle = colliderAngle,
                target = target,
            };
            sensorSenses.Add(sensorSense);
        }

        sensorSenses.OrderBy((key) => key.angle + key.distance);

        return sensorSenses.ToArray();
    }
}