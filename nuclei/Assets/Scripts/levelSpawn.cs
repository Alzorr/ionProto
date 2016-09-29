using UnityEngine;
using System.Collections;

public class levelSpawn : MonoBehaviour
{
    public int protonCount =5;
    public int electronCount=5;
    public int neutronCount=5;

    public GameObject protonPrefab;
    public GameObject electronPrefab;
    public GameObject neutronPrefab;

    //LEVEL
    public int levelWidth = 50;
    public int levelHeight = 50;

    // Use this for initialization
    void Start ()
    {
        for (int i = 0; i < neutronCount; i++)
        {
            Vector2 randomPosition = new Vector2(Random.Range(-levelWidth / 2, levelWidth / 2), Random.Range(-levelHeight / 2, levelHeight / 2));
            Instantiate(neutronPrefab, randomPosition, Quaternion.identity);
        }

        for (int i = 0; i < protonCount; i++)
        {
            Vector2 randomPosition = new Vector2(Random.Range(-levelWidth / 2, levelWidth / 2), Random.Range(-levelHeight / 2, levelHeight / 2));
            Instantiate(protonPrefab, randomPosition, Quaternion.identity);
        }

        for (int i = 0; i < electronCount; i++)
        {
            Vector2 randomPosition = new Vector2(Random.Range(-levelWidth / 2, levelWidth / 2), Random.Range(-levelHeight / 2, levelHeight / 2));
            Instantiate(electronPrefab, randomPosition, Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update ()
    {
	
	}
}
