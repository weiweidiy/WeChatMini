using Adic;
using EnhancedScrollerAdvance;
using HiplayGame;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TeamCellView : EnhancedUnitViewV2
{
    public class Factory : EnhancedUnitViewPlaceHolderFactoryV2<TeamCellView> { }

    Image imgHead;

    TextMeshProUGUI txtName;

    public override void OnRefreshCellView()
    {
        //throw new System.NotImplementedException();
        var userObj = _data.UnitData as UserObject;
        txtName.text = userObj.nickName;
        DownSprite(userObj.iconUrl);
    }

    protected override void OnInitialize()
    {
        imgHead = _go.transform.Find("Head/Head").GetComponent<Image>();
        txtName = _go.transform.Find("Name").GetComponent<TextMeshProUGUI>();
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
            imgHead.sprite = sprite;
        }
        else
        {
            Debug.LogError("ÍøÂç´íÎó " + url);
        }
    }
}

public class TeamRank : MonoBehaviour
{
    public EnhancedScrollerViewAdvanceV2 scroller;

    NormalEnhancedScrollerDelegateV2 dele;

    public Team team;

    [Inject]
    TeamManager teamManager;
    // Start is called before the first frame update
    void Start()
    {
        this.Inject();

        scroller = transform.Find("Scroller").GetComponent<EnhancedScrollerViewAdvanceV2>();
        dele = new NormalEnhancedScrollerDelegateV2(scroller, GetDataList(), GetCellViewFactory());
        scroller.Delegate = dele;

        teamManager.onMemberAdded += TeamManager_onMemberAdded;
        teamManager.onMemberExited += TeamManager_onMemberExited;
    }

    private void OnDestroy()
    {
        teamManager.onMemberAdded -= TeamManager_onMemberAdded;
        teamManager.onMemberExited -= TeamManager_onMemberExited;
    }

    private void TeamManager_onMemberExited(UserObject obj, Team arg2)
    {
        if (arg2 != team)
            return;

        var members = teamManager.GetTeamMembers(team);

        var datas = BuildData(members);

        dele.Reload(datas);
    }

    private void TeamManager_onMemberAdded(UserObject arg1, Team arg2)
    {
        if (arg2 != team)
            return;

        var members = teamManager.GetTeamMembers(team);

        var datas = BuildData(members);

        dele.Reload(datas);
    }

    List<EnhancedDataV2> BuildData(List<UserObject> list)
    {
        List<EnhancedDataV2> dataList = new List<EnhancedDataV2>();

        foreach (var user in list)
        {
            var unit = new EnhancedDataV2();
            unit.UnitData = user;
            dataList.Add(unit);
        }

        return dataList;
    }

    private Dictionary<string, IEnhancedUnitViewFactoryV2> GetCellViewFactory()
    {
        //return new ContentView.Factory();

        Dictionary<string, IEnhancedUnitViewFactoryV2> factories = new Dictionary<string, IEnhancedUnitViewFactoryV2>();

        factories.Add("", new TeamCellView.Factory());

        return factories;
    }

    private List<EnhancedDataV2> GetDataList()
    {
        List<EnhancedDataV2> dataList = new List<EnhancedDataV2>();

        //for (int i = 0; i < 10; i++)
        //{
        //    dataList.Add(new EnhancedDataV2() { UnitData = i });
        //}

        return dataList;
    }
}
