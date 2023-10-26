using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PROG7213_ST10083209_POE_Part_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<string> callNumbers;
        Random random;
        Random random1;
        int correctReorderings;
        int totalReorderings;


        
        private Dictionary<string, string> callNumbersAndDescriptions = new Dictionary<string, string>
        {
            {"000-099", "general works"},
            {"100-199", "philosophy and psychology"},
            {"200-299", "religion"},
            {"300-399", "social scienc"},
            {"400-499", "language"},
            {"500-599", "natural science and mathematics"},
            {"600-699", "technology"},
            {"700-799", "the arts"},
            {"800-899", "literature and rhetoric"},
            {"900-999", "history, biology, and geography"}
        };
        private int i;

        public MainWindow()
        {
            InitializeComponent();
            callNumbers = new List<string>();
            random = new Random();
            random1= new Random();
            correctReorderings = 0;
            totalReorderings = 0;


            ShowIdentifyingAreasButton();
            StartIdentifyingAreas();
            GetUserAnswer();



        }


        private void GenerateCallNumbers_Click(object sender, RoutedEventArgs e)
        {
            callNumbers.Clear();
            for (int i = 0; i < 10; i++)
            {
                callNumbers.Add(random.Next(000, 999).ToString());
            }
            callNumbersListBox.ItemsSource = callNumbers;
            
            MessageBox.Show("Now, you can try to reorder the call numbers in ascending order by clicking on them.");
        }

        private void OrderInAscendingButton_Click(object sender, RoutedEventArgs e)
        {
            var currentList = callNumbersListBox.Items.Cast<string>().ToList();
            var sortedList = currentList.OrderBy(x => x).ToList();
            callNumbersListBox.ItemsSource = sortedList;
        }








        private void ShowIdentifyingAreasButton()
        {
            IdentifyingAreasButton.Visibility = Visibility.Visible;
        }

        private void IdentifyingAreasButton_Click(object sender, RoutedEventArgs e)
        {
            IdentifyingAreasButton.Visibility = Visibility.Collapsed;
            StartIdentifyingAreas();
        }



        private void StartIdentifyingAreas()
        {
            string[] callNumbers = callNumbersAndDescriptions.Keys.ToArray();
            string callNumber, description;
            int randomIndex;

            for (int i = 0; i < 10; i++)
            {
                randomIndex = random.Next(0, callNumbers.Length);
                callNumber = callNumbers[randomIndex];
                description = callNumbersAndDescriptions[callNumber];

                ShowQuestion(callNumber, description);
                GetUserAnswer();
            }
        }

        private void ShowQuestion(string callNumber, string description)
            {
                string[] answers = GenerateAnswers(callNumber, description);

                CallNumberOrDescriptionLabel.Content = i % 2 == 0 ? "Call Number" : "Description";
                CallNumberOrDescriptionComboBox.ItemsSource = i % 2 == 0 ? answers : callNumbersAndDescriptions.Keys.ToArray();
                CorrectAnswerLabel.Content = answers[0];
            }

            private string[] GenerateAnswers(string correctAnswer, string otherAnswer)
            {
                List<string> answers = new List<string>();
                answers.Add(correctAnswer);

                for (int i = 0; i < 3; i++)
                {
                    string wrongAnswer;
                    do
                    {
                        wrongAnswer = callNumbersAndDescriptions.Keys.ToArray()[random.Next(0, callNumbersAndDescriptions.Count)];
                    } while (wrongAnswer == correctAnswer);

                    answers.Add(wrongAnswer);
                }

                return answers.ToArray();
            }

            private void GetUserAnswer()
            {
                if (CallNumberOrDescriptionComboBox.SelectedItem != null)
                {
                    string userAnswer = CallNumberOrDescriptionComboBox.SelectedItem.ToString();
                    ShowUserAnswer(userAnswer);
                }
            }

            private void ShowUserAnswer(string userAnswer)
            {
                ResultLabel.Content = userAnswer == CorrectAnswerLabel.Content ? "Correct!" : "Incorrect.";
            }

    }

        
    }
