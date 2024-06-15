using AppJogoDaForca.Libraries.Text;
using AppJogoDaForca.Models;
using AppJogoDaForca.Repositories;
using System.Runtime.CompilerServices;

namespace AppJogoDaForca
{
    public partial class MainPage : ContentPage
    {
        private Word _word;
        private int _errors;
        public MainPage()
        {
            InitializeComponent();
            ResetScreen();


        }

        private void OnCounterClicked(object sender, EventArgs e)
        {

        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;

           button.IsEnabled = false;
           string letter = button.Text;// cast
           var positions = _word.Text.GetPositions(letter);

            if (positions.Count == 0)
            {
                _errors++;
                ImgMain.Source = ImageSource.FromFile($"forca{_errors + 1}.png");
                //a posição no element at é 1, pq o 0 é as cores e o 1 é o estilo.
                button.Style = App.Current.Resources.MergedDictionaries.ElementAt(1)["Fail"] as Style;

                if (_errors == 6)
                {
                    await DisplayAlert("Perdeu", "Você foi enforcado", "Novo Jogo");
                    ResetScreen();
                }
                return;
            }

            foreach (int position in positions)
            {
                LblText.Text = LblText.Text.Remove(position, 1).Insert(position, letter);
                
            }

            button.Style = App.Current.Resources.MergedDictionaries.ElementAt(1)["Success"] as Style;
        }

        private void ResetScreen()
        {
            ResetVirtualKeyboard();
            ResetErrors();
            GenerateNewWord();
        }

        private void GenerateNewWord()
        {
            var repository = new WordRepositories();
            _word = repository.GetRandomWord();
            LblTips.Text = _word.Tips;
            LblText.Text = new string('_', _word.Text.Length);
        }

        private void ResetErrors()
        {
            _errors = 0;
            ImgMain.Source = ImageSource.FromFile($"forca{_errors + 1}.png");
        }

        private void ResetVirtualKeyboard()
        {
            ResetVirtualLines((HorizontalStackLayout)KeyboardContainer.Children[0]);
            ResetVirtualLines((HorizontalStackLayout)KeyboardContainer.Children[1]);
            ResetVirtualLines((HorizontalStackLayout)KeyboardContainer.Children[2]);
        }

        private void ResetVirtualLines(HorizontalStackLayout horizontal)
        {
            foreach (Button button in horizontal.Children)
            {
                button.IsEnabled = true;
            }
        }
    }

}
