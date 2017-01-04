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
using CremiaViewModel.IO;
using CremiaViewModel.Routine;
using CremiaViewModel.Entity;
using CremiaViewModel.Entity.Polyomino;
using CremiaViewModel.Routine.Polyomino;

namespace CremiaSoft
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //TwoCharacterPhraseSearcher.Search(
            //    new TwoCharacterPhraseEntity('感', ' '),
            //    new TwoCharacterPhraseEntity('友', ' '), 
            //    new TwoCharacterPhraseEntity(' ', '報'), 
            //    new TwoCharacterPhraseEntity(' ', '熱'));

            //TwoCharacterPhraseSearcher.Search(
            //    new TwoCharacterPhraseEntity('実', ' '),
            //    new TwoCharacterPhraseEntity('事', ' '),
            //    new TwoCharacterPhraseEntity(' ', '度'),
            //    new TwoCharacterPhraseEntity(' ', '勢'),
            //    TwoCharacterPhraseLocationEnum.None);

            //DictionaryMaker.OutputTwoCharacterPhraseFromCSV();







        }
    }
}
