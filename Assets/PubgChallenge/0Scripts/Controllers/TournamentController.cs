using System;
using PubgChallenge._0Scripts.Managers;
using PubgChallenge._0Scripts.Views;
using UnityEngine;

namespace PubgChallenge._0Scripts.Controllers
{
    public class TournamentController : MonoBehaviour
    {
        private readonly string _urlAPI = "https://api.pubg.com/tournaments";

        private readonly string _apiKEY =
            "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJqdGkiOiI1M2JhMWIwMC01OGYzLTAxMzktNDZjYi0wZjU5YzlmMDY0NjEiLCJpc3MiOiJnYW1lbG9ja2VyIiwiaWF0IjoxNjE0MTg3NTc3LCJwdWIiOiJibHVlaG9sZSIsInRpdGxlIjoicHViZyIsImFwcCI6IndpZG93Z2FtZS10ZXN0In0.6MSVSLBWsXlg3M7V9E9fMo_fH1WKcVwor7FPqR3Weqk";

        private TournamentData _tournamentData;
        [SerializeField] private GameObject _rowUiPrefab;
        [SerializeField] private Transform _uiContainer;

        private void Start()
        {
            StartCoroutine(DatabaseManager.GetRequest(_urlAPI, _apiKEY, request =>
            {
                if (request.isNetworkError || request.isHttpError)
                {
                    Debug.Log($"{request.error} : {request.downloadHandler.text}");
                }
                else
                {
                    _tournamentData = JsonUtility.FromJson<TournamentData>(request.downloadHandler.text);

                    foreach (var data in _tournamentData.data)
                    {
                        LoadDataUI(data.id, data.attributes.createdAt);
                    }
                }
            }));
        }

        private void LoadDataUI(string id, string datetime)
        {
            GameObject newRow = Instantiate(_rowUiPrefab, _uiContainer);
            newRow.GetComponent<TournamentView>().tournamentId.text = id;
            newRow.GetComponent<TournamentView>().tournamentDate.text = datetime;
        }
    }

    [Serializable]
    public class TournamentData
    {
        public data[] data;
    }

    [Serializable]
    public class data
    {
        public string type;
        public string id;
        public Attributes attributes;
    }

    [Serializable]
    public struct Attributes
    {
        public string createdAt;
    }
}