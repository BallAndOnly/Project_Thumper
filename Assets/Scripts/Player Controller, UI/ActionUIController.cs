using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ActionUIController : MonoBehaviour
{
    Canvas canvas;
    public Camera playerCamera;
    public TMP_Text ActionText;
    public Transform HandIMG;
    public Image HandIMGImage;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        ShowCanvas(false);
    }

    public void ShowCanvas(bool shouldShowCanvas)
    {
        canvas.enabled = shouldShowCanvas;
    }

    public void ShowHand(bool handInteracting)
    {
        HandIMGImage.enabled = handInteracting;
    }

    public void HandPos(Vector3 objectPos)
    {
        HandIMG.transform.position = playerCamera.WorldToScreenPoint(objectPos);
    }

    public void ShowActionText(string interactionText)
    {
        ShowCanvas(true);
        ActionText.text = interactionText;
    }

}
