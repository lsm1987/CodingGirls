using UnityEngine;
using System;
using System.Reflection;
using System.Collections;

public class MonoTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MonoInfo();
        }
    }

    /*
     * C:\Program Files (x86)\Unity\Editor\Data\Mono\bin>monop2 --runtime-version mono.exe
     * runtime version: 2.0.50727.1433
     */
    private void MonoInfo()
    {
        Type type = Type.GetType("Mono.Runtime");
        if (type != null)
        {
            MethodInfo displayName = type.GetMethod("GetDisplayName", BindingFlags.NonPublic | BindingFlags.Static);
            if (displayName != null)
            {
                Debug.Log(displayName.Invoke(null, null));
            }
        }
    }
}
