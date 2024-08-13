Hangman Console Application
Overview
This project is a console-based Hangman game implemented in C#. It incorporates various programming concepts, including classes, interfaces, properties, collections, file I/O, and random number generation.

Features
Class Design: The game is encapsulated within a HangmanGame class, implementing the IGame interface.
Game Logic: Uses an enum GameState to manage game states (Playing, Won, Lost).
Properties: The game state is managed using properties with appropriate access modifiers.
Methods: Includes various methods, such as Start(), Guess(char), IsWordGuessed(), and static ReadWordsFromFile(string).
File I/O: Reads a list of possible words from a words.txt file.
Randomization: Selects a random word for the game using System.Random.
Collections: Manages guessed and wrong letters using List<char>.
Instructions
Ensure a words.txt file is present in the project directory with a list of words, one per line.
Compile and run the program using a C# compiler or an IDE like Visual Studio.
Play the game by entering letters to guess the word.
Future Enhancements
Add a graphical user interface.
Integrate with a database to store game statistics.
