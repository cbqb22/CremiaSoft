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
using System.Windows.Shapes;
using System.Drawing;
using CremiaViewModel.Entity.Polyomino;
using CremiaViewModel.Routine.Polyomino;
using System.Printing;
using System.Windows.Xps;
using CremiaView.UI.Printing;


namespace CremiaSoft.UI.Windows
{
    /// <summary>
    /// PolyominoQuestionMaker.xaml の相互作用ロジック
    /// </summary>
    public partial class PolyominoQuestionMaker : Window
    {

        private const string polyominoSaveFolderPath = @"C:\Users\poohace\Pictures\謎解き\ペントミノ問題";

        public PolyominoQuestionMaker()
        {
            InitializeComponent();
            this.Loaded += PolyominoQuestionMaker_Loaded;
        }

        private void PolyominoQuestionMaker_Loaded(object sender, RoutedEventArgs e)
        {
            SaveImages();
            SetPolyomino(Make());
        }

        private void SaveImages()
        {

            string saveFolderPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "NumImages\\" + DateTime.Now.ToString("yyyyMMddHHmmss"));


            //InstalledFontCollectionオブジェクトの取得
            System.Drawing.Text.InstalledFontCollection ifc =
                new System.Drawing.Text.InstalledFontCollection();
            //インストールされているすべてのフォントファミリアを取得

            var ffs = ifc.Families;
            var fontNames = ffs.ToList().Select(x => x.Name).ToList();



            Enumerable.Range(0, 9).ToList().ForEach(num =>
            {
                Canvas canvas = new Canvas() { Width = 28, Height = 28 };
                TextBlock tbl = new TextBlock() { Width = 28, Height = 28 };
                tbl.FontSize = 20;
                tbl.TextAlignment = TextAlignment.Center;
                tbl.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black);
                tbl.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
                tbl.Text = num.ToString();
                //tbl.FontFamily = new System.Windows.Media.FontFamily("MS UI Gothic");
                canvas.Children.Add(tbl);

                //canvas.UpdateLayout();


                var folder = System.IO.Path.Combine(saveFolderPath, num.ToString());

                if (!System.IO.Directory.Exists(folder))
                    System.IO.Directory.CreateDirectory(folder);

                int counter = 0;
                counter++;

                Print(false, canvas, folder);
                tblNumber.Text = num.ToString();
                tblNumber.FontFamily = new System.Windows.Media.FontFamily("MS UI Gothic");
                //Print(false, cvNumber, folder);

                //fontNames.ForEach(x =>
                //{
                //    counter++;
                //    tblNumber.Text = num.ToString();
                //    tblNumber.FontFamily = new System.Windows.Media.FontFamily(x);

                //    Print(false, cvNumber, polyominoSaveFolderPath);
                //});
            });


        }

        public PolyominoSet Make()
        {
            PolyominoSet set = null;
            while (set == null)
            {
                set = PolyominoMaker.MakePentominoQuestion(6, 6, 6, true, true);
            }

            return set;

        }

        public void SetPolyomino(PolyominoSet set)
        {
            foreach (var cell in set.DrawCellAreaEntity.Cells)
            {
                int AnswerSizes = 30;
                Grid gd = new Grid();
                gd.Width = AnswerSizes;
                gd.Height = AnswerSizes;
                gd.SetValue(Canvas.LeftProperty, AnswerSizes * (cell.X + 1));
                gd.SetValue(Canvas.TopProperty, AnswerSizes * (cell.Y + 1));
                var color = System.Windows.Media.Color.FromArgb(cell.BackColor.A, cell.BackColor.R, cell.BackColor.G, cell.BackColor.B);
                gd.Background = new SolidColorBrush(color);
                canvas.Children.Add(gd);


                double QuestionSizes = 77.6;

                Grid gd2 = new Grid();
                gd2.Width = QuestionSizes;
                gd2.Height = QuestionSizes;
                gd2.SetValue(Canvas.LeftProperty, QuestionSizes * (cell.X + 1));
                gd2.SetValue(Canvas.TopProperty, QuestionSizes * (cell.Y + 1));

                if (cell.BackColor.R == 255 && cell.BackColor.G == 255 && cell.BackColor.B == 255)
                {
                    gd2.Background = new SolidColorBrush(Colors.White);

                }
                else
                {
                    gd2.Background = new SolidColorBrush(Colors.Gray);
                }
                canvasMono.Children.Add(gd2);

            }



            double topMargin = 0;
            double leftMargin = 0;
            int couter = 0;
            foreach (var p in set.UsedPolyominoEntityList)
            {
                couter++;

                if (p.IsMirror)
                {
                    p.MirrorModeOff();
                }
                p.ToAngle0();

                foreach (var cell in p.PolyCells)
                {

                    int AnswerSizes = 25;
                    Grid gd = new Grid();
                    gd.Width = AnswerSizes;
                    gd.Height = AnswerSizes;
                    gd.SetValue(Canvas.LeftProperty, AnswerSizes * (cell.X + 1) + leftMargin);
                    gd.SetValue(Canvas.TopProperty, AnswerSizes * (cell.Y + 1) + topMargin);
                    var color = System.Windows.Media.Color.FromArgb(cell.BackColor.A, cell.BackColor.R, cell.BackColor.G, cell.BackColor.B);
                    gd.Background = new SolidColorBrush(color);
                    canvasPolyomino.Children.Add(gd);

                }

                if (couter % 2 == 0)
                {
                    topMargin += 180;
                    leftMargin = 0;
                }
                else
                {
                    leftMargin += 180;
                }



            }



        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            canvasMono.Children.Clear();
            canvasPolyomino.Children.Clear();
            SetPolyomino(Make());
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {

            //PrintSinglePage();
            Print(true, cRoot, polyominoSaveFolderPath);
        }


        private void Print(bool printFlag, Canvas canvas, string saveFolderPath)
        {
            try
            {

                Canvas c = canvas;
                var container = VisualTreeHelper.GetParent(c) as FrameworkElement;
                var grid = container as Grid;

                if (container is Grid == true)
                {
                    grid.Children.Remove(canvas);
                    grid.Children.Clear();
                }

                PrintCenter pc = new PrintCenter(c, printFlag, saveFolderPath);

                // 印刷UIを解放する
                if (pc != null)
                {
                    pc.RemoveFixedPageChild();
                }

                if (grid != null)
                    grid.Children.Add(canvas);

            }
            catch (Exception ex)
            {
                MessageBox.Show("印刷時にエラーが発生：" + ex.Message + ex.StackTrace);
            }

        }

        private void btnSaveToImage_Click(object sender, RoutedEventArgs e)
        {
            Print(false, cRoot, polyominoSaveFolderPath);
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            gdProgressBar.Visibility = Visibility.Visible;
            SearchAndSave();
        }

        public async void SearchAndSave()
        {

            // Progressクラスのインスタンスを生成
            IProgress<int> p = new Progress<int>(ShowProgress);

            int counter = 0;
            while (counter < 100)
            {
                counter++;

                int percentage = counter * 100 / 100; // 進捗率
                p.Report(percentage);

                PolyominoSet set = null;

                //タスク内の戻り値がない場合。
                await Task.Run(() =>
                {
                    set = Make();
                });

                canvas.Children.Clear();
                canvasMono.Children.Clear();
                canvasPolyomino.Children.Clear();
                SetPolyomino(set);
                Print(false, cRoot, polyominoSaveFolderPath);

                //Action act = delegate ()
                //{
                //    canvas.Children.Clear();
                //    canvasMono.Children.Clear();
                //    canvasPolyomino.Children.Clear();
                //    Make();
                //    Print(false);
                //};

                //Dispatcher.BeginInvoke(act, System.Windows.Threading.DispatcherPriority.Background);

            }

        }

        // 進捗を表示するメソッド（これはUIスレッドで呼び出される）
        private void ShowProgress(int percent)
        {
            tblProgress.Text = percent + "％完了";
            pb.Value = percent;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var grid = sender as Grid;


            if (((SolidColorBrush)grid.Background).Color.ToString() == Colors.Gray.ToString())
            {
                grid.Background = new System.Windows.Media.SolidColorBrush(Colors.Transparent);
            }
            else
            {
                grid.Background = new System.Windows.Media.SolidColorBrush(Colors.Gray);
            }
        }

        private void btnPrint2_Click(object sender, RoutedEventArgs e)
        {
            Print(true, canvasAnswerSheet, polyominoSaveFolderPath);
        }
    }
}
