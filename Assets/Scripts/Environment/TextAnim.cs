using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextAnim : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TMPText;
    [SerializeField] float timeBtwnChars;
    [SerializeField] float timeBtwnWords;

    int i = 0;
    public string[] stringArray;
    void Start()
    {
        
    }

    public void EndCheck()
    {      
        if (i <= stringArray.Length - 1)
        {
            TMPText.text = stringArray[i];
            StartCoroutine(TextVisible());
        }
    }

    private IEnumerator TextVisible()
    {
        TMPText.ForceMeshUpdate();
        int totalVisibleChar = TMPText.textInfo.characterCount;
        int counter = 0;

        while (true)
        {
            int visibleCount = counter % (totalVisibleChar + 1);
            TMPText.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleChar)
            {
                i += 1;
                Invoke("EndCheck", timeBtwnWords);
                break;
            }

            counter += 1;
            yield return new WaitForSeconds(timeBtwnChars);
        }
    }
}
