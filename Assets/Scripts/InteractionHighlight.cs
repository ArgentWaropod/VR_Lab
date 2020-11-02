using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractionHighlight : MonoBehaviour
{

    public Material mat_Hover = null;
    public Material mat_Highlight = null;

    private Material mat_Original = null;
    private MeshRenderer meshRenderer = null;
    private XRGrabInteractable grabObject = null;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        mat_Original = meshRenderer.material;
        grabObject = GetComponent<XRGrabInteractable>();

        grabObject.onHoverEnter.AddListener(SetHoverMaterial);
        grabObject.onHoverExit.AddListener(SetOriginalMaterial);
        grabObject.onHoverEnter.AddListener(SetHighlightMaterial);
    }

    private void SetHighlightMaterial(XRBaseInteractor arg0)
    {
        meshRenderer.material = mat_Highlight;
    }

    private void SetOriginalMaterial(XRBaseInteractor arg0)
    {
        meshRenderer.material = mat_Original;
    }

    private void SetHoverMaterial(XRBaseInteractor arg0)
    {
        meshRenderer.material = mat_Hover;
    }

}