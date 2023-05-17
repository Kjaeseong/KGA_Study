using System.Collections;
using UnityEngine;

public struct EarthPos
{
    public double Lat { get; set; }
    public double Lng { get; set; }
}

public class GpsManager : MonoBehaviour
{
    private EarthPos _pos = new EarthPos();
    public EarthPos Pos { get => _pos; }
    public int RefreshCount { get; private set; }
    private LocationInfo _location;

    [SerializeField] private float _coroutineCycle;
    private WaitForSeconds _cycle;
    private Coroutine _coroutine;

    private void Awake()
    {
        Init();
    }

    public void Init() 
    {
        _coroutine = null;
        _cycle = new WaitForSeconds(_coroutineCycle);
    }

    public void GpsOn()
    {
        if(_coroutine == null)
        {
            _coroutine = StartCoroutine(GpsCoroutine());
        }
        else
        {
            StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(GpsCoroutine());
        }
    }

/*
Gps를 구동시키는 함수의 흐름

1. 디바이스가 Gps를 사용중이지 않다면 멈춰야 함
2. Gps 서비스 시작
3. 만약 초기화 단계에서 멈추고 동작하지 않는 상태라면 대기해줘야 함
4. Gps상태가 '실패'일 경우 동작을 멈춰야함
5. 위 단계들을 다 통과하면(정상구동) 일정 주기마다 좌표 갱신해주면 됨
*/

    private IEnumerator GpsCoroutine()
    {
        // GPS 기능이 사용중이지 않다면 멈춤
        if (!Input.location.isEnabledByUser)
        {
            yield break;
        }

        // GPS 서비스 시작
        Input.location.Start();

        int waitCount = 20;
        // 위치 서비스의 상태가 초기화 단계이고(Running 상태면 실행하지 않는다) &&
        // 대기 카운트가 남아있다면
        // 즉, 초기화 단계에서 안 넘어갈 경우
        while (Input.location.status == LocationServiceStatus.Initializing && waitCount > 0)
        {
            // 대기 후 카운트 1회 차감
            yield return _cycle;
            waitCount -= 1;
        }

        // 대기 카운트가 남아있지 않다면 멈춤
        if (waitCount < 1)
        {
            yield break;
        }

        // 위치서비스 상태가 실패일 경우 멈춤
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            yield break;
        }
        else // 실패가 아니라면
        {
            while (true)
            {
                // 현재의 위치는 마지막으로 갱신된 데이터로 한다. 
                _location = Input.location.lastData;

                // 위도, 경도를 _location 변수에서 받아온다
                _pos.Lat = _location.latitude * 1.0d;
                _pos.Lng = _location.longitude * 1.0d;

                RefreshCount++;
                
                // 지정한 시간 단위로 반복
                yield return _cycle;
            }
        }
    }
}