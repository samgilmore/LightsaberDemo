using UnityEngine;

public class DualBladeMode : MonoBehaviour
{
    public GameObject bladeBase1;
    public GameObject bladeBase2;
    public KeyCode toggleKey = KeyCode.B;
    private bool isDualBlade = false;

    void Start()
    {
        bladeBase2.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            isDualBlade = !isDualBlade;
            ToggleDualBladeMode();
        }
    }

    private void ToggleDualBladeMode()
    {
        if (isDualBlade)
        {
            bladeBase2.SetActive(true);
            transform.localRotation = Quaternion.Euler(90, 0, 0);
        }
        else
        {
            bladeBase2.SetActive(false);
            transform.localRotation = Quaternion.identity;
        }
    }

    public bool IsDualBladeMode()
    {
        return isDualBlade;
    }
}