using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.AssetManagement
{
  public interface IAssetProvider 
  {
    GameObject Instantiate(string path);
    GameObject GetAsset(string path);
  }
}