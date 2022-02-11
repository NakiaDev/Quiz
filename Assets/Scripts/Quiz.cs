using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static SharedValues;
using System.Linq;

public class Quiz : MonoBehaviour
{
    public TMP_InputField questionInput;
    public TextMeshPro answerText;
    public TextMeshPro questionText;
    public GameObject noMoreQuestions;

    Dictionary<string, string> usedQuestions = new Dictionary<string, string>();

    public void QuestionTextChanged()
    {
        PlayerPrefs.SetString(QuestionsPref, questionInput.text);
    }

    public void GetNewQuestion()
    {
        noMoreQuestions.SetActive(false);

        if (string.IsNullOrEmpty(questionInput.text)) return;

        string[] questions = questionInput.text.Split(new string[] { "\r\n", "\r", "\n" }, System.StringSplitOptions.None);
        if (questions.Length == 0) return;

        KeyValuePair<string, string> questionWithAnswer = GetRandomQuestion(ConvertArrayToDictionary(questions));
        if (questionWithAnswer.Key == null || questionWithAnswer.Value == null)
        {
            noMoreQuestions.SetActive(true);
        }
        else
        {
            answerText.text = questionWithAnswer.Value;
            questionText.text = questionWithAnswer.Key;
        }
    }

    private Dictionary<string, string> ConvertArrayToDictionary(string[] questions)
    {
        Dictionary<string, string> converted = new Dictionary<string, string>();

        foreach (string item in questions)
        {
            string[] questionWithAnswer = item.Split(';');
            if (questionWithAnswer.Length != 2) continue;

            if (questionWithAnswer[0].StartsWith(" "))
                questionWithAnswer[0] = questionWithAnswer[0].Substring(1);

            if (questionWithAnswer[1].StartsWith(" "))
                questionWithAnswer[1] = questionWithAnswer[1].Substring(1);

            converted.Add(questionWithAnswer[0], questionWithAnswer[1]);
        }

        return converted;
    }

    private KeyValuePair<string, string> GetRandomQuestion(Dictionary<string, string> questions)
    {
        int random = Random.Range(0, questions.Count);
        KeyValuePair<string, string> questionWithAnswer = questions.ElementAt(random);

        if (usedQuestions.Contains(questionWithAnswer))
        {
            Dictionary<string, string> temp = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> item in questions)
            {
                if (!usedQuestions.Contains(item))
                    temp.Add(item.Key, item.Value);
            }

            if (temp.Count > 0)
                return GetRandomQuestion(temp);
            else
                return new KeyValuePair<string, string>();
        }
        else
        {
            usedQuestions.Add(questionWithAnswer.Key, questionWithAnswer.Value);
            return questionWithAnswer;
        }
    }

    public void Restart()
    {
        usedQuestions = new Dictionary<string, string>();
        answerText.text = "";
        questionText.text = "";
        noMoreQuestions.SetActive(false);
    }
}
