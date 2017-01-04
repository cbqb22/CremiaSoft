﻿using System;
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
        public PolyominoQuestionMaker()
        {
            InitializeComponent();
            this.Loaded += PolyominoQuestionMaker_Loaded;
        }

        private void PolyominoQuestionMaker_Loaded(object sender, RoutedEventArgs e)
        {
            SetPolyomino(Make());
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
            Print(true,cRoot);
        }


        private void Print(bool printFlag,Canvas canvas)
        {
            try
            {

                Canvas c = canvas;
                var container = VisualTreeHelper.GetParent(c) as FrameworkElement;

                if(container is Grid == false)
                {
                    return;
                }
                var grid = container as Grid;
                grid.Children.Remove(canvas);
                grid.Children.Clear();

                PrintCenter pc = new PrintCenter(c, printFlag);

                // 印刷UIを解放する
                if (pc != null)
                {
                    pc.RemoveFixedPageChild();
                }

                grid.Children.Add(canvas);

            }
            catch (Exception ex)
            {
                MessageBox.Show("印刷時にエラーが発生：" + ex.Message + ex.StackTrace);
            }

        }

        private void btnSaveToImage_Click(object sender, RoutedEventArgs e)
        {
            Print(false, cRoot);
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

                int percentage = counter  * 100 / 100; // 進捗率
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
                Print(false, cRoot);

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
            Print(true,canvasAnswerSheet);
        }
    }
}