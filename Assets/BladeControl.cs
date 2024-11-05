using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float extendSpeed = 0.1f;
    private bool weaponActive = true;
    private float scaleMin = 0f;
    private float scaleMax;
    private float extendDelta;
    private float scaleCurrent;
    private float localScaleX;
    private float localScaleZ;
    
    public GameObject blade; 
    public AudioSource ignitionSource; 
    public AudioSource reversedIgnitionSource;
    
    void Start()
    {
        localScaleX = transform.localScale.x;
        localScaleZ = transform.localScale.z;

        scaleMax = transform.localScale.y;
        scaleCurrent = scaleMax;

        extendDelta = scaleMax / extendSpeed;

        weaponActive = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (weaponActive)
            {
                reversedIgnitionSource.time = 2.0f;
                reversedIgnitionSource.Play();
                extendDelta = -Mathf.Abs(extendDelta);
            }
            else
            {
                ignitionSource.Play();
                extendDelta = Mathf.Abs(extendDelta);
            }
        }

        scaleCurrent += extendDelta * Time.deltaTime;
        scaleCurrent = Mathf.Clamp(scaleCurrent, scaleMin, scaleMax);

        transform.localScale = new Vector3(localScaleX, scaleCurrent, localScaleZ);

        weaponActive = scaleCurrent > 0;

        if (weaponActive && !blade.activeSelf)
        {
            blade.SetActive(true);
        }
        else if (!weaponActive && blade.activeSelf)
        {
            blade.SetActive(false);
        }
    }
}
