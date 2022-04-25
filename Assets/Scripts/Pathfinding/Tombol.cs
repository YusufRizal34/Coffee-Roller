using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tombol : MonoBehaviour
{
    [SerializeField]
    private Transform targetTransform;
    private RectTransform rectTransform;
    private Image image;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }


    private void Update()
    {
        var screenPoint = Camera.main.WorldToScreenPoint(targetTransform.position);
        rectTransform.position = screenPoint;

        var viewPortPoint = Camera.main.WorldToViewportPoint(targetTransform.position);
        var distanceFromCenter = Vector2.Distance(viewPortPoint, Vector2.one * 0.5f);

        var show = distanceFromCenter < 0.3f;

        image.enabled = show;
    }
}
