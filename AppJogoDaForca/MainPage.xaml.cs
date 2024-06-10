using AppJogoDaForca.Libraries.Text;
using AppJogoDaForca.Models;
using AppJogoDaForca.Repositories;

namespace AppJogoDaForca
{
    public partial class MainPage : ContentPage
    {
        private Word _word;
        private int _errors = 0;
        public MainPage()
        {
            InitializeComponent();
            var repository = new WordRepositories();
            _word = repository.GetRandomWord();
            LblTips.Text = _word.Tips;
            LblText.Text = new string('_', _word.Text.Length);
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {

        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            ((Button)sender).IsEnabled = false;
           string letter = ((Button)sender).Text;// cast
           var positions = _word.Text.GetPositions(letter);

            if (positions.Count == 0)
            {
                _errors++;
                return;
            }

            foreach (int position in positions)
            {
                LblText.Text = LblText.Text.Remove(position, 1).Insert(position, letter);
                
            }
        }
    }

}
