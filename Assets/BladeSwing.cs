using UnityEngine;
using System.Collections;

public class BladeSwing : MonoBehaviour
{
    public float swingSpeed = 2f;
    private bool isSwinging = false;
    private Quaternion startLocalRotation;
    private Quaternion singleBladeSwingRotation;
    private Quaternion dualBladeSwingRotation;
    private Transform cameraTransform;
    private DualBladeMode dualBladeMode;

    public AudioSource swingSource;

    void Start()
    {
        cameraTransform = transform.parent;
        dualBladeMode = GetComponent<DualBladeMode>();

        singleBladeSwingRotation = Quaternion.Euler(57, -67, -47);
        dualBladeSwingRotation = Quaternion.Euler(70, -40, 30);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isSwinging)
        {
            StartCoroutine(SmoothSwingSequence());
        }
    }

    private IEnumerator SmoothSwingSequence()
    {
        isSwinging = true;

        swingSource.time = 0.4f;
        swingSource.Play();

        startLocalRotation = transform.localRotation;

        Quaternion targetSwingRotation = dualBladeMode != null && dualBladeMode.IsDualBladeMode()
            ? startLocalRotation * dualBladeSwingRotation
            : startLocalRotation * singleBladeSwingRotation;

        yield return RotateToLocalTarget(cameraTransform.localRotation * targetSwingRotation);

        yield return RotateToLocalTarget(cameraTransform.localRotation * startLocalRotation);

        isSwinging = false;
    }

    private IEnumerator RotateToLocalTarget(Quaternion targetRotation)
    {
        while (Quaternion.Angle(transform.localRotation, targetRotation) > 0.1f)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, swingSpeed * Time.deltaTime);
            yield return null;
        }
        transform.localRotation = targetRotation;
    }
}