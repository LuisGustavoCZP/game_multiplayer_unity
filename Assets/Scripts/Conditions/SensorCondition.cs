using UnityEngine;

[CreateAssetMenu(fileName = "NewSensorCondition", menuName = "Conditions/SensorCondition")]
public class SensorCondition : Condition
{
    public float angle;
    public float distance;

    public override bool Check(CharacterBehaviour behaviour)
    {
        var sensor = behaviour.GetComponent<CharacterSensor>();

        if(sensor.senses.Count == 0) return false;

        foreach (var sense in sensor.senses)
        {
            if(
                (distance == 0 || sense.distance < distance)
                && 
                (angle == 0 || sense.angle < angle)
            )
            {
                return true;
            }
        }
        
        return false;
    }
}