using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public LineRenderer hook;

    void Start()
    {
        
    }

    void Update()
    {
        gameObject.transform.localPosition = hook.GetPosition(1);
    }
}
