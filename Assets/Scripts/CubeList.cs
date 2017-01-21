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
        GameObject father = GameObject.FindGameObjectWithTag("Ground");
        for (int i = 0; i < listSize; i++)
        {
            for (int j = 0; j < listSize; j++)
            {
                list[i, j] = Instantiate(cubePrefab);
                list[i, j].transform.SetParent(father.transform);
                getCube(i, j).transform.localScale = new Vector3(cubeSize, 1, cubeSize);
                getCube(i, j).transform.localPosition = new Vector3((i + 0.5f) * cubeSize-terrainRange/2,1, (j + 0.5f) * cubeSize-terrainRange/2);
            }
        }
        father.GetComponent<Transform>().Rotate(new Vector3(0, 45, 0));
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
    public float getScale(Vector2 position)
    {
        float x = position.x - getCube(0, 0).GetComponent<Transform>().position.x;
        float y = position.y - getCube(0, 0).GetComponent<Transform>().position.y;
        int xIndex = (int)Mathf.Floor(x);
        int yIndex = (int)Mathf.Floor(y);
        if (xIndex >= 0 && xIndex < listSize && yIndex >= 0 && yIndex < listSize)
            return getCube(xIndex, yIndex).GetComponent<Transform>().localScale.y;
        else
            return 1f;
    }
}
