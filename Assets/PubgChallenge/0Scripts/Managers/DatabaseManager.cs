using System;
using System.Collections;
using UnityEngine.Networking;

namespace PubgChallenge._0Scripts.Managers
{
    public static class DatabaseManager
    {
        public static IEnumerator GetRequest(string url, string apiKey, Action<UnityWebRequest> callback)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                request.SetRequestHeader("Accept", "application/vnd.api+json");
                request.SetRequestHeader("Authorization", apiKey);
                yield return request.SendWebRequest();
                callback(request);
            }
        }
    }
}