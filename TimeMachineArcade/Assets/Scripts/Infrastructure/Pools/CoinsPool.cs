using Pools;
using UnityEngine;

public class CoinsPool : ObjectsPool<RectTransform>
{
    public CoinsPool(RectTransform prefab, Transform parent, int maxCount) : base(prefab, parent, maxCount)
    {
    }
}
