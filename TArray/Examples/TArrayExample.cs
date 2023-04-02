using Extensions;
using UnityEngine;

public class TArrayExample : MonoBehaviour
{
    public TArray<int> intArray = new int[5, 5];
    public TArray<string> stringArray = new TArray<string>(5, 5);
    public TArray<GameObject> gameObjectArray = new TArray<GameObject>();
}