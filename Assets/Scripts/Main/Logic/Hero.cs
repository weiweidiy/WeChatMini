using Adic;
using System;
using UnityEngine;

namespace HiplayGame
{
    public class Hero
    {
        [Inject]
        IAssetLoader assetLoader;

        [Inject]
        GroundView groundView;

        GameObject go;

        public void CreateAsync(Transform parent, Action<GameObject> complete)
        {
            assetLoader.InstantiateAsync("PolyArtWizardStandardMat", parent, false, (go) =>
            {

                this.go = go;
                complete?.Invoke(go);
            });
        }

        public void Destroy()
        {
            GameObject.Destroy(go);
        }

        public void SetParent(Transform parent)
        {
            go.transform.parent = parent;
        }

        public void Rotate(Vector3 rotate)
        {
            go.transform.Rotate(rotate);
        }
    }

}
