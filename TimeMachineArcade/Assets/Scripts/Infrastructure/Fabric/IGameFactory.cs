using UnityEngine;

namespace Infrastructure.Fabric
{
    public interface IGameFactory
    {
        public GameObject CreatePlayer(Transform at);
    }
}