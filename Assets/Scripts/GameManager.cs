using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public MazeLoader MazeLoader;
    public GameObject player;
    public GameObject destinationPrefab;

    public Image imgFadeout;
    public GameObject textMeshPro;

    [HideInInspector]
    public GameObject mazeController;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Start()
    {
        while (!MazeLoader.completed)
        {
            yield return null;
        }

        Camera.main.transform.position = mazeController.transform.position;
        float maxMazeSize = MazeLoader.mazeColumns > MazeLoader.mazeRows ? MazeLoader.mazeColumns : MazeLoader.mazeRows;
        Camera.main.transform.position += Vector3.up * ((8 * maxMazeSize - 1) + 12);

        Instantiate(destinationPrefab, MazeLoader.destinationPoint, Quaternion.identity, mazeController.transform);
        Instantiate(player, MazeLoader.transform.position, Quaternion.identity);
    }
}
