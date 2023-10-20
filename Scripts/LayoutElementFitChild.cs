using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LayoutElement))]
[ExecuteInEditMode]
public class LayoutElementFitChild : MonoBehaviour
{
    public void Fit()
    {
        RectTransform firstChild = transform.GetChild(0).GetComponent<RectTransform>();
        LayoutElement layoutElement = GetComponent<LayoutElement>();
        layoutElement.preferredHeight = firstChild.rect.height;
    }

    // adds a button to the inspector to call the Fit method
    [ContextMenu("Fit")]
    public void FitButton()
    {
        Fit();
    }

    private void LateUpdate()
    {
        Fit();
    }
}
