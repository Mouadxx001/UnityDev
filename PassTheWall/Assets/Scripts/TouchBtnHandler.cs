using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

enum Direction
{
    Left,
    Right,
    None
};

public class TouchBtnHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Direction btnDirection = Direction.None;

    public void OnPointerDown(PointerEventData eventData)
    {
        if(btnDirection == Direction.Left)
        {
            PlayerController.direction = Direction.Left;
        }
        else if(btnDirection == Direction.Right)
        {
            PlayerController.direction = Direction.Right;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (btnDirection == Direction.Left)
        {
            PlayerController.direction = Direction.None;
        }
        else if(btnDirection == Direction.Right)
        {
            PlayerController.direction = Direction.None;
        }
    }
}
