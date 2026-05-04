using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    private float rotateOffset = 180f;
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float shotDelay = 0.15f;
    private float nextShot;
    [SerializeField] private int maxAmmo = 30;
    private float reloadTime = 1f;
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private AudioManager audioManager;

    public int currentAmmo;
    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoText();
    }

    void Update()
    {
        RotateGun();
        Shoot();
        Reload();
    }
    void RotateGun()
    {
        if (Input.mousePosition.x < 0 || Input.mousePosition .x > Screen.width || Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height)
        {
            return; // Ignore rotation if the mouse is outside the screen bounds
        }
        Vector3 displacement = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle + rotateOffset);

        if (angle < -90 || angle > 90)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && Time.time > nextShot && currentAmmo > 0)
        {
            nextShot = Time.time + shotDelay;
            Instantiate(bulletPrefab, firePos.position, firePos.rotation);
            currentAmmo--;
            UpdateAmmoText();
            audioManager.PlayShootSound();
        }
    }

    void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            currentAmmo = maxAmmo;
            nextShot = Time.time + reloadTime;
            UpdateAmmoText();
            audioManager.PlayReloadSound();
        }
    }

    private void UpdateAmmoText()
    {
        if (ammoText != null)
        {
            if (currentAmmo > 0) 
            {
                ammoText.text = currentAmmo.ToString();
            }
            else 
            {
                ammoText.text = "Empty";
            }
        }
    }
}
