using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextBoxManager3 : MonoBehaviour {

    public GameObject textBox;

    public Text theText;

    public TextAsset textfile;
    public string[] textlines;

    public int currentLine;
    public int endAtLine;

    public Movement p;

    private bool isTyping = false;

    private bool cancelTyping = false;
    public float typeSpeed;

    // Use this for initialization
    void Start()
    {
        p = FindObjectOfType<Movement>();

        if (textfile != null)
        {
            textlines = (textfile.text.Split('\n'));
        }
        if(endAtLine == 0)
        {
            endAtLine = textlines.Length - 1;
        }

    }

   void Update()
    {
        //theText.text = textlines[currentLine];

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isTyping)
            {
                currentLine += 1;
                if (currentLine > endAtLine)
                {
                    Application.LoadLevel(4);
                }else
                {
                    StartCoroutine(TextScroll(textlines[currentLine]));
                }
            }
            else if(isTyping && !cancelTyping)
            {
                cancelTyping = true;
            }
        }
    }  
    
    private IEnumerator TextScroll(string lineOfText)
    {
        int letter = 0;
        theText.text = " ";
        isTyping = true;
        cancelTyping = false;
        while (isTyping && !cancelTyping && (letter <lineOfText.Length-1))
        {
            theText.text += lineOfText[letter];
            letter++;
            yield return new WaitForSeconds(typeSpeed);
        }
        theText.text = lineOfText;
        isTyping = false;
        cancelTyping = false;
    }   

    public void EnableTextBox()
    {
        textBox.SetActive(true);
        StartCoroutine(TextScroll(textlines[currentLine]));

    }
}
