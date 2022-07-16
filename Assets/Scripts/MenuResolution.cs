using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cozunurlukAyarla : MonoBehaviour
{
    int x;
    int y;

    // Start is called before the first frame update
    void Start()
    {
        //Ayarla();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            if (Screen.fullScreenMode == FullScreenMode.FullScreenWindow)
            {
                Screen.SetResolution(1024, 576, FullScreenMode.Windowed);
            }
            else
            {
                Ayarla();
            }
            
        }
    }

    void Ayarla()
    {
        Screen.SetResolution(1024, 576, FullScreenMode.FullScreenWindow);
        
        if (Screen.width/16 > Screen.height/9)
        {
            Screen.SetResolution(Screen.currentResolution.height/9*16, Screen.currentResolution.height, FullScreenMode.FullScreenWindow);
        }
        else
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.width/16*9, FullScreenMode.FullScreenWindow);
        }
    }

}