
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class AudioUtils {

    private static float twelfthRootOfTwo = Mathf.Pow(2, 1.0f / 12);

    public static float St2pitch(float st)

    {

            return Mathf.Clamp(Mathf.Pow(twelfthRootOfTwo, st), 0f, 4f);

    }

}