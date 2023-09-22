using UnityEngine;

public class TwistInput : PointerInput
{
    readonly Vector2 m_ScreenCenter;
    private Vector3 _initialPoint;
    private Vector2 _initialTouchPosition;
    private Quaternion _targetRotation;

    private float m_angleDifference = 0f;
    private float distanceFactor;
    public float AngleDifference => m_angleDifference;
    public float DistanceFactor => distanceFactor;

    public TwistInput()
    {
        OnPointerDown += PointerDown;
        OnPointerUpdate += PointerUpdate;
        OnPointerUp += PointerUp;
        m_ScreenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
    }


    void PointerDown(Vector3 mousePosition)
    {
        _initialTouchPosition = Input.mousePosition;
    }

    void PointerUpdate(Vector3 mousePosition)
    {
        // _initialTouchPosition = Input.mousePosition;

        // Get current touch position and calculate direction from center
        Vector2 currentTouchPosition = Input.mousePosition;
        Vector2 directionFromCenter = currentTouchPosition - m_ScreenCenter;
        Vector2 previousDirectionFromCenter = _initialTouchPosition - m_ScreenCenter;

        // Calculate angle difference between previous and current direction
        m_angleDifference = Vector2.SignedAngle(previousDirectionFromCenter, directionFromCenter);

        distanceFactor = directionFromCenter.magnitude / m_ScreenCenter.magnitude;

        _initialTouchPosition = currentTouchPosition;
    }

    void PointerUp(Vector3 mousePosition)
    {
        _initialTouchPosition = Vector2.zero;
        m_angleDifference = 0;
    }


    public override void Calculate()
    {
        base.Calculate();
    }
}