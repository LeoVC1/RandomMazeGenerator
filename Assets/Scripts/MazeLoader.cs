using UnityEngine;
using System.Collections.Generic;

public class MazeLoader : MonoBehaviour {
	public int mazeRows, mazeColumns;
	public GameObject wall;
	public GameObject floor;
    public GameObject mazeController;
	public float size = 2f;

	private MazeCell[,] mazeCells;

    private List<Vector3> cells = new List<Vector3>();

    [HideInInspector]
    public Vector3 destinationPoint;
    [HideInInspector]
    public bool completed;

    void Start () {
		InitializeMaze ();

		MazeAlgorithm ma = new HuntAndKillMazeAlgorithm (mazeCells);
		ma.CreateMaze ();
	}
	
	void Update () {
	}

	private void InitializeMaze() {

		mazeCells = new MazeCell[mazeRows,mazeColumns];

        for (int r = 0; r < mazeRows; r++) {
			for (int c = 0; c < mazeColumns; c++) {
                List<Transform> walls = new List<Transform>();

				mazeCells [r, c] = new MazeCell ();

				// For now, use the same wall object for the floor!
				mazeCells [r, c] .floor = Instantiate (floor, new Vector3 (r*size, -(size/2f), c*size), Quaternion.identity, transform) as GameObject;
				mazeCells [r, c] .floor.name = "Floor " + r + "," + c;
				mazeCells [r, c] .floor.transform.Rotate (Vector3.right, 90f);

                walls.Add(mazeCells[r, c].floor.transform);

				if (c == 0) {
					mazeCells[r,c].westWall = Instantiate (wall, new Vector3 (r*size, 0, (c*size) - (size/2f)), Quaternion.identity, transform) as GameObject;
					mazeCells [r, c].westWall.name = "West Wall " + r + "," + c;
                    walls.Add(mazeCells[r, c].westWall.transform);
                }

				mazeCells [r, c].eastWall = Instantiate (wall, new Vector3 (r*size, 0, (c*size) + (size/2f)), Quaternion.identity, transform) as GameObject;
				mazeCells [r, c].eastWall.name = "East Wall " + r + "," + c;

                walls.Add(mazeCells[r, c].eastWall.transform);

                if (r == 0) {
					mazeCells [r, c].northWall = Instantiate (wall, new Vector3 ((r*size) - (size/2f), 0, c*size), Quaternion.identity, transform) as GameObject;
					mazeCells [r, c].northWall.name = "North Wall " + r + "," + c;
					mazeCells [r, c].northWall.transform.Rotate (Vector3.up * 90f);
                    walls.Add(mazeCells[r, c].northWall.transform);
                }

				mazeCells[r,c].southWall = Instantiate (wall, new Vector3 ((r*size) + (size/2f), 0, c*size), Quaternion.identity, transform) as GameObject;
				mazeCells [r, c].southWall.name = "South Wall " + r + "," + c;
				mazeCells [r, c].southWall.transform.Rotate (Vector3.up * 90f);

                walls.Add(mazeCells[r, c].southWall.transform);

                cells.Add(FindCenter(walls.ToArray()));
			}
		}

        Vector3 center = FindCenter(transform.GetComponentsInChildren<Transform>());

        GameObject middle = Instantiate(mazeController, transform.position, Quaternion.identity);
        middle.transform.position = center;
        transform.parent = middle.transform;

        GameManager.instance.mazeController = middle;

        destinationPoint = cells[cells.Count - 1];
        completed = true;
    }

    Vector3 FindCenter(Transform[] objects)
    {
        Vector3 centroid = Vector3.zero;

        if (objects.Length > 0)
        {
            foreach (var child in objects)
            {
                centroid += child.transform.position;
            }
            centroid /= (objects.Length);
        }

        return centroid;
    }


}
