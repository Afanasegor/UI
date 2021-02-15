using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// Swiping class - to realize swipes :D
/// </summary>
public class Swiping : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public SnapScrolling movingPlatform;

    // checking touch position and moving...
    public void OnBeginDrag(PointerEventData eventData)
    {
        if ((Mathf.Abs(eventData.delta.x)) > (Mathf.Abs(eventData.delta.y)))
        {
            if (eventData.delta.x > 10)
            {
                movingPlatform.Before();
            }
            else if (eventData.delta.x < -10)
            {
                movingPlatform.Next();
            }
        }
    }

    // unnaccecery to realize now...
    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
