using UnityEngine;

namespace Infrastructure.AssetManagement
{
  public class AssetProvider : IAssetProvider
  {
    public GameObject GetAsset (string path)
    {
      var prefab = Resources.Load<GameObject>(path);
      return prefab;
    }

    public GameObject Instantiate(string path)
    {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab);    
    }
  }
}