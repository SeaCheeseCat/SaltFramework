using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject Owner;
    private Transform m_transform;
    public float speed = 1.5f;//控制移动速度
    public float moveSpeed = 5f; // 移动速度

    public float swingAmount = 10f; // 摆动的角度
    public float swingSpeed = 5f;   // 摆动的速度

    private void Awake()
    {
        m_transform = Owner.GetComponent<Transform>();
    }

    private void Update()
    {
        //ControlMove();
    }

    public void ControlMove() 
    {
        // 控制物体移动
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 获取相机的前方向（不受相机旋转影响）
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0; // 将y轴设为0，使其保持在水平方向
        cameraForward.Normalize(); // 归一化

        // 计算移动方向
        Vector3 moveDirection = (cameraForward * verticalInput + Camera.main.transform.right * horizontalInput).normalized;
        Vector3 moveAmount = moveDirection * moveSpeed * Time.deltaTime;

     


        // 左右摆动效果
        if (moveDirection != Vector3.zero)
        {
            float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);

            // 使用 Lerp 进行平滑旋转
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, swingSpeed * Time.deltaTime);
        }


        // 移动物体
        transform.Translate(moveAmount, Space.World);

    }

}
