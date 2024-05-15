using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using UnityEngine.UI;

public class HighScoreManager : MonoBehaviour
{
    // Database connection string
    private string dbName = "URI=file:HighScore.db";

    // UI Text arrays for displaying top 10 high scores
    public Text[] playerNameTexts; // Array for player names
    public Text[] scoreTexts; // Array for scores

    // UI InputField for player name input
    public InputField playerNameInputField;

    // Initialize the current score
    private int currentScore;

    void Start()
    {
        // Create the high scores table if it doesn't exist
        CreateHighScoresTable();
        DisplayTopHighScores();
    }

    // Function to create the high scores table
    private void CreateHighScoresTable()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS high_scores (player_name VARCHAR(50), score INT);";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    // Function to set the current score
    public void SetCurrentScore(int score)
    {
        currentScore = score;
    }

    // Function to save the high score
    public void SaveHighScore()
    {
        // Get the player's name from the input field
        string playerName = playerNameInputField.text;

        // Save the player's name and score to the database
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = $"INSERT INTO high_scores (player_name, score) VALUES ('{playerName}', {currentScore});";
                command.ExecuteNonQuery();
            }
            connection.Close();
        }

        // Update the top high scores display
        DisplayTopHighScores();
    }

    // Function to display the top 10 high scores
    public void DisplayTopHighScores()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();

            using (var command = connection.CreateCommand())
            {
                // Query to get the top 10 high scores in descending order
                command.CommandText = "SELECT player_name, score FROM high_scores ORDER BY score DESC LIMIT 10;";

                using (IDataReader reader = command.ExecuteReader())
                {
                    int index = 0;
                    // Iterate through the results and update the UI text arrays
                    while (reader.Read() && index < playerNameTexts.Length && index < scoreTexts.Length)
                    {
                        // Update player names text array
                        playerNameTexts[index].text = reader["player_name"].ToString();

                        // Update scores text array
                        scoreTexts[index].text = reader["score"].ToString();

                        index++;
                    }
                }
            }
            connection.Close();
        }
    }
}
