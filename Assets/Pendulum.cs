using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// http://www.maths.surrey.ac.uk/explore/michaelspages/documentation/Simple
/// </summary>
public class Pendulum : MonoBehaviour
{
    public bool running;
    public float speed;
    public float l;
    public float theta;
    public float curTheta;
    private float deltaTheta;

    void Update()
    {
        if (running)
        {
            deltaTheta += Time.deltaTime * (Physics.gravity.y / l * Mathf.Sin(curTheta * Mathf.Deg2Rad)) * Mathf.Rad2Deg * speed;
            curTheta += Time.deltaTime * deltaTheta * speed;
        }
        UpdatePosition();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector3 { z = transform.position.z }, transform.position);
    }

    private void UpdatePosition()
    {
        var localPosition = transform.localPosition;
        localPosition.x = l * Mathf.Sin(curTheta * Mathf.Deg2Rad);
        localPosition.y = -l * Mathf.Cos(curTheta * Mathf.Deg2Rad);
        transform.localPosition = localPosition;
        transform.localEulerAngles = new Vector3 { x = curTheta };
    }

    public void SetL(float l)
    {
        this.l = l;
        UpdatePosition();
    }

    public void SetTheta(float theta)
    {
        this.theta = theta;
        curTheta = theta;
        UpdatePosition();
    }

    public void ResetPosition()
    {
        deltaTheta = 0;
        curTheta = theta;
        UpdatePosition();
    }
}
