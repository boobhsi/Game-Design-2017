using UnityEngine;

using System.Collections;

using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    private Text _text;

    private float _currentScore = 0;

    const string ScorePrefix = "Score : ";

    // Use this for initialization

    void Start()
    {

        _text = this.GetComponent<Text>();

        _text.text = ScorePrefix + _currentScore;

    }

    public void AddScore(int score)

    {

        _currentScore += score;

        _text.text = ScorePrefix + _currentScore;

    }

    // Update is called once per frame

    void Update()
    {

    }

}
