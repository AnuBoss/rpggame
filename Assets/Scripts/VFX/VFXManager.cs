using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private GameObject doubleRingMarker;
    public GameObject DoubleRingMarker
    {
        get { return doubleRingMarker; }
    }

    public static VFXManager instance;

    [SerializeField]
    private GameObject[] magicVFX;
    public GameObject[] MagicVFX { get { return magicVFX; } }

    [SerializeField]
    private MagicData[] magicData;
    public MagicData[] MagicData { get { return magicData; } }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMagic(int id, Vector3 posA, float time)
    {
        //Load . Magic
        if (magicVFX[id]== null)
        return;
        posA.y += 1.5f;
        posA.z -= 0.5f;
        GameObject objLoad = Instantiate(magicVFX[id], posA, Quaternion.identity);
        Destroy(objLoad, time);
    }
    public void ShootMagic(int id, Vector3 posA, Vector3 posB, float time)
    {
        //Shoot . Magic
        if (magicVFX[id]== null)
        return;
       
        posB.y += 1.5f;
        GameObject objShoot = Instantiate(magicVFX[id], posA, Quaternion.identity);
        objShoot.transform.position = Vector3.LerpUnclamped(posA, posB, time);
        Destroy(objShoot, time);
    }
}
