using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private bool m_isExecuting = false;
    public bool IsExecuting { get => m_isExecuting; set => m_isExecuting = value; }

    private void Awake()
    {
        Random.InitState(42);
    }

    public IEnumerator Shake(float _duration, float _magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;
        IsExecuting = true;

        while (elapsed < _duration)
        {
            float x = Random.Range(-0.1f, 0.1f) * _magnitude;
            float y = Random.Range(-0.1f, 0.1f) * _magnitude;

            transform.position = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
        IsExecuting = false;
    }
}
