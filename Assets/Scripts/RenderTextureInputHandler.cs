using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RenderTextureInputHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Camera mainCamera;
    [SerializeField] GraphicRaycaster targetUIRaycaster;
    Vector3 cameraExitSize;
    EventSystem eventSystem;
    Canvas thisCanvas;

    private void Start()
    {
        eventSystem = GetComponent<EventSystem>();
        thisCanvas = GetComponent<Canvas>();

        cameraExitSize = new Vector3(mainCamera.targetTexture.width, mainCamera.targetTexture.height, 0);
        Debug.Log(cameraExitSize);
    }

    public void OnPointerClick(PointerEventData clickData)
    {
        RaycastResult clickResult = clickData.pointerPressRaycast;

        Debug.Log(clickResult.gameObject);
        
        if(clickResult.gameObject) 
        {
            Vector3 pointerPositionScaled = ScreenSpaceScaledVector3(clickData);
            Debug.Log(pointerPositionScaled);

            PointerEventData portalEventData = new PointerEventData(eventSystem);

            portalEventData.position = pointerPositionScaled;

            var results = new List<RaycastResult>();

            targetUIRaycaster.Raycast(portalEventData, results);

            foreach (RaycastResult result in results)
            {
                var resultButton = result.gameObject.GetComponent<Button>();

                if (resultButton) resultButton.OnPointerClick(portalEventData);
            }
        }
    }

    private Vector3 ScreenSpaceScaledVector3(PointerEventData clickData)
    {
        //get size of the Clickable Canvas in pixels
        Vector2 currentScreenSize = thisCanvas.pixelRect.size;

        //divide pointer pos x and y by screen size to get screen percentage result
        float clickPercentageX = clickData.position.x / currentScreenSize.x;
        float clickPercentageY = clickData.position.y / currentScreenSize.y;

        //multiply percentage result by target screen size x and y to get scaled vector3
        float scaledXPos = clickPercentageX * cameraExitSize.x;
        float scaledYPos = clickPercentageY * cameraExitSize.y;

        //combine into new Vector 3 and return
        Vector3 pointerPositionInCameraSpace = new Vector3(scaledXPos, scaledYPos, 0);
        return pointerPositionInCameraSpace;
    }

}

