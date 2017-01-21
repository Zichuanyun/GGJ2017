using UnityEngine;
using System.Collections;

public class CubeList : MonoBehaviour {

    public int listSize;
    public float terrainRange;
    public static float ratio = 3.0f;
    public GameObject cubePrefab;
    GameObject[,] list;
    float cubeSize;

    public int getListSize() {
        return listSize;
    }
    public GameObject getCube(int x, int y) {
        return list[x,y];
    }
    public void Start() {
        list = new GameObject[listSize, listSize];
        cubeSize = terrainRange / listSize;
        for (int i = 0; i < listSize; i++)
        {
            for (int j = 0; j < listSize; j++)
            {
                list[i, j] = Instantiate(cubePrefab);
                getCube(i, j).transform.localScale = new Vector3(cubeSize, 1, cubeSize);
                getCube(i, j).transform.localPosition = new Vector3((i + 0.5f) * cubeSize-terrainRange/2,1, (j + 0.5f) * cubeSize-terrainRange/2);
            }
        }
    }
    public void reset()
    {
        for (int i = 0; i < listSize; i++)
        {
            for (int j = 0; j < listSize; j++)
            {
                getCube(i, j).GetComponent<CubeBehavior>().reset();
            }
        }
    }
}
