using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public BehaviorNode rootNode;

    void Update()
    {
        if (rootNode != null)
        {
            var result = rootNode.Execute(this);
            if (result == NodeState.Failure)
            {
                Debug.Log("Behavior tree failed");
            }
            else if (result == NodeState.Success)
            {
                Debug.Log("Behavior tree succeeded");
            }
        }
    }
}