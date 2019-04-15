using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneUtils {
    public static void Reset() {
        Application.LoadLevel(Application.loadedLevelName);
    }
}