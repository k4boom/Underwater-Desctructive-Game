using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { set; get; } 
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    private Dictionary<int, Vector2> activeTouches = new Dictionary<int, Vector2>();

    public Vector3 GetPlayerInput()
    {
        Vector3 r = Vector3.zero;
        foreach(Touch touch in Input.touches)
        {
            if(touch.phase == TouchPhase.Began)
            {
                activeTouches.Add(touch.fingerId, touch.position);
            } else if(touch.phase == TouchPhase.Ended)
            {
                if(activeTouches.ContainsKey(touch.fingerId))
                {
                    activeTouches.Remove(touch.fingerId);
                }
            } else
            {
                float mag = 0;
                r = (touch.position - activeTouches[touch.fingerId]);
                mag = r.magnitude / 300;
                r = r.normalized * mag;
            }
        }
        return r;
    }
}
