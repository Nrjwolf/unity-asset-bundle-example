using System;
using System.Collections;
using UnityEngine;

public class Main : MonoBehaviour
{
    private WWW www;
    public AssetBundle bundle { get; protected set; } // бандл с ресурсами, который будет загружен
    private bool bundleLoaded = false;

    // перед стартом игры подгружаем бандл
    IEnumerator Start()
    {
        // загружаем по указанному пути
        www = new WWW("http://nrjwolf.com/bundle/mybundle");
        yield return www;
        if (www.assetBundle != null)
        {
            bundle = www.assetBundle;
            bundleLoaded = true;
            Debug.Log("Asset loaded successfully");
            AddDucks();
        }
        else
        {
            Debug.Log(www.error);
        }
    }

    void Update()
    {
        if (!bundleLoaded && www != null)
        {
            // показываем прогресс загрузки бандла
            print((int)(www.progress * 100) + "%");
        }
    }

    void AddDucks()
    {
        foreach (var path in bundle.GetAllAssetNames())
        {
            var go = new GameObject();
            go.transform.position = new Vector2(
                UnityEngine.Random.Range(-3f, 3f),
                UnityEngine.Random.Range(-3f, 3f)
            );
            var sprite = bundle.LoadAsset<Sprite>(path);
            go.AddComponent<SpriteRenderer>().sprite = sprite;
        }
    }
}
