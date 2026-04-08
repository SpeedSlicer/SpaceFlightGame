using Unity.VisualScripting;
using UnityEngine;

public class ShipCustomizerStorage : MonoBehaviour
{
    public static string[] storage;
    public static bool[] owned;

    [SerializeField]
    ShipCustomObject[] storages;

    public static int setIndex = 0;
    public static bool updateColors = false;

    void Awake()
    {
        storage = new string[storages.Length];

        for (int i = 0; i < storages.Length; i++)
        {
            storage[i] = "#" + storages[i].color.ToHexString();
        }
        owned = new bool[storage.Length];
        for (int i = 0; i < owned.Length; i++)
        {
            owned[i] = false;
        }
        owned[0] = true;
    }

    public static void SetIndex(int index)
    {
        setIndex = index;
    }
}
