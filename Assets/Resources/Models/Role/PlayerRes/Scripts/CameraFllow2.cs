using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFllow2 : MonoBehaviour
{
    public Transform player;
    public float speed_one = 10f;
    public float speed_two = 10f; //使用speed_one,speed_two可以调整玩家和相机的距离
    private float smooth = 1f;
    void LateUpdate()
    {
        Vector3 camera_felow = player.position + Vector3.up * speed_one - player.forward * speed_two;
        transform.position = Vector3.Lerp(transform.position, camera_felow, Time.deltaTime * smooth);
        transform.LookAt(player.position);
    }
}
