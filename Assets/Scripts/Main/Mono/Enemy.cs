using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public GameObject hud;

    public Image icon;

    public TextMeshProUGUI name;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetName(string nameContent)
    {
        if (nameContent == "")
        {
            hud.SetActive(false);
            return;
        }


        hud.SetActive(true);
        this.name.text = nameContent;
    }

    public void SetIcon(string url)
    {
        hud.SetActive(true);
        DownSprite(url);
    }



    async Task DownSprite(string url)
    {
        UnityWebRequest wr = new UnityWebRequest(url);
        DownloadHandlerTexture texDl = new DownloadHandlerTexture(true);
        wr.downloadHandler = texDl;
        await wr.SendWebRequest();
        int width = 100;
        int high = 100;
        if (!wr.isNetworkError)
        {
            Texture2D tex = new Texture2D(width, high);
            tex = texDl.texture;
            //Save2LocalPath(tex);
            Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
            icon.sprite = sprite;
        }
        else
        {
            Debug.LogError("ÍøÂç´íÎó " + url);
        }
    }
}
