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
        if (Settings.isNewGame)
        {
            Settings.isNewGame = false;
            GeneratePlayerHero();
        }

        if (Settings.isWarping)
        {
            Settings.isWarping = false;
            WarpPlayers();
        }
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

        hero.CharInit(VFXManager.instance, UIManager.instance,
                InventoryManager.instance, PartyManager.instance);

        InventoryManager.instance.AddItem(hero, 0);//health potion
        InventoryManager.instance.AddItem(hero, 2);//Shield.A
        //InventoryManager.instance.AddItem(hero, 1);


    }

    private void WarpPlayers()
    {
        PartyManager.instance.LoadAllHeroData();
    }
}
