using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] heroPrefabs;
    public GameObject[] HerePrefabs { get { return heroPrefabs; } }

    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GeneratePlayerHero();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GeneratePlayerHero()
    {
        int i = Settings.playerPrefabId;

        GameObject heroObj = Instantiate(heroPrefabs[i],
            new Vector3(46f, 10f, 38f), Quaternion.identity);

        heroObj.tag = "Player";

        Character hero = heroObj.GetComponent<Character>();
        PartyManager.instance.Members.Add(hero);

       
    }
}
