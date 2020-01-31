using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyMessenger;
using UnityEngine;

class TinyTokenManager {
    private static TinyTokenManager _Instance;
    public static TinyTokenManager Instance {
        get {
            if (_Instance == null) _Instance = new TinyTokenManager();
            return _Instance;
        }
        private set {}
    }

    private struct TokenContainer {
        public TinyMessageSubscriptionToken Token;
        public Type MessageType;

        public TokenContainer(TinyMessageSubscriptionToken token, Type messageType) {
            Token = token;
            MessageType = messageType;
        }
    }

    private Dictionary<String, TokenContainer> _CachedTokens;

    public TinyTokenManager() {
        _CachedTokens = new Dictionary<string, TokenContainer>();
    }

    private string SubscriptionTag<T>(MonoBehaviour listener) {
        return String.Format("{0}/{1}", listener.GetInstanceID().ToString(), typeof(T).ToString());
    }

    public void Register<T>(MonoBehaviour listener, Action<T> action, bool silenceWarning = false) where T : class, ITinyMessage {
        var tag = SubscriptionTag<T>(listener);        

        if (_CachedTokens.ContainsKey(tag)) {
            if (!silenceWarning) {
                Debug.LogWarning(String.Format("Event {0} already subscribed - replacing with a new one", tag));
            }
            Unregister<T>(listener);
        }

        _CachedTokens
            .Add(tag, 
                 new TokenContainer(
                     TinyMessengerHub.Instance.Subscribe<T>(action),
                     typeof(T)));
    }

    public void Unregister<T>(MonoBehaviour listener) {
        var tag = SubscriptionTag<T>(listener);

        if (_CachedTokens.ContainsKey(tag)) {
            var container = _CachedTokens[tag];
            _CachedTokens.Remove(tag);

            TinyMessengerHub.Instance.Unsubscribe(container.Token, container.MessageType);
        }
    }

    public void UnregisterAll(MonoBehaviour listener) {
        char[] separator = {'/'};
        var id = listener.GetInstanceID().ToString();
        var keysToUnsubscribe = new List<string>();

        foreach (var key in _CachedTokens.Keys) 
            if (key.Split(separator)[0].Equals(id)) 
                keysToUnsubscribe.Add(key);

        foreach (var key in keysToUnsubscribe) {
            var container = _CachedTokens[key];            
            _CachedTokens.Remove(key);
            
            TinyMessengerHub.Instance.Unsubscribe(container.Token, container.MessageType);            
        }        
    }    
}
