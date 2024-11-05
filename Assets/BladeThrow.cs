using UnityEngine;
using System.Collections;

public class BladeThrow : MonoBehaviour
{
    public float throwDistance = 5f;
    public float throwSpeed = 2f;
    public float spinSpeed = 360f;
    private bool isThrown = false;
    private Vector3 startLocalPosition;
    private Quaternion startLocalRotation;
    private Transform cameraTransform;
    private DualBladeMode dualBladeMode;

    void Start()
    {
        cameraTransform = transform.parent;
        dualBladeMode = GetComponent<DualBladeMode>(); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && !isThrown)
        {
            StartCoroutine(ThrowAndReturn());
        }
    }

    private IEnumerator ThrowAndReturn()
    {
        isThrown = true;

        startLocalPosition = transform.localPosition;
        startLocalRotation = transform.localRotation;

        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + cameraTransform.forward * throwDistance;

        float journey = 0f;
        while (journey < 1f)
        {
            journey += throwSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, journey);
            transform.Rotate(spinSpeed * Time.deltaTime, 0, 0);
            yield return null;
        }

        journey = 0f;
        while (journey < 1f)
        {
            journey += throwSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(targetPosition, startPosition, journey);
            transform.Rotate(spinSpeed * Time.deltaTime, 0, 0);
            yield return null;
        }

        transform.localPosition = startLocalPosition;
        transform.localRotation = startLocalRotation;
        isThrown = false;
    }
}