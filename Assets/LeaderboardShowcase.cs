using System.Collections;
using Dan.Main;
using Dan.Models;
using TMPro;
using UnityEngine;

namespace Dan.Demo
{
    public class LeaderboardShowcase : MonoBehaviour
    {
        [Header("Leaderboard Essentials:")]
        [SerializeField] private Transform _entryDisplayParent;
        [SerializeField] private EntryDisplay _entryDisplayPrefab;

        [Header("Search Query Essentials:")]
        [SerializeField] private TMP_Dropdown _timePeriodDropdown;
        [SerializeField] private TMP_InputField _pageInput, _entriesToTakeInput;
        [SerializeField] private int _defaultPageNumber = 1, _defaultEntriesToTake = 100;

        private void Start()
        {
            Load();
        }

        public void Load()
        {
            var timePeriod = 
                _timePeriodDropdown.value == 1 ? Dan.Enums.TimePeriodType.Today :
                _timePeriodDropdown.value == 2 ? Dan.Enums.TimePeriodType.ThisWeek :
                _timePeriodDropdown.value == 3 ? Dan.Enums.TimePeriodType.ThisMonth :
                _timePeriodDropdown.value == 4 ? Dan.Enums.TimePeriodType.ThisYear : Dan.Enums.TimePeriodType.AllTime;

            var pageNumber = int.TryParse(_pageInput.text, out var pageValue) ? pageValue : _defaultPageNumber;
            pageNumber = Mathf.Max(1, pageNumber);
            _pageInput.text = pageNumber.ToString();
            
            var take = int.TryParse(_entriesToTakeInput.text, out var takeValue) ? takeValue : _defaultEntriesToTake;
            take = Mathf.Clamp(take, 1, 100);
            _entriesToTakeInput.text = take.ToString();
            
            var searchQuery = new LeaderboardSearchQuery
            {
                Skip = (pageNumber - 1) * take,
                Take = take,
                TimePeriod = timePeriod
            };
            
            Leaderboards.DemoSceneLeaderboard.GetEntries(searchQuery, OnLeaderboardLoaded, ErrorCallback);
        }

        private void OnLeaderboardLoaded(Entry[] entries)
        {
            // Destroy existing entries before creating new ones
            foreach (Transform t in _entryDisplayParent) 
                Destroy(t.gameObject);

            // Create new entry displays based on the loaded entries
            foreach (var entry in entries) 
                CreateEntryDisplay(entry);
        }

        private void CreateEntryDisplay(Entry entry)
        {
            // Instantiate the prefab for each leaderboard entry and set the entry data
            var entryDisplay = Instantiate(_entryDisplayPrefab.gameObject, _entryDisplayParent);
            entryDisplay.GetComponent<EntryDisplay>().SetEntry(entry);
        }

        private void ErrorCallback(string error)
        {
            Debug.LogError(error);
        }
    }
}
