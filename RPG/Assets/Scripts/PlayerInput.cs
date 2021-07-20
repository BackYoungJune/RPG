using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool MouseRightButton { get; private set; }      // 감지된 마우스 오른쪽 버튼
    public bool MouseLeftButton { get; private set; }       // 감지된 마우스 왼쪽 버튼

    public bool QButton { get; private set; }   // 감지된 q 버튼
    private void Update()
    {
        MouseRightButton = Input.GetMouseButtonDown(1);     // 마우스 오른쪽 입력감지
        MouseLeftButton = Input.GetMouseButtonDown(0);      // 마우스 왼쪽 입력감지
        QButton = Input.GetKey(KeyCode.Q);
    }
}
