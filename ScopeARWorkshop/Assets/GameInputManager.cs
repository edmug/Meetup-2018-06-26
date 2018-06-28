using HoloToolkit.Unity.InputModule;
using System;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

interface IInputSource
{
    event Action<Ray> OnTap;
}

// When running a holographic project in the Unity editor, tap events are not triggered unless
// you have an xbox controller. This is a little workaround to use the "enter" key to trigger a
// tap event.
class EditorKeyPressSource : MonoBehaviour, IInputSource
{
    const KeyCode EditorKeyCode = KeyCode.Return;

    public event Action<Ray> OnTap;

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(EditorKeyCode))
        {
            OnTap(GetCameraRayToCenterOfScreen());
        }
#endif
    }

    private Ray GetCameraRayToCenterOfScreen()
    {
        var screenCenterPoint = new Vector3(Screen.width / 2, Screen.height / 2);
        return Camera.main.ScreenPointToRay(screenCenterPoint);
    }
}

// Input source powered by the Mixed Reality Toolkit's GestureRecognizer.
class GestureRecognizerSource : MonoBehaviour, IInputSource
{
    public event Action<Ray> OnTap;

    private void Start()
    {
        // TODO: Set up the Mixed Reality Toolkit's GestureRecognizer to raise Tap events
    }
}

public class GameInputManager : MonoBehaviour
{
    Type[] inputSourceTypes = new Type[]
    {
        typeof(EditorKeyPressSource),
        typeof(GestureRecognizerSource)
    };

    [SerializeField]
    private GameObject ProjectilePrefab;

    private void Start()
    {
        foreach (var sourceType in inputSourceTypes)
        {
            IInputSource inputSource = (IInputSource)gameObject.AddComponent(sourceType);
            inputSource.OnTap += InputSource_OnTap;
        }
    }

    private void InputSource_OnTap(Ray ray)
    {
        // TODO: Launch a basketball!
    }
}
