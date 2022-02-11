using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static SharedValues;

public class GameManager : MonoBehaviour
{
    public RectTransform TextArea;
    public RectTransform ButtonContainer;
    public RectTransform Answer;
    public RectTransform Question;
    public TMP_InputField questionInput;

    private readonly float defaultXTextArea = (float)-487;
    private readonly float defaultYTextArea = (float)303;

    private readonly float defaultXButtonContainer = (float)284;
    private readonly float defaultYButtonContainer = (float)142;

    private readonly float defaultXAnswer = (float)0;
    private readonly float defaultYAnswer = (float)-318;

    private readonly float defaultXQuestion = (float)0;
    private readonly float defaultYQuestion = (float)-432;

    private void Awake()
    {
        Application.targetFrameRate = 31;
        Application.runInBackground = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        DragablePositions(DragNames.TextArea, TextArea, defaultXTextArea, defaultYTextArea);
        DragablePositions(DragNames.ButtonContainer, ButtonContainer, defaultXButtonContainer, defaultYButtonContainer);
        DragablePositions(DragNames.Answer, Answer, defaultXAnswer, defaultYAnswer);
        DragablePositions(DragNames.Question, Question, defaultXQuestion, defaultYQuestion);

        questionInput.text = PlayerPrefs.GetString(QuestionsPref, "");
    }

    private void DragablePositions(DragNames dragName, RectTransform rectTransform, float defaultX, float defaultY)
    {
        float x = PlayerPrefs.GetFloat(dragName.ToString() + "X", defaultX);
        float y = PlayerPrefs.GetFloat(dragName.ToString() + "Y", defaultY);

        rectTransform.anchoredPosition = new Vector2(x, y);
    }

    public void ResetToDefault()
    {
        TextArea.anchoredPosition = new Vector2(defaultXTextArea, defaultYTextArea);
        PlayerPrefs.SetFloat(DragNames.TextArea + "X", defaultXTextArea);
        PlayerPrefs.SetFloat(DragNames.TextArea + "Y", defaultYTextArea);

        ButtonContainer.anchoredPosition = new Vector2(defaultXButtonContainer, defaultYButtonContainer);
        PlayerPrefs.SetFloat(DragNames.ButtonContainer + "X", defaultXButtonContainer);
        PlayerPrefs.SetFloat(DragNames.ButtonContainer + "Y", defaultYButtonContainer);

        Answer.anchoredPosition = new Vector2(defaultXAnswer, defaultYAnswer);
        PlayerPrefs.SetFloat(DragNames.Answer + "X", defaultXAnswer);
        PlayerPrefs.SetFloat(DragNames.Answer + "Y", defaultYAnswer);

        Question.anchoredPosition = new Vector2(defaultXQuestion, defaultYQuestion);
        PlayerPrefs.SetFloat(DragNames.Question + "X", defaultXQuestion);
        PlayerPrefs.SetFloat(DragNames.Question + "Y", defaultYQuestion);
    }
}
