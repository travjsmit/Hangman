using System;
using System.Collections.Generic;
using System.IO;

// Interface for a game
interface IGame
{
    void Start();
    bool Guess(char letter);
}

// Enum to define game state
enum GameState
{
    Playing,
    Won,
    Lost
}

// Hangman game class
class HangmanGame : IGame
{
    // Private fields
    private string wordToGuess;
    private char[] guessedWord;
    private int attemptsLeft;
    private List<char> wrongGuesses;

    // Public property
    public GameState State { get; private set; }

    // Constructor
    public HangmanGame(string word, int maxAttempts)
    {
        wordToGuess = word.ToUpper();
        guessedWord = new string('_', wordToGuess.Length).ToCharArray();
        attemptsLeft = maxAttempts;
        wrongGuesses = new List<char>();
        State = GameState.Playing;
    }

    // Start the game
    public void Start()
    {
        Console.WriteLine("Welcome to Hangman!");

        while (State == GameState.Playing)
        {
            DisplayGameStatus();
            Console.Write("Enter a letter: ");
            char guess = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();

            if (!Guess(guess))
            {
                Console.WriteLine($"The letter '{guess}' is not in the word.");
            }

            if (IsWordGuessed())
            {
                State = GameState.Won;
            }
            else if (attemptsLeft <= 0)
            {
                State = GameState.Lost;
            }
        }

        DisplayFinalResult();
    }

    // Guess a letter
    public bool Guess(char letter)
    {
        if (!char.IsLetter(letter) || wrongGuesses.Contains(letter) || new string(guessedWord).Contains(letter))
        {
            Console.WriteLine("Invalid guess or already guessed. Try again.");
            return false;
        }

        if (wordToGuess.Contains(letter))
        {
            for (int i = 0; i < wordToGuess.Length; i++)
            {
                if (wordToGuess[i] == letter)
                {
                    guessedWord[i] = letter;
                }
            }
            return true;
        }
        else
        {
            wrongGuesses.Add(letter);
            attemptsLeft--;
            return false;
        }
    }

    // Display current game status
    private void DisplayGameStatus()
    {
        Console.WriteLine($"\nWord: {new string(guessedWord)}");
        Console.WriteLine($"Attempts Left: {attemptsLeft}");
        Console.WriteLine($"Wrong Guesses: {string.Join(", ", wrongGuesses)}");
    }

    // Check if the whole word is guessed
    private bool IsWordGuessed()
    {
        return new string(guessedWord) == wordToGuess;
    }

    // Display the final result of the game
    private void DisplayFinalResult()
    {
        if (State == GameState.Won)
        {
            Console.WriteLine($"Congratulations! You guessed the word: {wordToGuess}");
        }
        else if (State == GameState.Lost)
        {
            Console.WriteLine($"You lost! The word was: {wordToGuess}");
        }
    }

    // Static method to read words from a file
    public static List<string> ReadWordsFromFile(string filePath)
    {
        List<string> words = new List<string>();

        try
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        words.Add(line.Trim());
                    }
                }
            }

            if (words.Count == 0)
            {
                Console.WriteLine("No words found in the file.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
        }

        return words;
    }
}

// Main class
class Program
{
    static void Main(string[] args)
    {
        // File containing words for the game
        string filePath = "C:\\Users\\T.S. SMU\\OneDrive\\Documents\\Code\\Application Programming Lang and Tools\\Hangman\\Hangman\\words.txt";

        // Read words from file
        List<string> words = HangmanGame.ReadWordsFromFile(filePath);

        if (words.Count == 0)
        {
            Console.WriteLine("The words list is empty. Please check the words.txt file.");
            return;
        }

        // Random number generator
        Random random = new Random();
        string randomWord = words[random.Next(words.Count)];

        // Create and start the game
        HangmanGame game = new HangmanGame(randomWord, maxAttempts: 6);
        game.Start();
    }
}

