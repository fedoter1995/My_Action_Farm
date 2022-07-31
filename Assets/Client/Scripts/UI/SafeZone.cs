using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeZone : MonoBehaviour
{
    [SerializeField] private RectTransform stubPanel;
    private void Awake()
    {
        UpdateSafeZone();
    }

    private void UpdateSafeZone()
    {
        var safeArea = Screen.safeArea;
        int screenHeight = Screen.height;
        var myRectTransform = GetComponent<RectTransform>();

        var unsafeAreaHight = screenHeight - safeArea.height;

        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        stubPanel.sizeDelta = new Vector2(0, unsafeAreaHight);
        stubPanel.anchoredPosition = new Vector2(0, -unsafeAreaHight/2);

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;


        myRectTransform.anchorMin = anchorMin;
        myRectTransform.anchorMax = anchorMax;

    }
}
