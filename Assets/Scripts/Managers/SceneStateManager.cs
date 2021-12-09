using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneStateManager
{
    public static void NextScene(SceneNameType nextSceneName) {
        SceneManager.LoadScene(nextSceneName.ToString());
    }
}
