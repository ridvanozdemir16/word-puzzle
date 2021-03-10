using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp38
{

    class Program
    {
        // Definition of variables \\

        static int row;
        static string pathDict = null; //location of dictionary.txt
        static string pathPuzzle = null; //location of puzzle.txt
        static string pathSolution = null; //location of solution.txt
        static int dictLength; //length of dictionary.txt
        static string[] dictionary = new string[dictLength]; //array that keeping words in dictionary.txt alphabetically.
        static int ticPosition; // coordinate of '+'..
        static string tempWord = null; // a variable for printing finding word to the screen.
        static int tempWordCounter = 0; //variable for counting temping words
        static int wordCounter; //variable for counting finding words. If this variable reaches lenght of words in dictionary.txt, program will be end.
        static string[] puzzleHorizontal=new string [30]; // array that keeping rows of puzzle.txt. We use this variable for creating array of puzzleVertical. 
        static string[,] puzzleVertical=new string[1,1]; // array that keeping puzzle
        static string str = null; // variable that used in Bubble Sort Algorithm.


        // Definition of variables \\

        // FUNCTIONS \\

        //function that makes our puzzleVertical array dynamic..
        static void ResizeArray(ref string[,] original, int cols, int rows)
        {
             string[,] newArray = new string[rows, cols]; //create a new 2 dimensional array with the size we want
             Array.Copy(original, newArray, original.Length); //copy the contents of the old array to the new one
             original = newArray;//set the original to the new array
        }
        //function that makes our puzzleVertical array dynamic..

        //function that makes our puzzleHorizontal array dynamic..
        static int horizontalLength(string path)
        {
            StreamReader puzzlef = File.OpenText(path);

            row = 0;
            int i = 0;
            do
            {
                puzzleHorizontal[i] = puzzlef.ReadLine();
                Console.WriteLine();
                i++; row++;
            } while (!puzzlef.EndOfStream);
            Array.Resize(ref puzzleHorizontal, row);
            return row;
        }
        //function that makes our array dynamic..

        // function that reading dictionary.txt and putting the words that are in dictionary.txt into dictionary array alphabetically 
        static string Dictionary(string path)
        {
            //finding how many words are in the dictionary.txt
            StreamReader dictioanaryLengthf = File.OpenText(path);
            dictLength = 0;
            do
            {
                str = dictioanaryLengthf.ReadLine();
                dictLength++;

            } while (!dictioanaryLengthf.EndOfStream);
            //finding how many words are in the dictionary.txt
            Array.Resize(ref dictionary, dictLength); //----> resizing the dictionary array according to numbers of words in the dictionary.txt 
            
            //reading from dictionary.txt and putting the words into dictionary array
            StreamReader dictioanaryf = File.OpenText(path);
            int i = 0;
            do
            {
                str = dictioanaryf.ReadLine();
                dictionary[i] = str;
                i = i + 1;

            } while (!dictioanaryf.EndOfStream);
            dictioanaryf.Close();
            //reading from dictionary.txt and putting the words into dictionary array

            //Sorting the words in the dictionary array alphabetically with using "Bubble Sort Algorithm"
            string temp = null;
            for (int s = 0; s < dictionary.Length; s++)
            {
                for (int k = 0; k < dictionary.Length; k++)
                {

                    if (dictionary[s].Length < dictionary[k].Length)
                    {
                        temp = dictionary[k];
                        dictionary[k] = dictionary[s];
                        dictionary[s] = temp;
                    }
                    else if (dictionary[s].Length == dictionary[k].Length)
                    {
                        int result = dictionary[s].CompareTo(dictionary[k]);
                        if (result < 0)
                        {
                            temp = dictionary[k];
                            dictionary[k] = dictionary[s];
                            dictionary[s] = temp;
                        }
                    }
                }
            }
            //Sorting the words in the dictionary array alphabetically with using "Bubble Sort Algorithm"
            return path;
        }
       // function that reading dictionary.txt and putting the words that are in dictionary.txt into dictionary array alphabetically

        //function that printing dictionary array to the screen.
        static void dictionaryPrinter()
        {
            int temp=0;
            Console.SetCursorPosition(47, 1);
            Console.WriteLine("+--WORD-LIST---------------------------------+");
            for (int s = 0; s < (dictLength/2)+2; s++)
            {
                Console.SetCursorPosition(47, 2 + s);
                Console.WriteLine("|                                            |");
                temp = s;
            }
            Console.SetCursorPosition(47, 3+temp);
            Console.WriteLine("+--------------------------------------------+");

            for (int s = 0; s < dictionary.Length/2; s++)
            {
                Console.SetCursorPosition(50, 3 + s);
                Console.WriteLine("[ ]" + dictionary[s]);
            }
            int coordinate = 3;
            for (int s = dictionary.Length/2; s < dictionary.Length; s++)
            {
                Console.SetCursorPosition(63, coordinate);
                Console.WriteLine("[ ]" + dictionary[s]);
                coordinate++;
            }
            //kelimelerin tabloda yazılması
        }
        //function that printing dictionary array to the screen.

         //function that printing '+' if the word was found.
        static string ticPrinter(int TicPosition, string tic)
        {
            if (TicPosition >= 0 && TicPosition <= dictLength/2-1)
            {
                Console.SetCursorPosition(51, 3 + TicPosition);
                Console.WriteLine(tic);
            }
            else
            {
                Console.SetCursorPosition(64, TicPosition - 7);
                Console.WriteLine(tic);
            }
            return tic;
        }
        //function that printing '+' if the word was found.

        //function that reading puzzle.txt and printing to the screen
        static string puzzlePrinter(string path)
        {
            Console.SetCursorPosition(0, 0);
            StreamReader puzzlef = File.OpenText(path);

            int i = 0;
            int j = 0;
            int num1;
            for (int number = 0; number < row; number++)
            {
                num1 = number % 10;
                Console.SetCursorPosition(2, 3 + number);
                Console.Write(num1);
            }
            for (int number = 0; number < row; number++)
            {
                num1 = number % 10;
                Console.SetCursorPosition(3 + number, 2);
                Console.Write(num1);
            }
            do
            {
                puzzleHorizontal[i] = puzzlef.ReadLine();

                do
                {
                    puzzleVertical[i, j] = puzzleHorizontal[i].Substring(j, 1);

                    Console.SetCursorPosition(3 + j, 3 + i);
                    Console.Write(puzzleVertical[i, j]);
                    j++;
                } while (j < puzzleVertical.GetLength(1));
                Console.WriteLine();



                //Reads the first line

                i++;
                j = 0;


            } while (i < puzzleVertical.GetLength(0));
            return path;
        }
        //function that reading puzzle.txt and printing to the screen

        //function that scaning the puzzle horizontally
        static int scanHorizontal(string[,] puzzle)
        {
            for (int a = 0; a < puzzle.GetLength(0); a++)

            {
                int c = 0;
                for (int b = c; b < puzzle.GetLength(1); b++)
                {
                    int sameWords = 0;
                    int count1_horizental = 0;
                    int count2_horizental = 0;
                    int left = 0;
                    int right = 0;

                    int puzzle_space_length = 0;
                    if (puzzleVertical[a, b] != " " && puzzleVertical[a, b] != "█" && (puzzleVertical[a, b - 1] == " " || puzzleVertical[a, b + 1] == " ")) //finding the clue letter
                    {
                        do
                        {
                            if (puzzleVertical[a, b - left] != "█")
                                count1_horizental++;                        //counting the length of the word's left side according to clue letter.
                            left++;

                        } while (puzzleVertical[a, b - left] != "█");

                        do
                        {
                            if (puzzleVertical[a, b + right] != "█")
                                count2_horizental++;                    //counting the lengt of the word's right side according to clue letter.
                            right++;

                        } while (puzzleVertical[a, b + right] != "█");

                        puzzle_space_length = count1_horizental + count2_horizental - 1; //length of space of words in puzzle.
                        for (int m = 0; m < dictionary.Length; m++)
                        {
                            if (dictionary[m].Length == puzzle_space_length && dictionary[m][left - 1] == Convert.ToChar(puzzleVertical[a, b]))
                            { sameWords++; } //situation of same clue letter in  two differen words that are same length
                        }
                        if (sameWords == 1)
                        {
                            for (int m = 0; m < dictionary.Length; m++)
                            {

                                if (puzzleVertical[a, b] != " " && puzzleVertical[a, b] != "█" && (puzzleVertical[a, b - 1] == " " || puzzleVertical[a, b + 1] == " "))
                                {

                                    if (dictionary[m].Length == puzzle_space_length && dictionary[m][left - 1] == Convert.ToChar(puzzleVertical[a, b]))
                                    {
                                        Console.SetCursorPosition(b - left + 4, a + 3);
                                        Console.WriteLine(dictionary[m]);                       //printing the finding words to the screen and assigning it into puzzle array.
                                        int place = 0;
                                        for (int d = b - left + 1; d <= b + right - 1; d++)
                                        {
                                            puzzleVertical[a, d] = Convert.ToString(dictionary[m][place]);
                                            place++;
                                            //  c = b + dictionary[m].Length;
                                        }
                                        tempWord = dictionary[m];
                                        dictionary[m] = "xx";
                                        tempWordCounter = wordCounter;
                                        wordCounter++;
                                        ticPosition = m;
                                    }
                                }



                            }
                            if (tempWordCounter != wordCounter)
                            {
                                
                                Console.SetCursorPosition(0, 0);
                                Console.WriteLine("WORD= " + wordCounter);
                                Console.SetCursorPosition(2, row+3);                   //printing words and their counts to the screen.
                                Console.WriteLine("                                         ");
                                Console.SetCursorPosition(2, row+3);
                                Console.WriteLine("The word '" + tempWord + "' is placed.");
                                ticPrinter(ticPosition, "+");
                            }
                            Console.ReadLine();
                        }
                        c = b + right;
                    }
                }
            }
            return puzzle.GetLength(0);
        }
        //function that scaning the puzzle horizontally

        //function that scaning the puzzle vertically
        static int scanVertical(string[,] puzzle)
        {
            for (int b = 0; b < puzzle.GetLength(1); b++)
            {
                for (int a = 0; a < puzzle.GetLength(0); a++)
                {
                    int sameWords = 0;
                    int count1_vertical = 0;
                    int count2_vertical = 0;
                    int up = 0;
                    int down = 0;

                    int puzzle_space_length = 0;
                    if (puzzleVertical[a, b] != " " && puzzleVertical[a, b] != "█" && (puzzleVertical[a - 1, b] == " " || puzzleVertical[a + 1, b] == " "))//finding the clue letter
                    {
                        do
                        {
                            if (puzzleVertical[a - up, b] != "█")
                                count1_vertical++;                          //counting the length of the word's left side according to clue letter.
                            up++;

                        } while (puzzleVertical[a - up, b] != "█");

                        do
                        {
                            if (puzzleVertical[a + down, b] != "█")
                                count2_vertical++;                      //counting the length of the word's right side according to clue letter.
                            down++;

                        } while (puzzleVertical[a + down, b] != "█");

                        puzzle_space_length = count1_vertical + count2_vertical - 1; //length of space of words in puzzle.
                        for (int m = 0; m < dictionary.Length; m++)
                        {
                            if (dictionary[m].Length == puzzle_space_length && dictionary[m][up - 1] == Convert.ToChar(puzzleVertical[a, b]))
                            {
                                sameWords++; //situation of same clue letter in  two differen words that are same length
                            }
                        }
                        if (sameWords == 1)
                        {

                            for (int m = 0; m < dictionary.Length; m++)
                            {
                                if (dictionary[m].Length == puzzle_space_length && dictionary[m][up - 1] == Convert.ToChar(puzzleVertical[a, b]))
                                {
                                    if (puzzleVertical[a, b] != " " && puzzleVertical[a, b] != "█" && (puzzleVertical[a - 1, b] == " " || puzzleVertical[a + 1, b] == " "))
                                    {
                                        int place = 0;
                                        for (int d = a - up + 1; d <= a + down - 1; d++)
                                        {
                                            puzzleVertical[d, b] = Convert.ToString(dictionary[m][place]);  //printing the finding words to the screen and assigning it into puzzle array.
                                            place++;
                                            //  c = b + dictionary[m].Length;
                                        }
                                        tempWord = dictionary[m];
                                        tempWordCounter = wordCounter;
                                        wordCounter++;
                                        ticPosition = m;
                                    }
                                    if (dictionary[m].Length == puzzle_space_length && dictionary[m][up - 1] == Convert.ToChar(puzzleVertical[a, b]))
                                    {
                                        for (int w = 0; w < dictionary[m].Length; w++)
                                        {
                                            Console.SetCursorPosition(b + 3, a - up + 4);
                                            Console.WriteLine(dictionary[m][w]);
                                            up--;

                                        }
                                        dictionary[m] = "xx";
                                        up = 1;

                                    }
                                }

                            }
                            if (tempWordCounter != wordCounter)
                            {
                                Console.SetCursorPosition(0, 0);
                                Console.WriteLine("WORD= " + wordCounter);
                                Console.SetCursorPosition(2, row+3);                   //printing words and their counts to the screen.
                                Console.WriteLine("                                         ");
                                Console.SetCursorPosition(2, row+3);
                                Console.WriteLine("The word " + tempWord + " is placed.");
                                ticPrinter(ticPosition, "+");
                            }
                            Console.ReadLine();
                        }
                    }
                }
            }
            return puzzle.GetLength(1);
        }
        //function that scaning the puzzle vertically

        //function that printing the final puzzle into solution.txt
        static string dosyayaYaz(string path)
        {
            string dosya_yolu = path; //We specify the path of the file to be processed.
            FileStream fs = new FileStream(dosya_yolu, FileMode.OpenOrCreate, FileAccess.Write);//We are creating a file stream object.
            // First parameter is file path,
            //Second parameter specifies that the file will be opened if it is opened or not
            //Third parameter indicates that access to the file will be for writing data.
            StreamWriter sw = new StreamWriter(fs);  //We have created a StreamWriter object for the write operation.
            int j = 0;
            int i = 0;
            do
            {
                do
                {

                    sw.Write(puzzleVertical[i, j]); //WriteLine () method will write the text we will add to the file.
                    j++;
                } while (j < puzzleVertical.GetLength(1));
                sw.WriteLine();



                //Reads the first line

                i++;
                j = 0;


            } while (i < puzzleVertical.GetLength(0));
            
            sw.Flush();//We transferred the data to the file.
            sw.Close();
            fs.Close();
            // When we're done, we close the objects we use
            return path;
        }
        //function that printing the final puzzle into solution.txt

        // function of tutorial page 2
        static void tutorial2()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("                                                          What is 'Word Puzzle'?");
            Console.WriteLine();
            Console.WriteLine("                                  ╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("                                  ║                                                              ║");
            Console.WriteLine("                                  ║    WORDS=1                                                   ║");
            Console.WriteLine("                                  ║                                                              ║");
            Console.WriteLine("                                  ║    ███████████████        +-WORD-LIST-------------+          ║");
            Console.WriteLine("                                  ║    █    █OINTMENT█        | [ ]HEN    [ ]OPENER   |          ║");
            Console.WriteLine("                                  ║    █ ████ ██████ █        | [ ]MEN    [ ]CENTRAL  |          ║");
            Console.WriteLine("                                  ║    █ ████ ████   █        | [ ]DENT   [ ]ENGLISH  |          ║");
            Console.WriteLine("                                  ║    █ █ █     ███ █        | [ ]ENVY   [ ]HYGIENE  |          ║");
            Console.WriteLine("                                  ║    █ █ ██ █ ████ █        | [ ]MENU   [+]OINTMENT |          ║");
            Console.WriteLine("                                  ║    █      █ ████ █        | [ ]OXEN   [ ]TENDERLY |          ║");
            Console.WriteLine("                                  ║    ███ ████ ██ █ █        | [ ]TEEN               |          ║");
            Console.WriteLine("                                  ║    █  R   ███    █        | [ ]ENJOY              |          ║");
            Console.WriteLine("                                  ║    █ █ ██ ████ ███        | [ ]SPEND              |          ║");
            Console.WriteLine("                                  ║    █ █ █     █ █ █        | [ ]ENOUGH             |          ║");
            Console.WriteLine("                                  ║    █ ████ ██ █ █ █        | [ ]ENRICH             |          ║");
            Console.WriteLine("                                  ║    █ ████ ██ █ █ █        | [ ]HAPPEN             |          ║");
            Console.WriteLine("                                  ║    █       █ █   █        | [ ]LENDER             |          ║");
            Console.WriteLine("                                  ║    ███████████████        | [ ]MENTAL             |          ║");
            Console.WriteLine("                                  ║                           +-----------------------+          ║");
            Console.WriteLine("                                  ║ The word 'OINTMENT' is placed.                               ║");
            Console.WriteLine("                                  ║                                                              ║");
            Console.WriteLine("                                  ║    Solving Steps                                             ║");
            Console.WriteLine("                                  ║  1. Read puzzle.txt and dictionary.txt                       ║");
            Console.WriteLine("                                  ║  2. Scan the puzzle matrix to find a word                    ║");
            Console.WriteLine("                                  ║  3. Compare that word with dictionary. If it is suitable,    ║");
            Console.WriteLine("                                  ║ place the word                                               ║");
            Console.WriteLine("                                  ║  4. Continue until all the words are placed                  ║");
            Console.WriteLine("                                  ║  5. Finished puzzle should also be written to                ║");
            Console.WriteLine("                                  ║ the solution.txt file                                        ║");
            Console.WriteLine("                                  ║                                                              ║");
            Console.WriteLine("                                  ║        (1)Back                               Go to Menu(2)   ║");
            Console.WriteLine("                                  ║       <--------                               -------->      ║");
            Console.WriteLine("                                  ╚══════════════════════════════════════════════════════════════╝");
            ConsoleKeyInfo info = Console.ReadKey();
            if (info.KeyChar == '1')
            {
                tutorial1();
            }
            else if (info.KeyChar == '2')
            {
                Console.Clear();
                menu();
            }
        }
        // function of tutorial page 2

        //function of tutorial page 1
        static void tutorial1()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("                                                          What is 'Word Puzzle'?");
            Console.WriteLine();
            Console.WriteLine("                                  ╔══════════════════════════════════════════════════════════════╗");
            Console.WriteLine("                                  ║                                                              ║");
            Console.WriteLine("                                  ║                        WORD PUZZLE                           ║");
            Console.WriteLine("                                  ║                                                              ║");
            Console.WriteLine("                                  ║     Word puzzle contains puzzle matrix and words of the      ║");
            Console.WriteLine("                                  ║  matrix. These are given as text files. The objective is to  ║");
            Console.WriteLine("                                  ║  solve the puzzle by the computer AI. Program scans first    ║");
            Console.WriteLine("                                  ║  horizontally then scans vertically.                         ║");
            Console.WriteLine("                                  ║                                                              ║");
            Console.WriteLine("                                  ║    WORDS=0                                                   ║");
            Console.WriteLine("                                  ║                                                              ║");
            Console.WriteLine("                                  ║    ███████████████        +-WORD-LIST-------------+          ║");
            Console.WriteLine("                                  ║    █    █O       █        | [ ]HEN    [ ]OPENER   |          ║");
            Console.WriteLine("                                  ║    █ ████ ██████ █        | [ ]MEN    [ ]CENTRAL  |          ║");
            Console.WriteLine("                                  ║    █ ████ ████   █        | [ ]DENT   [ ]ENGLISH  |          ║");
            Console.WriteLine("                                  ║    █ █ █     ███ █        | [ ]ENVY   [ ]HYGIENE  |          ║");
            Console.WriteLine("                                  ║    █ █ ██ █ ████ █        | [ ]MENU   [ ]OINTMENT |          ║");
            Console.WriteLine("                                  ║    █      █ ████ █        | [ ]OXEN   [ ]TENDERLY |          ║");
            Console.WriteLine("                                  ║    ███ ████ ██ █ █        | [ ]TEEN               |          ║");
            Console.WriteLine("                                  ║    █  R   ███    █        | [ ]ENJOY              |          ║");
            Console.WriteLine("                                  ║    █ █ ██ ████ ███        | [ ]SPEND              |          ║");
            Console.WriteLine("                                  ║    █ █ █     █ █ █        | [ ]ENOUGH             |          ║");
            Console.WriteLine("                                  ║    █ ████ ██ █ █ █        | [ ]ENRICH             |          ║");
            Console.WriteLine("                                  ║    █ ████ ██ █ █ █        | [ ]HAPPEN             |          ║");
            Console.WriteLine("                                  ║    █       █ █   █        | [ ]LENDER             |          ║");
            Console.WriteLine("                                  ║    ███████████████        | [ ]MENTAL             |          ║");
            Console.WriteLine("                                  ║ Pres 'ENTER' to start.    +-----------------------+          ║");
            Console.WriteLine("                                  ║                                                              ║");
            Console.WriteLine("                                  ║                                                              ║");
            Console.WriteLine("                                  ║        (1)Back                                 Next(2)       ║");
            Console.WriteLine("                                  ║       <--------                               -------->      ║");
            Console.WriteLine("                                  ╚══════════════════════════════════════════════════════════════╝");
            ConsoleKeyInfo info = Console.ReadKey();
            if (info.KeyChar == '1')
            {
                Console.Clear();
                menu();
            }
            else if (info.KeyChar == '2')
            {
                tutorial2();
            }
        }
        //function of tutorial page 1

        //function of menu
        static void menu()
        {
            Console.WriteLine("-------WORD PUZZLE--------");
            Console.WriteLine("1.Puzzles");
            Console.WriteLine("2.What is 'Word Puzzle'");
            Console.WriteLine("3.Exit");
            ConsoleKeyInfo info = Console.ReadKey();
            if (info.KeyChar == '1')
            {
                Console.Clear();
                Console.WriteLine("-------WORD PUZZLE--------");
                Console.WriteLine("1.Main Puzzle");
                Console.WriteLine("2.Optional Puzzle");
                Console.WriteLine("3.Back to the main menu");
                ConsoleKeyInfo info1 = Console.ReadKey();
                if (info1.KeyChar == '1')
                {
                    pathDict = "dictionary.txt";
                    pathPuzzle = "puzzle.txt";
                    pathSolution = "solution.txt";
                }
                else if (info1.KeyChar == '2')
                {
                    
                    
                    pathDict = "dictionaryOptional.txt";
                    pathPuzzle = "puzzleOptional.txt";
                    pathSolution = "solutionOptional.txt";
                }
                else
                {
                    Console.Clear();
                    menu();
                }
            }

            else if (info.KeyChar == '2')
            {
                Console.Clear();
                tutorial1();
            }
            else
            {
                System.Environment.Exit(1);
            }

        }
        //function of menu

        //function that rules them all..Main function
        static void wordPuzzle()
        {
            int space_place;
            menu();
            Console.Clear();
            horizontalLength(pathPuzzle);
            ResizeArray(ref puzzleVertical, row, row);
            Dictionary(pathDict);
            dictionaryPrinter();
            puzzlePrinter(pathPuzzle);
            Console.SetCursorPosition(2, row+3);
            Console.WriteLine("Press ENTER to start.");
            Console.ReadLine();
            do
            {
                space_place = 0;

                scanHorizontal(puzzleVertical);
                scanVertical(puzzleVertical);
                for (int a = 0; a < row; a++)
                {
                    for (int b = 0; b < row; b++)
                    {
                        if (puzzleVertical[a, b] == " ")
                            space_place++;
                    }
                }

            } while (space_place != 0);
            Console.SetCursorPosition(2, row+3);
            Console.WriteLine("                         ");
            Console.SetCursorPosition(2, row+3);
            Console.WriteLine("Puzzle has been solved. Goodbye.");
            dosyayaYaz(pathSolution);
            Console.SetCursorPosition(2, row+4);
            Console.WriteLine("Do you want to play again?(Y/N)");
            ConsoleKeyInfo info = Console.ReadKey();
            if (info.KeyChar == 'y' || info.KeyChar == 'Y')
            {
                Console.Clear();
                wordCounter = 0;
                wordPuzzle();
            }
            else
            {
                System.Environment.Exit(1);
            }
        }
        //function that rules them all..Main function

            // FUNCTIONS \\

        static void Main(string[] args)
            {
            wordPuzzle();
            }
        
    }
}


