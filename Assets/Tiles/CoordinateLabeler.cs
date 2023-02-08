using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField]
    Color DefaultColor = Color.gray;
    [SerializeField]
    Color BlockedColor = Color.red;

    TextMeshPro label;
    Vector2Int Cor = new Vector2Int();
    Waypoint waypoint;

    void Awake()
    {
        label = GetComponent<TextMeshPro>();
        waypoint = GetComponentInParent<Waypoint>();

        label.enabled = false;
        if (!Application.isPlaying)
        {
            label.enabled = true;
        }
        DisplayCor();
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCor();
            UpdateObjectName();
        }

        SetLabelColor();
        ToggleLabels();
    }

    void SetLabelColor()
    {
        if (waypoint.IsPlaceable)
        {
            label.color = DefaultColor;
        }
        else
        {
            label.color = BlockedColor;
        }
    }

    void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            label.enabled = !label.IsActive();
        }
    }

    void DisplayCor()
    {
        Cor.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        Cor.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        label.text = Cor.x + "," + Cor.y;
    }

    void UpdateObjectName()
    {
        transform.parent.name = Cor.ToString();
    }
}
