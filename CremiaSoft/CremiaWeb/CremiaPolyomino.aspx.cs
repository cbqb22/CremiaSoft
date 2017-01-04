using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using CremiaWeb.Controls;
using CremiaViewModel.Routine.Polyomino;
using CremiaViewModel.Entity.Polyomino;

namespace CremiaWeb
{
    public partial class CreamiPolyomino : System.Web.UI.Page
    {
        /// <summary>
        /// ビューステートが読み込まれる前
        /// 毎回作る動的なオブジェクト
        /// 動的なものは毎回作らないとviewstateが働かない
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreLoad(object sender, EventArgs e)
        {
            SetPanelFromDdls();
            SetPanelFromDdlsForDrawArea();
            MakePolyomino();     //セッションを利用して再構築

            btnPolyominoAdd.Focus();
        }

        /// <summary>
        /// 色辞書
        /// </summary>
        private Dictionary<int, Color> colors = new Dictionary<int, Color> {
            { 0, Color.Green },
            { 1, Color.Cyan },
            { 2, Color.Brown },
            { 3, Color.Yellow },
            { 4, Color.Tomato },
            { 5, Color.Purple },
            { 6, Color.Pink },
            { 7, Color.Navy },
            { 8, Color.LightSkyBlue },
            { 9, Color.Maroon },
            { 10, Color.MediumSlateBlue },
            { 11, Color.Gainsboro },
            { 12, Color.PaleGoldenrod },
            { 13, Color.Thistle },
            { 14, Color.DodgerBlue },
            { 15, Color.CadetBlue },
        };

        protected void Page_Load(object sender, EventArgs e)
        {

            //初回時
            if (!IsPostBack)
            {

            }
            //ポストバック時
            else
            {
            }




            //SetPanelFromDdls();

            //MakePolyomino();

        }



        /// <summary>
        /// Pageアンロード
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_UnLoad(object sender, EventArgs e)
        {
        }



        /// <summary>
        /// ポリオミノを追加
        /// </summary>
        private void AddPolyomino()
        {

            int? minX = null;
            int? maxX = null;
            int? minY = null;
            int? maxY = null;

            List<object> list = new List<object>();
            FindObject<PolyominoButton>(pan, ref list);

            System.Drawing.Color backcolor = System.Drawing.Color.White;

            //該当するxyのリスト
            var xys = Enumerable.Range(0, 0).Select(x => new { X = 0, Y = 0 }).ToList();

            foreach (var c in list)
            {
                if (c is PolyominoButton)
                {
                    var pb = c as PolyominoButton;

                    if (pb.IsSelected)
                    {

                        if (backcolor == System.Drawing.Color.White)
                        {
                            backcolor = pb.BackColor;
                        }

                        if (minX == null)
                        {
                            minX = pb.X;
                        }
                        else if (pb.X < minX)
                        {
                            minX = pb.X;
                        }


                        if (maxX == null)
                        {
                            maxX = pb.X;
                        }
                        else if (maxX < pb.X)
                        {
                            maxX = pb.X;
                        }

                        if (minY == null)
                        {
                            minY = pb.Y;
                        }
                        else if (pb.Y < minY)
                        {
                            minY = pb.Y;
                        }

                        if (maxY == null)
                        {
                            maxY = pb.Y;
                        }
                        else if (maxY < pb.Y)
                        {
                            maxY = pb.Y;
                        }

                        var xy = new { pb.X, pb.Y };
                        xys.Add(xy);


                    }

                }
            }






            if (minX == null || maxX == null || minY == null || maxY == null)
            {
                //作成不可のメッセージを出す
                return;
            }

            Panel p0 = new Panel();
            p0.CssClass = "PolyominoContainer";

            int xvalue = (int)maxX - (int)minX;
            int yvalue = (int)maxY - (int)minY;



            //Y軸
            int counter = 0;
            int rowcounter = 0;
            for (int y = 0; y < yvalue + 1; y++)
            {
                Panel p = new Panel();
                rowcounter++;

                //X軸
                for (int x = 0; x < xvalue + 1; x++)
                {
                    bool match = false;
                    xys.ForEach
                        (
                            (xy) =>
                            {
                                if (!match)
                                {
                                    if (xy.X - minX == x && xy.Y - minY == y)
                                    {
                                        match = true;
                                    }
                                }
                            }
                        );

                    counter++;

                    //p.ID = "polychild" + x.ToString() + y.ToString();
                    PolyominoButton pb = new PolyominoButton(x, y, match, 30, 30, match ? backcolor : System.Drawing.Color.White);
                    //pb.ID = "polychildbtn" + x.ToString() + y.ToString();
                    if (rowcounter == 1)
                    {
                        //最初のセルだけ×を入れるとなぜかタブレットでみると段差ができてしまう為、やむをえなく空白文字をいれている。
                        if (counter == 1)
                        {
                            pb.Text = "×";
                            pb.ForeColor = Color.Red;
                            pb.Font.Size = 15;
                            pb.Font.Bold = true;
                            pb.Click += btnPolyominoDelete_Click; //クリックしたら削除

                        }
                        else
                        {
                            pb.Text = "　";
                            pb.Font.Size = 15;
                            pb.OnClientClick = "return false;"; //その他のボタンはポストバックさせない
                        }


                    }
                    else
                    {
                        pb.OnClientClick = "return false;"; //その他のボタンはポストバックさせない
                    }

                    p.Controls.Add(pb);
                }

                p0.Controls.Add(p);

            }

            panelPolyominoStack.Controls.Add(p0);

            //カラーインデックスを１進める
            int index = 0;
            if (Session["CurrentColorIndex"] != null && Session["CurrentColorIndex"] is int)
            {
                index = (int)Session["CurrentColorIndex"];
            }

            if (index == colors.Count - 1)
            {
                Session["CurrentColorIndex"] = 0;
            }
            else
            {
                Session["CurrentColorIndex"] = ++index;

            }




        }


        /// <summary>
        /// ポリオミノ達を再構築
        /// </summary>
        private void MakePolyomino()
        {
            var stack = Session["MakePolyomino"] as Panel;

            if (stack == null)
                return;


            foreach (var child in stack.Controls)
            {
                if (!(child is Panel))
                {
                    continue;
                }


                int? minX = null;
                int? maxX = null;
                int? minY = null;
                int? maxY = null;

                List<object> list = new List<object>();
                FindObject<PolyominoButton>((Panel)child, ref list);

                System.Drawing.Color backcolor = System.Drawing.Color.White;

                //該当するxyのリスト
                var xys = Enumerable.Range(0, 0).Select(x => new { X = 0, Y = 0 }).ToList();

                foreach (var c in list)
                {
                    if (c is PolyominoButton)
                    {
                        var pb = c as PolyominoButton;

                        if (pb.IsSelected)
                        {
                            if (backcolor == System.Drawing.Color.White)
                            {
                                backcolor = pb.BackColor;
                            }


                            if (minX == null)
                            {
                                minX = pb.X;
                            }
                            else if (pb.X < minX)
                            {
                                minX = pb.X;
                            }


                            if (maxX == null)
                            {
                                maxX = pb.X;
                            }
                            else if (maxX < pb.X)
                            {
                                maxX = pb.X;
                            }

                            if (minY == null)
                            {
                                minY = pb.Y;
                            }
                            else if (pb.Y < minY)
                            {
                                minY = pb.Y;
                            }

                            if (maxY == null)
                            {
                                maxY = pb.Y;
                            }
                            else if (maxY < pb.Y)
                            {
                                maxY = pb.Y;
                            }

                            var xy = new { pb.X, pb.Y };
                            xys.Add(xy);


                        }

                    }
                }






                if (minX == null || maxX == null || minY == null || maxY == null)
                {
                    //作成不可のメッセージを出す
                    return;
                }

                Panel p0 = new Panel();
                p0.CssClass = "PolyominoContainer";

                int xvalue = (int)maxX - (int)minX;
                int yvalue = (int)maxY - (int)minY;



                //Y軸
                int counter = 0;
                int rowcounter = 0;
                for (int y = 0; y < yvalue + 1; y++)
                {
                    Panel p = new Panel();
                    rowcounter++;

                    //X軸
                    for (int x = 0; x < xvalue + 1; x++)
                    {
                        bool match = false;
                        xys.ForEach
                            (
                                (xy) =>
                                {
                                    if (!match)
                                    {
                                        if (xy.X - minX == x && xy.Y - minY == y) //0,0座標に合わせて評価
                                        {
                                            match = true;
                                        }
                                    }
                                }
                            );
                        counter++;

                        //p.ID = "polychild" + x.ToString() + y.ToString();
                        PolyominoButton pb = new PolyominoButton(x, y, match, 30, 30, match ? backcolor : System.Drawing.Color.White);
                        //pb.ID = "polychildbtn" + x.ToString() + y.ToString();
                        if (rowcounter == 1)
                        {
                            //最初のセルだけ×を入れるとなぜかタブレットでみると段差ができてしまう為、やむをえなく空白文字をいれている。
                            if (counter == 1)
                            {
                                pb.Text = "×";
                                pb.ForeColor = Color.Red;
                                pb.Font.Size = 15;
                                pb.Font.Bold = true;
                                pb.Click += btnPolyominoDelete_Click; //クリックしたら削除

                            }
                            else
                            {
                                pb.Text = "　";
                                pb.Font.Size = 15;
                                pb.OnClientClick = "return false;"; //その他のボタンはポストバックさせない
                            }

                        }
                        else
                        {
                            pb.OnClientClick = "return false;"; //その他のボタンはポストバックさせない
                        }

                        p.Controls.Add(pb);


                    }

                    p0.Controls.Add(p);

                }

                panelPolyominoStack.Controls.Add(p0);
            }



        }

        /// <summary>
        /// 全ての図形登録用のセルを未選択に戻す
        /// </summary>
        private void SetAllCellClear()
        {
            List<object> list = new List<object>();
            FindObject<PolyominoButton>(pan, ref list);

            foreach (var c in list)
            {
                if (c is PolyominoButton)
                {
                    var pb = c as PolyominoButton;
                    pb.IsSelected = false;
                    pb.BackColor = System.Drawing.Color.White;
                }

            }
        }

        /// <summary>
        /// 指定のTypeのコントロールを取得
        /// 子要素も探す
        /// </summary>
        /// <param name="control"></param>
        /// <param name="t"></param>
        /// <param name="result"></param>
        private void FindObject<T>(Control control, ref List<object> result) where T : class
        {
            if (result == null)
            {
                result = new List<object>();
            }


            foreach (var c in control.Controls)
            {
                if (c is T)
                {
                    result.Add(c);
                }

                if (c is Control)
                {
                    Control sub = c as Control;
                    if (sub.Controls.Count != 0)
                    {
                        FindObject<T>(sub, ref result);
                    }
                }

            }

        }


        /// <summary>
        /// 図形登録用
        /// </summary>
        private void SetPanelFromDdls()
        {

            DropDownList ddlx = ddlX;
            int xvalue = int.Parse(ddlx.SelectedValue);
            DropDownList ddly = ddlY;
            int yvalue = int.Parse(ddly.SelectedValue);
            pan.Controls.Clear();

            //Y軸
            for (int y = 0; y < yvalue; y++)
            {
                Panel p = new Panel();

                //X軸
                for (int x = 0; x < xvalue; x++)
                {

                    //p.ID = "panelchild" + x.ToString() + y.ToString();
                    PolyominoButton pb = new PolyominoButton(x, y, false, 30, 30, System.Drawing.Color.White);
                    //pb.ID = "pbtn" + x.ToString() + y.ToString();
                    pb.Click += Pb_Click;

                    p.Controls.Add(pb);
                }

                pan.Controls.Add(p);

            }

        }

        /// <summary>
        /// 描画エリア用
        /// </summary>
        private void SetPanelFromDdlsForDrawArea()
        {

            DropDownList ddlx = ddlX_drawArea;
            int xvalue = int.Parse(ddlx.SelectedValue);
            DropDownList ddly = ddlY_drawArea;
            int yvalue = int.Parse(ddly.SelectedValue);
            pan_drawArea.Controls.Clear();

            //Y軸
            for (int y = 0; y < yvalue; y++)
            {
                Panel p = new Panel();
                //p.ID = "panelchild" + x.ToString() + y.ToString();

                //X軸
                for (int x = 0; x < xvalue; x++)
                {

                    PolyominoButton pb = new PolyominoButton(x, y, true, 30, 30, System.Drawing.Color.Red); //DrawAreaはIsSelectedをtrue
                    //pb.ID = "pbtn" + x.ToString() + y.ToString();
                    pb.Click += PbDrawArea_Click;
                    p.Controls.Add(pb);
                }

                pan_drawArea.Controls.Add(p);

            }

        }


        protected void Pb_Click(object sender, EventArgs e)
        {
            var b = sender as PolyominoButton;
            if (b == null)
                return;

            int index = 0;
            if (Session["CurrentColorIndex"] != null && Session["CurrentColorIndex"] is int)
            {
                index = (int)Session["CurrentColorIndex"];
            }

            if (b.IsSelected)
            {
                b.IsSelected = false;
                b.BackColor = System.Drawing.Color.White;
            }
            else
            {
                b.IsSelected = true;
                b.BackColor = colors[index];

            }
        }

        protected void PbDrawArea_Click(object sender, EventArgs e)
        {
            var b = sender as PolyominoButton;
            if (b == null)
                return;


            if (b.IsSelected)
            {
                b.IsSelected = false;
                b.BackColor = System.Drawing.Color.White;
            }
            else
            {
                b.IsSelected = true;
                b.BackColor = System.Drawing.Color.Red;

            }
        }

        protected void ddl_SelectedIndexChanged(object sender, EventArgs e)
        {
            var s = sender as DropDownList;
            if (s == null)
                return;

            SetPanelFromDdls();


        }



        protected void ddl_drawArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            var s = sender as DropDownList;
            if (s == null)
                return;

            SetPanelFromDdlsForDrawArea();

        }


        protected void btnPolyominoAdd_Click(object sender, EventArgs e)
        {
            AddPolyomino();
            Session["MakePolyomino"] = panelPolyominoStack; //セッションに入れておいて、PreLoadで再構築

            SetAllCellClear();

        }

        protected void btnPolyominoDelete_Click(object sender, EventArgs e)
        {
            var s = sender as Button;
            if (s == null)
                return;


            //押されたボタンが含んでいるパネルを削除
            bool find = false;
            Panel container = null;
            foreach (Panel p in panelPolyominoStack.Controls)
            {
                foreach (Panel psub in p.Controls)
                {
                    List<object> list = new List<object>();
                    FindObject<PolyominoButton>(psub, ref list);
                    if (list.Contains(s))
                    {
                        find = true;
                        container = p;
                        break;
                    }
                }

                if (find)
                    break;

            }

            panelPolyominoStack.Controls.Remove(container);

            Session["MakePolyomino"] = panelPolyominoStack; //セッションに入れておいて、PreLoadで再構築

        }

        /// <summary>
        /// この実行前にOnClientClickを呼び出している。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPolyominoStart_Click(object sender, EventArgs e)
        {

            var s = sender as Button;
            if (s == null)
                return;

            try
            {

                List<PolyominoEntity> polyominoList = new List<PolyominoEntity>();

                int polyominoFilledCellCount = 0;
                foreach (Panel p in panelPolyominoStack.Controls)
                {
                    List<object> list = new List<object>();
                    FindObject<PolyominoButton>(p, ref list);

                    List<CellEntity> polycells = new List<CellEntity>();
                    foreach (PolyominoButton pb in list)
                    {
                        if (pb.IsSelected)
                        {
                            CellEntity ce = new CellEntity(pb.X, pb.Y, pb.BackColor, false, true);
                            //ce.X = pb.X;
                            //ce.Y = pb.Y;
                            //ce.BackColor = pb.BackColor;

                            polycells.Add(ce);
                            polyominoFilledCellCount++;
                        }

                    }

                    PolyominoEntity pe = new PolyominoEntity(polycells, 0);
                    polyominoList.Add(pe);

                }

                List<Tuple<int, int>> disableAreaList = new List<Tuple<int, int>>();
                List<object> lis = new List<object>();
                FindObject<PolyominoButton>(pan_drawArea, ref lis);

                //描画エリアの有効セルの数
                int enableCellCount = 0;

                foreach (PolyominoButton pb in lis)
                {
                    if (!pb.IsSelected)
                    {
                        Tuple<int, int> tp = new Tuple<int, int>(pb.X, pb.Y);
                        disableAreaList.Add(tp);
                    }
                    else
                    {
                        enableCellCount++;
                    }
                }

                DrawCellAreaEntity drawCellEntity = new DrawCellAreaEntity(int.Parse(ddlX_drawArea.SelectedValue), int.Parse(ddlY_drawArea.SelectedValue), disableAreaList);

                //カウントチェック
                if (enableCellCount != polyominoFilledCellCount)
                {
                    string message = string.Format("描画エリアの有効セル数({0})と図形の合計セル数({1})が一致しません。\\n処理を中止します。", enableCellCount, polyominoFilledCellCount);
                    string cScript = "alert('" + message + "');";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "key", cScript, true);

                    //string sScript = "alert('StartupScript');";
                    //ClientScript.RegisterStartupScript(this.GetType(), "key", sScript, true);

                    return;
                }



                var result = PolyominoAnalysis.Analysis(polyominoList, drawCellEntity, cbxMirrorOn.Checked, cbxRotationOn.Checked);

                //一致結果があれば、描画に表現する
                if (result != null)
                {
                    List<object> list = new List<object>();
                    FindObject<PolyominoButton>(pan_drawArea, ref list);

                    foreach (PolyominoButton pb in list)
                    {
                        pb.IsSelected = true;
                        pb.BackColor = result.DrawCellAreaEntity.GetCellByXY(pb.X, pb.Y).BackColor;
                    }
                }
                else
                {
                    string cScript = "alert('解析しましたが、マッチするパターンが存在しませんでした。。');";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "key", cScript, true);

                }
            }
            catch (Exception ex)
            {
                string message = string.Format("エラーが発生しました。\\n処理を中止します。\\n\\nError:{0}\\n\\nStackTrace:{1}", ex.Message, ex.StackTrace);
                string cScript = "alert('" + message + "');";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "key", cScript, true);

            }
            //finally
            //{

            //}

        }

        /// <summary>
        /// クリアボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClearDrawArea_Click(object sender, EventArgs e)
        {
            var s = sender as Button;
            if (s == null)
                return;


            List<object> lis = new List<object>();
            FindObject<PolyominoButton>(pan_drawArea, ref lis);


            foreach (PolyominoButton pb in lis)
            {
                pb.IsSelected = true;
                pb.BackColor = Color.Red;
            }


        }
    }
}