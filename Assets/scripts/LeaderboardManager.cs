using UnityEngine;
using TMPro;
using Dan.Main;

namespace LeaderboardCreatorDemo
{
    public class LeaderboardManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text[] _entryTextObjects;
        [SerializeField] private TMP_InputField _usernameInputField;
        private const string LEADERBOARD_PUBLIC_KEY = "d5a45051562f80592eea8adc1e2ab3e5012e9b628f98f62482ce9716d7d44033 "; // Replace with actual key

        private static LeaderboardManager instance;

        private void Awake()
        {

        }

        private void Start()
        {
            LoadEntries();
        }

        private void LoadEntries()
        {
            if (_entryTextObjects == null || _entryTextObjects.Length == 0)
            {
                Debug.LogError("Entry Text Objects are not assigned!");
                return;
            }

            LeaderboardCreator.GetLeaderboard(LEADERBOARD_PUBLIC_KEY, entries =>
            {
                foreach (var t in _entryTextObjects)
                    t.text = "";

                int length = Mathf.Min(_entryTextObjects.Length, entries.Length);
                for (int i = 0; i < length; i++)
                    _entryTextObjects[i].text = $"{entries[i].Rank}. {entries[i].Username} - {entries[i].Score}";
            });
        }

        public void UploadEntry()
        {
            int score = Random.Range(1, 100); 

            LeaderboardCreator.UploadNewEntry(LEADERBOARD_PUBLIC_KEY, _usernameInputField.text, score, success =>
            {
                if (success)
                {
                    Debug.Log("Entry uploaded successfully!");
                    LoadEntries();
                }
                else
                {
                    Debug.LogError("Failed to upload leaderboard entry.");
                }
            });
        }
    }
}
