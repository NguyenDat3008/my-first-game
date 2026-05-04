using System.Collections;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D cursorNormal;
    [SerializeField] private Texture2D cursorShoot;
    [SerializeField] private Texture2D cursorReload;
    private float reloadTime = 1f;
    private float shootTime = 0.15f;
    private bool isReloading = false;

    private Vector2 hotspot = new Vector2(16, 48);
    void Start()
    {
        Cursor.SetCursor(cursorNormal, hotspot, CursorMode.Auto);
    }

    void Update()
    {
        // Nếu đang trong thời gian thay đạn, không cho phép các hành động cursor khác đè lên
        if (isReloading) return;

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ChangeCursorRoutine(cursorShoot, shootTime));
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(ChangeCursorRoutine(cursorReload, reloadTime));
        }
    }

    // Hàm xử lý thời gian chờ
    IEnumerator ChangeCursorRoutine(Texture2D targetTexture, float duration)
    {
        isReloading = true;
        Cursor.SetCursor(targetTexture, hotspot, CursorMode.Auto);

        yield return new WaitForSeconds(duration);

        Cursor.SetCursor(cursorNormal, hotspot, CursorMode.Auto);
        isReloading = false;
    }
}
