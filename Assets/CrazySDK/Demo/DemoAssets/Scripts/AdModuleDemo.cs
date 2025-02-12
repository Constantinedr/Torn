using UnityEngine;
using UnityEngine.UI;

namespace CrazyGames
{
    public class AdModuleDemo : MonoBehaviour
    {

        private void Start()
        {
            CrazySDK.Init(() => { }); // ensure if starting this scene from editor it is initialized
        }


        public void ShowMidgameAd()
        {
            CrazySDK.Ad.RequestAd(
                CrazyAdType.Midgame,
                () =>
                {
                    Debug.Log("Midgame ad started");
                },
                (error) =>
                {
                    Debug.Log("Midgame ad error: " + error);
                },
                () =>
                {
                    Debug.Log("Midgame ad finished");
                }
            );
        }

        public void ShowRewardedAd()
        {
            CrazySDK.Ad.RequestAd(
                CrazyAdType.Rewarded,
                () =>
                {
                    Debug.Log("Rewarded ad started");
                },
                (error) =>
                {
                    Debug.Log("Rewarded ad error: " + error);
                },
                () =>
                {
                    Debug.Log("Rewarded ad finished, reward the player here");
                }
            );
        }
    }
}
