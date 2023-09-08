using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLOpener : MonoBehaviour
{
    [SerializeField] string fL, lL, gL;

    public void openfl()
    {
        Application.OpenURL(fL);
    }

    public void openll()
    {
        Application.OpenURL(lL);
    }

    public void opengl()
    {
        Application.OpenURL(gL);
    }

}
