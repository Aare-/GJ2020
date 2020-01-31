using UnityEngine;
using UnityEngine.UI;
   
public class ImgWrapper : MonoBehaviour {

    public RectTransform RTransform {
        get {
            if (_RTransform == null)
                _RTransform = GetComponent<RectTransform>();
            return _RTransform;
        }
    } 

    public Image Image {
        get {
            if (_Image == null)
                _Image = GetComponent<Image>();
            return _Image;
        }
    }

    [SerializeField]
    private Image _Image;
    
    [SerializeField]
    private RectTransform _RTransform;
}
