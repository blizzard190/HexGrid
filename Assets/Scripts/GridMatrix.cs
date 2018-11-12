using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GridMatrix : MonoBehaviour {

    public int width = 6;
    public int height = 6;

    public GridCell cellPrefab;
    public Text cellLabelPrefab;

    Canvas gridCanvas;
    HexMesh hexMesh;
    GridCell[] cells;

    void Awake()
    {
        gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();

        cells = new GridCell[height * width];

        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                CreateCell(x, z, i++);
            }
        }
    }

    void Start()
    {
        hexMesh.Triangulate(cells);
    }

    void CreateCell(int x, int z, int i)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (HexMatrix.innerRadius * 2f);
        position.y = 0f;
        position.z = z * (HexMatrix.outerRadius * 1.5f);

        GridCell cell = cells[i] = Instantiate<GridCell>(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;


        Text label = Instantiate<Text>(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition =
        new Vector2(position.x, position.z);
        label.text = z.ToString() + "\n" + x.ToString();
    }
}
