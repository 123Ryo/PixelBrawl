using UnityEngine;
using UnityEngine.EventSystems;

public class AttackJoystick : FixedJoystick, IPointerDownHandler, IPointerUpHandler
{
    public AttackController attackController;

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData); // 保留拖動功能
        if (attackController != null)
        {
            attackController.TriggerPointerDown();
        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData); // 保留釋放功能
        if (attackController != null)
        {
            attackController.TriggerPointerUp();
        }
    }
}
