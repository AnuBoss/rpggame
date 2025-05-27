using UnityEngine;
using System.Collections.Generic;
public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private List<Enemy> monsters;
    public List<Enemy> Mosnters { get { return monsters; } }

   
    public static EnemyManager instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        instance = this;

    }
    void Start()
    {
        foreach (Character m in monsters)
        {
            m.CharInit(VFXManager.instance, UIManager.instance, InventoryManager.instance, PartyManager.instance);
        }

        InventoryManager.instance.AddItem(monsters[0], 0);
        InventoryManager.instance.AddItem(monsters[0], 1);
        InventoryManager.instance.AddItem(monsters[0], 2);

    }

    // Update is called once per frame
    void Update()
    {

    }

    
}
