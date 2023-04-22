using Adic;
using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

namespace HiplayGame
{
    public class Role
    {
        [Inject]
        IAssetLoader assetLoader;

        GameObject go;

        public void CreateAsync(string name, Transform parent, Action<GameObject> complete)
        {
            Debug.Log("´´½¨user");
            assetLoader.InstantiateAsync("Role", parent, false, (go) =>
            {
                this.go = go;
                var text = go.transform.Find("UIName").Find("Text").GetComponent<TextMeshProUGUI>();
                text.text = name;
                complete?.Invoke(go);
            });
        }

        public void Destroy()
        {
            GameObject.Destroy(go);
        }

        public void SetLocalPosition(Vector3 localPos)
        {
            go.transform.localPosition = localPos;
        }

        public void MoveTo(Vector3 target)
        {
            go.transform.DOMove(target, 3f);
        }
    }

}
