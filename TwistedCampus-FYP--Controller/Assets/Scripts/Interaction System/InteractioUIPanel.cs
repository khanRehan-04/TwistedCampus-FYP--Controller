using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractioUIPanel : MonoBehaviour
{
    [SerializeField] private Image progressBar;
    [SerializeField] private TextMeshProUGUI toolTipText;

    private string emptyString = "";

    public void SetToolTip(string toolTip)
    {
        toolTipText.SetText(toolTip);
    }

    public void UpdateProgressBar(float fillAmount)
    {
        progressBar.fillAmount = fillAmount;
    }

    public void ResetUI()
    {
        progressBar.fillAmount = 0f;
        toolTipText.SetText(emptyString);
    }
}
