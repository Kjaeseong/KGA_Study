using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Util.SingletonPattern;

public class GameManager : Singleton<GameManager>
{
    public SceneChanger Scene { get; private set; }
    public PermissionChecker Permis { get; private set; }
    public GpsManager Gps { get; private set; }

    private void Awake() 
    {
        base.SingletonInit();
        Init();
    }

    private void Init()
    {
        Scene = GetComponentInChildren<SceneChanger>();
        Permis = GetComponentInChildren<PermissionChecker>();
        Gps = GetComponentInChildren<GpsManager>();
    }
}
