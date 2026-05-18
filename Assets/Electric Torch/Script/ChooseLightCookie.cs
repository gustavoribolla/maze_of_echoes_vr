using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChooseLightCookie : MonoBehaviour
{
    public List<Texture> lightCookie = new List<Texture>();

    private Light _thisLight;
    private int _scroolList = 0;

    void Awake()
    {
        _thisLight = GetComponent<Light>();
    }

    void Update()
    {
        ChooseCookie();
    }

    void ChooseCookie()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            _scroolList += 1;

            if (_scroolList >= lightCookie.Count)
            {
                _scroolList = 0;
            }

            if (lightCookie.Count > 0)
            {
                _thisLight.cookie = lightCookie[_scroolList];
            }
        }
    }
}