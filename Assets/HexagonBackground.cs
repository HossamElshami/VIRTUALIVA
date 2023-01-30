using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HexagonBackground : MonoBehaviour
{
    public GameObject hexagonPrefab;
    public List<GameObject> hexagonList = new List<GameObject>();
    public float gridXOffset, gridYOffset;

    public float offsetY = 0, offsetX = 0;
    bool firstRow = true;

    private void Start()
    {
        for (int i = 0; i < 200; i++)
        {
            GameObject go = Instantiate(hexagonPrefab, transform.position, transform.rotation);
            go.transform.parent = transform;
            hexagonList.Add(go);
        }
        for (int x = 0; x < 200; x++)
        {
            if (x == 0) continue;

            if (x % 10 == 0)
            {
                offsetY += gridYOffset;
                firstRow = !firstRow;
                offsetX = firstRow ? 0 : gridXOffset / 2;
            }
            offsetX += gridXOffset;
            hexagonList[x].transform.position = new Vector3(offsetX, offsetY, 0);
        }
        transform.position = new Vector3(-19, -9, 4);
    }
}
