using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private void Awake()
    {
        Random.InitState(42);
    }

    public IEnumerator Shake(float _duration, float _magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < _duration)
        {
            float x = Random.Range(1f, -1f) * _magnitude;
            float y = Random.Range(-1f, 1f) * _magnitude;

            Vector3 newPosition;
            newPosition.x = originalPos.x + x;
            newPosition.y = originalPos.y + y;
            newPosition.z = originalPos.z;

            transform.position = newPosition;
            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
