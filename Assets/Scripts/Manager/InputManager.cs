using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
public class InputManager : MonoBehaviour
{
    public float minPixelSwipe;
    Vector2 initialRaycastPos;
    bool isMoved = false;
    GameManager gm;

    void Awake()
    {
        gm = FindAnyObjectByType<GameManager>();
    }

    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }
    void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    void Update()
    {
        if (Touch.activeTouches.Count <= 0) return;

        Touch touch = Touch.activeTouches[0];

        if (touch.phase == TouchPhase.Began)
        {
            initialRaycastPos = touch.screenPosition;
            isMoved = false;
        }

        if (touch.phase == TouchPhase.Moved && !isMoved)
        {
            Vector2 swipeDelta = touch.delta;
            if (swipeDelta.magnitude > minPixelSwipe)
            {
                Debug.Log("swipe rilevato oltre la soglia");
                FliptTheSlice(swipeDelta);
                isMoved = true;
            }
        }
    }

    void FliptTheSlice(Vector2 delta)
    {
        Vector2Int gridDir = Vector2Int.zero;
        if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y)) { gridDir.x = delta.x > 0 ? 1 : -1; }
        else { gridDir.y = delta.y > 0 ? 1 : -1; }

        Ray ray = Camera.main.ScreenPointToRay(initialRaycastPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Food hitSlice = hit.collider.GetComponentInParent<Food>();
            if (hitSlice != null)
                gm.TryMove(hitSlice.gridPosition, gridDir);
        }
        else return;
    }
}