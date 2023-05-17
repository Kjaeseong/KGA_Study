using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    PermissionCheck, MainTitle,
    End
}

public class SceneChanger : MonoBehaviour
{
    public void Load(SceneType type)
    {
        SceneManager.LoadScene((int)type);
    }
}
