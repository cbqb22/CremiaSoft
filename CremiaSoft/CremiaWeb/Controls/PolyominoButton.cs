using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



/// <summary>
/// https://msdn.microsoft.com/ja-jp/library/yhzc935f(v=vs.100).aspx
/// このクラスを使う手順はこちら
/// 各フィールドの値は、ViewStateの処理がget,setに入れないと保持されない。
/// 値をViewStateから取ってくるようにする。
/// </summary>
namespace CremiaWeb.Controls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:PolyominoButton runat=server></{0}:PolyominoButton>")]
    public class PolyominoButton : Button
    {


        public PolyominoButton()
        {
        }

        public PolyominoButton(int x,int y,bool isselect,Unit width,Unit height, System.Drawing.Color color)
        {
            X = x;
            Y = y;
            IsSelected = isselect;
            Width = width;
            Height = height;
            BackColor = color;

        }

        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(0)]
        [Description("マスが選択されているか")]
        [Localizable(true)]
        public bool IsSelected
        {
            get
            {
                //ViewStateを保持する為の処理
                //これがないと、再描画して前の状態（値）が初期化されてしまう。
                if (ViewState["IsSelected"] == null)
                {
                    return false;
                }
                else
                {
                    bool s = (bool)ViewState["IsSelected"];
                    return s;
                }
            }
            set
            {
                ViewState["IsSelected"] = value;
            }
        }

        //X座標
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(0)]
        [Description("X座標")]
        [Localizable(true)]
        public int X
        {
            get
            {

                if (ViewState["X"] == null)
                {
                    return 0;
                }
                else
                {
                    int s = (int)ViewState["X"];
                    return s;
                }
            }
            set
            {
                ViewState["X"] = value;
            }
        }

        //Y座標
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(0)]
        [Description("Y座標")]
        [Localizable(true)]
        public int Y
        {
            get
            {

                if (ViewState["Y"] == null)
                {
                    return 0;
                }
                else
                {
                    int s = (int)ViewState["Y"];
                    return s;
                }
            }
            set
            {
                ViewState["Y"] = value;
            }
        }


        //ボタンの幅
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(0)]
        [Description("ボタンの幅")]
        [Localizable(true)]
        public override Unit Width
        {
            get
            {

                if (ViewState["Width"] == null)
                {
                    return 0;
                }
                else
                {
                    Unit i = (Unit)ViewState["Width"];
                    return i;
                }
            }
            set
            {
                ViewState["Width"] = value;
                base.Width = Width;
            }
        }

        //ボタンの幅
        [Bindable(true)]
        [Category("Appearance")]
        [DefaultValue(0)]
        [Description("ボタンの高さ")]
        [Localizable(true)]
        public override Unit Height
        {
            get
            {

                if (ViewState["Height"] == null)
                {
                    return 0;
                }
                else
                {
                    Unit i = (Unit)ViewState["Height"];
                    return i;
                }
            }
            set
            {
                ViewState["Height"] = value;
                base.Height = Height;
            }
        }



    }

}
