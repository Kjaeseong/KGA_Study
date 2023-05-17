using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class Initializer : MonoBehaviour
{
    // 1. 권한 요청(위치정보)
    // 1.2. 요청이 승인됐을 때는 Gps 구동시키고 씬 전환
    [SerializeField] private float _delayTime;
    private WaitForSeconds _delay;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _delay = new WaitForSeconds(_delayTime);

        GameManager.Instance.Permis.Request(
            Permission.FineLocation,
            () => StartCoroutine(InitCoroutine())
        );
    }

    private IEnumerator InitCoroutine()
    {
        GameManager.Instance.Gps.GpsOn();
        yield return _delay;

        GameManager.Instance.Scene.Load(SceneType.MainTitle);
    }
}
