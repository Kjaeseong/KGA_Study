using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using System;

public class PermissionChecker : MonoBehaviour
{
    public void Request(string targetPermission, Action grantedAction)
    {
        // 입력받은 종류의 권한이 승인이 이미 되어 있다면
        if(Permission.HasUserAuthorizedPermission(targetPermission))
        {
            grantedAction();
        }
        else // 승인 되어 있지 않다면
        {
            // 권한의 처리(승인/거절/아예 거절)시 반응(콜백)에 대한 정보를 담고있는 클래스
            var pCallback = new PermissionCallbacks();

            // 승인시 grantedAction 액션을 호출하겠다.
            pCallback.PermissionGranted += _ => grantedAction();    
            
            // Permission 구조체에서 권한요청 함수 호출, (대상 권한, 콜백)
            Permission.RequestUserPermission(targetPermission, pCallback);
        }
            // 각 상태에 따라 체이닝으로 동작을 여러개 등록할 수 있음
            // pCallback.PermissionGranted +=                       승인시
            // pCallback.PermissionDenied +=                        1회 거절시
            // pCallback.PermissionDeniedAndDontAskAgain +=         아예 거절(사용자가 수동으로 권한 승인해야함)
    }
}
