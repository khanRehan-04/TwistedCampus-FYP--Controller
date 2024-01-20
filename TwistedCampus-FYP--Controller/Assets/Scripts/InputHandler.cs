using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [Header("Input Data")]
    public InteractionInputData interactionInputData;

    // Start is called before the first frame update
    void Start()
    {
        interactionInputData.ResetInput();
    }

    // Update is called once per frame
    void Update()
    {
        GetInteractionInputData();
    }

    void GetInteractionInputData()
    {
        interactionInputData.InteractedClicked = Input.GetKeyDown(KeyCode.E);
        Debug.Log("Cliked = " +  interactionInputData.InteractedClicked);
        interactionInputData.InteractedRelease = Input.GetKeyUp(KeyCode.E);
        Debug.Log("Released = " + interactionInputData.InteractedRelease);
    }
}
