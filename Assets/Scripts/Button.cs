using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;

public class Button : XRBaseInteractable
{
    public UnityEvent onPress = null;

    float yMin = 0.0f;
    float yMax = 0.0f;
    bool previousPress = false;

    private float previousHeight = 0.0f;
    private XRBaseInteractor hoverInteract = null;
    protected override void Awake()
    {
        base.Awake();
        onHoverEnter.AddListener(StartPress);
        onHoverExit.AddListener(EndPress);
    }

    private void OnDestroy()
    {
        onHoverEnter.AddListener(StartPress);
        onHoverExit.AddListener(EndPress);
    }

    private void StartPress(XRBaseInteractor interactor)
    {
        hoverInteract = interactor;
        previousHeight = getYPos(hoverInteract.transform.position);
    }
    private void EndPress(XRBaseInteractor interactor)
    {
        hoverInteract = null;
        previousHeight = 0.0f;

        previousPress = false;
        setY(yMax);
    }
    private void Start()
    {
        minMax();
    }
    void minMax()
    {
        Collider collider = GetComponent<Collider>();
        yMin = transform.localPosition.y - (collider.bounds.size.y * 0.5f);
        yMax = transform.localPosition.y;
        
    }
    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        if(hoverInteract)
        {
            float newHandHeight = getYPos(hoverInteract.transform.position);
            float handDifference = previousHeight - newHandHeight;
            previousHeight = newHandHeight;

            float newPosition = transform.localPosition.y - handDifference;
            setY(newPosition);
            checkPress();
        }
    }

    private float getYPos(Vector3 posi)
    {
        Vector3 localPos = transform.root.InverseTransformPoint(posi);
        return localPos.y;
    }

    void setY(float pos)
    {
        Vector3 newPosition = transform.localPosition;
        newPosition.y = Mathf.Clamp(pos, yMin, yMax);
        transform.localPosition = newPosition;
    }

    void checkPress()
    {
        bool inPosition = inPos();
        if (inPosition && inPosition != previousPress)
        {
            Debug.Log("Pressed");
            onPress.Invoke();
        }

        previousPress = inPosition;
    }

    bool inPos()
    {
        float inRange = Mathf.Clamp(transform.localPosition.y, yMin, yMin + 0.01f);
        return transform.localPosition.y == inRange;
    }

}
