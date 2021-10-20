using UnityEngine;
using UnityEngine.Advertisements;
using System;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    #if UNITY_ANDROID
        private string gameId = "3824949";
    #elif UNITY_IOS
        private string gameId = "3824948";
    #endif

    [SerializeField]
    private bool testMode = false;
    private string adPlacementId = "rewardedVideo";
    public static event Action RewardedVideoAdFinished;
    public static AdsManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        Advertisement.Initialize(gameId, testMode);
        Advertisement.AddListener(this);
    }

    public void ShowRewardAd()
    {
        if (Advertisement.IsReady(adPlacementId))
            Advertisement.Show(adPlacementId);
    }
    
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        switch (showResult)
        {
            case ShowResult.Finished:
                if (placementId == adPlacementId)
                    RewardedVideoAdFinished?.Invoke();
                break;

            default:
                break;
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //throw new System.NotImplementedException();
    }
}
