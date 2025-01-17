using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MiniMap : MonoBehaviourPunCallbacks
{
    // Kaydırma hızı
    [SerializeField] private float scrollSpeed = 1f;
    // Minimum zoom seviyesi
    [SerializeField] private float minValue = 10f;
    // Maximum zoom seviyesi
    [SerializeField] private float maxValue = 60f;
    // Şu anki zoom seviyesi
    private float currentValue;

    private void Start()
    {
        if(!photonView.IsMine)
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // Fare tekerlek verilerine bakmak
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        // Kaydırmak durumuna göre zoom değişkenini ayarlamak
        if (scrollDelta > 0)
        {
            currentValue += scrollSpeed;
        }
        else if (scrollDelta < 0)
        {
            currentValue -= scrollSpeed;
        }
        // Kaydırma değerinin kodda tanıtılan minimum ve maksimum yakınlaştırma değerleri arasında sınırlandırılması
        // Mevcut yakınlaştırma değeri sınırlayıcı değerler arasında "sıkıştırılır" (Clamp), yani sonsuza kadar yakınlaştırıp uzaklaştıramayız
        currentValue = Mathf.Clamp(currentValue, minValue, maxValue);
        gameObject.GetComponent<Camera>().orthographicSize = currentValue;
    }
}
