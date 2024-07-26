using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    private Camera mainCamera;   // Reference to the main camera
    public UnityEvent<CharacterOrderData> onPlayerOrder;
    public Character selectedCharacter = null;

    void Start()
    {
        mainCamera = Camera.main;   // Assign the main camera at the start
        //selectedCharacter
    }

    void Update()
    {
        // Check if the player clicks the mouse button
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse position into the game world
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits something in the game world
            if (Physics.Raycast(ray, out hit) && selectedCharacter)
            {
                var order = new CharacterOrderData("move", selectedCharacter.name, new CharacterTargetData("position", JsonUtility.ToJson(hit.point)));
                onPlayerOrder.Invoke(order);
                // Get the hit point and set it as the target position
                //selectedCharacter.SendMessage("GoTo", hit.point); //onPlayerOrder.Invoke(.name, );
            }
        }
    }
}
