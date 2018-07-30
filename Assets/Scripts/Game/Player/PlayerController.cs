using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class PlayerController : PunipuniController
{
    public Vector3 Velocity { get; set; }

    private Vector3 _startPos = Vector3.zero;
    private Vector3 _move = Vector3.zero;

    protected override void Start()
    {
        base.Start();
        this.transform.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
    }

    protected override void Update()
    {

    }

    public void KeyInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            BeginPunipuni();
            _startPos = this.transform.position;
        }

        if (Input.GetMouseButtonUp(0))
        {
            EndPunipuni();
            Velocity = Vector3.zero;
        }

        if (Input.GetMouseButton(0))
        {
            TrackingPunipuni();
            _move = this.transform.position - _startPos;

            Velocity = TouchControl();
        }
    }

    /// <summary>
    /// タッチでの操作
    /// </summary>
    private Vector3 TouchControl()
    {
        Vector3 tryMove = Vector3.zero;

        var touchPos = Input.mousePosition;
        var selfPos = BeginMousePosition + _move;
        var pos = Camera.main.ScreenToWorldPoint(touchPos);
        var target = new Vector3(pos.x, pos.y, 0.0f);
        var diff = target - selfPos;
        diff.z = 0.0f;
        Debug.DrawLine(selfPos, target);

        var dir = selfPos + new Vector3(0.0f, 1.0f, 0.0f);
        dir.z = 0.0f;
        Debug.DrawLine(selfPos, dir, Color.red);

        var axis = Vector3.Cross(dir, diff);
        var angle = Vector3.Angle(dir, diff) * (axis.z < 0 ? -1 : 1);

        return SetVelocityForRigidbody2D(angle, 1.0f);
    }

    // 速度を設定
    // @param 角度
    // @param 速さ
    public Vector3 SetVelocityForRigidbody2D(float direction, float speed)
    {
        // Setting velocity.
        Vector3 v = Vector3.zero;
        v.x = -Mathf.Cos(Mathf.Deg2Rad * direction) * speed;
        v.y = -Mathf.Sin(Mathf.Deg2Rad * direction) * speed;

        return v;
    }
}
