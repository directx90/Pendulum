using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulums : MonoBehaviour
{
    public float theta = 10;
    public int count = 10;
    public float defaultL = 1;
    public float zSpace = 1;
    public float lSpace = 0.3f;
    public float speed = 1;
    public GameObject pendulum;

    private bool running;

    private void Start()
    {
        var pendulums = GetComponentsInChildren<Pendulum>();
        running = true;
        foreach (var item in pendulums)
        {
            item.running = true;
        }
    }

    void Update()
    {
        var pendulums = GetComponentsInChildren<Pendulum>();
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            running = true;
            foreach (var item in pendulums)
            {
                item.running = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            running = false;
            foreach (var item in pendulums)
            {
                item.running = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            running = false;
            foreach (var item in pendulums)
            {
                item.running = false;
                item.SetTheta(theta);
                item.ResetPosition();
            }
        }
    }

    private void OnDrawGizmos()
    {
        var pendulums = GetComponentsInChildren<Pendulum>();
        for (int i = 0; i < pendulums.Length; i++)
        {
            var item = pendulums[i];
            item.SetL(defaultL + lSpace * i);
            if (!Application.isPlaying)
            {
                item.SetTheta(theta);
            }
            var localPosition = item.transform.localPosition;
            localPosition.z = zSpace * i;
            item.transform.localPosition = localPosition;
            item.speed = speed;
        }

        if (count < pendulums.Length)
        {
            for (int i = count; i < pendulums.Length; i++)
            {
                if (Application.isPlaying)
                {
                    Destroy(pendulums[i].gameObject);
                }
                else
                {
                    DestroyImmediate(pendulums[i].gameObject);
                }
            }
        }
        else if (count > pendulums.Length)
        {
            for (int i = pendulums.Length; i < count; i++)
            {
                var clone = Instantiate(pendulum, transform).GetComponent<Pendulum>();
                clone.SetL(defaultL + lSpace * i);
                clone.SetTheta(theta);

                var localPosition = clone.transform.localPosition;
                localPosition.z = zSpace * i;
                clone.transform.localPosition = localPosition;
            }
        }
    }
}