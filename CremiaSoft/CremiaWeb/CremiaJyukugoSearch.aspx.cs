using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CremiaViewModel.Routine;
using CremiaViewModel.Entity;

namespace CremiaWeb
{
    public partial class CremiaJyukugoSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadComplete += CremiaJyukugoSearch_LoadComplete;
        }

        void CremiaJyukugoSearch_LoadComplete(object sender, EventArgs e)
        {
            
        }

        private void Reset()
        {
            ClearLabels();
            ClearTextBox();
        }

        private void ClearTextBox()
        {
            tbxA.Text = "";
            tbxB.Text = "";
            tbxC.Text = "";
            tbxD.Text = "";
        }

        private void ClearLabels()
        {
            lb.Text = "";
            lbResults.Text = "";
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ClearLabels();

            TwoCharacterPhraseLocationEnum emptyLocation = TwoCharacterPhraseLocationEnum.None;

            int count = 0;

            if (tbxA.Text.Length == 1)
            {
                count++;
            }
            else
            {
                emptyLocation = TwoCharacterPhraseLocationEnum.A;
            }

            if (tbxB.Text.Length == 1)
            {
                count++;
            }
            else
            {
                emptyLocation = TwoCharacterPhraseLocationEnum.B;
            }
            if (tbxC.Text.Length == 1)
            {
                count++;
            }
            else
            {
                emptyLocation = TwoCharacterPhraseLocationEnum.C;
            }
            if (tbxD.Text.Length == 1)
            {
                count++;
            }
            else
            {
                emptyLocation = TwoCharacterPhraseLocationEnum.D;
            }


            //一つ埋まっていないのはOK
            if (count == 0)
            {

                string cScript = "alert('上下左右の４マスにそれぞれ検索したい漢字を１文字を入力して下さい。');"; //描画前
                ClientScript.RegisterClientScriptBlock(this.GetType(), "key", cScript, true);
                //string sScript = "alert('StartupScript');";//描画後
                //ClientScript.RegisterStartupScript(this.GetType(), "key", sScript, true);

                //lbResults.Text = "上下左右の４マスにそれぞれ検索したい漢字を１文字を入力して下さい。";

                //         string script =
                //"<script language=javascript>" +
                //"window.alert('上下左右の４マスにそれぞれ検索したい漢字を\r\n１文字を入力して下さい。')" +
                //"</script>";
                //         Response.Write(script);

                return;
            }
            else if (count < 3)
            {
                string cScript = "alert('上下左右のマスのうち、最低でも３マスは入力してください。');"; //描画前
                ClientScript.RegisterClientScriptBlock(this.GetType(), "key", cScript, true);

                //lbResults.Text = "上下左右のマスのうち、最低でも３マスは入力してください。";


                return;

            }

            

            var result = TwoCharacterPhraseSearcher.Search
                (
                new TwoCharacterPhraseEntity(emptyLocation == TwoCharacterPhraseLocationEnum.A ? ' ' : tbxA.Text.ToCharArray(0,1)[0],' '),
                    new TwoCharacterPhraseEntity(emptyLocation == TwoCharacterPhraseLocationEnum.B ? ' ' : tbxB.Text.ToCharArray(0, 1)[0], ' '),
                    new TwoCharacterPhraseEntity(' ', emptyLocation == TwoCharacterPhraseLocationEnum.C ? ' ' : tbxC.Text.ToCharArray(0, 1)[0]),
                    new TwoCharacterPhraseEntity(' ', emptyLocation == TwoCharacterPhraseLocationEnum.D ? ' ' : tbxD.Text.ToCharArray(0, 1)[0]),
                    emptyLocation
                );

            //result.Add(result[0]);

            if (result.Count == 0)
            {
                lbResults.Text = "該当する熟語が見つかりませんでした。";
                return;
            }

            lb.Text = result[0].CommonCharacter.ToString();

            
            //もし複数ある場合は他の候補を表示する
            if (1 < result.Count)
            {
                result.RemoveAt(0);

                string s = "他にも、";

                Action<TwoCharacterPhraseFourEntity> showMethod = delegate(TwoCharacterPhraseFourEntity x) 
                {
                    s += string.Format("【{0}】", x.CommonCharacter.ToString());
                };

                result.ForEach(showMethod);
                s += "　があります。";

                lbResults.Text = string.Format(s);
            }
        }


    }
}