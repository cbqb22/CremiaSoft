using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CremiaViewModel.Entity.Polyomino;


namespace CremiaViewModel.Routine.Polyomino
{
    public static class PolyominoAnalysis
    {

        /// <summary>
        /// ※※※※※※※※※※※※※※※※※※※※※※※※※※※※
        /// ※※※※※※※※※※※※※※※※※※※※※※※※※※※※
        /// ※※※※　　　　　　　　　　　　　　　　　　　　※※※※
        /// ※※※※　完成しているので、原則いじらないこと　※※※※
        /// ※※※※　　　　　　　　　　　　　　　　　　　　※※※※
        /// ※※※※※※※※※※※※※※※※※※※※※※※※※※※※
        /// ※※※※※※※※※※※※※※※※※※※※※※※※※※※※
        ///
        /// ポリオミノを描画に当てはめるロジックエントリーポイント
        /// </summary>
        /// <param name="polyominoList">ポリオミノのリスト</param>
        /// <param name="drawCellEntity">描画マス枠</param>
        /// <param name="IncludingMirror">反転を考慮するか</param>
        /// <returns>結果</returns>
        public static PolyominoSet Analysis(List<PolyominoEntity> polyominoList, DrawCellAreaEntity drawCellEntity, bool IncludingMirror, bool IncludingRotation)
        {

            PolyominoSet set = new PolyominoSet();


            //固定した図形を格納
            List<PolyominoEntity> usedPolyomino = new List<PolyominoEntity>();

            //再帰的に呼び出す
            if (CheckPolyominoMatch(polyominoList, usedPolyomino, drawCellEntity,IncludingMirror,IncludingRotation))
            {
                //全て一致

                set.DrawCellAreaEntity = drawCellEntity;
                set.UsedPolyominoEntityList = usedPolyomino;
                return set;
            }
            else
            {
                //一致なし
                return null;
            }
        }

        /// <summary>
        /// ポリオミノマッチの回帰的ロジック
        /// </summary>
        /// <param name="polyominoList">ポリオミノのリスト</param>
        /// <param name="usedPolyomino">使用済みのポリオミノリスト</param>
        /// <param name="drawCellEntity">描画マス枠</param>
        /// <param name="IncludingMirror">反転を考慮するか</param>
        /// <returns></returns>
        public static bool CheckPolyominoMatch(List<PolyominoEntity> polyominoList, List<PolyominoEntity> usedPolyomino, DrawCellAreaEntity drawCellEntity, bool IncludingMirror,bool IncludingRotation)
        {
            //次の図形へ
            foreach (var polysub in polyominoList)
            {

                //同じ図形は２度使わない。
                if (usedPolyomino.Contains(polysub))
                {
                    continue;
                }

                //反転モード用のカウンター
                int MirrorCounter = 0;

                do
                {
                    //回転用のカウンター
                    int RotateCounter = 0;

                    do
                    {

                        //図形のひとつずつセルを動かす
                        foreach (var cellsub in polysub.PolyCells)
                        {
                            //左上の空白となるセル(利用不可領域対応)
                            CellEntity leftUpperEmpty = drawCellEntity.EmptyCellLeftUpper();


                            //左上に合わせてはみ出しチェックと重なりチェック
                            if (!はみ出しと重なりチェック(drawCellEntity, polysub, leftUpperEmpty, cellsub))
                            {
                                continue;
                            }


                            //図形の固定(描画エリアを塗りつぶす)
                            描画エリアを塗りつぶす(drawCellEntity, polysub, leftUpperEmpty, cellsub);

                            //使用済みに登録
                            usedPolyomino.Add(polysub);


                            //全てエリアが塗りつぶされるかつ、全てのポリオミノが使われる
                            if (drawCellEntity.IsAllCellFilledOrDisabled && usedPolyomino.Count == polyominoList.Count)
                            {
                                return true;
                            }


                            //再帰的に呼び出す
                            if (CheckPolyominoMatch(polyominoList, usedPolyomino, drawCellEntity,IncludingMirror,IncludingRotation))
                            {
                                //全て一致
                                return true;
                            }
                            else
                            {
                                //一致でない場合は継続
                                //この階層で固定したものがあれば開放
                                usedPolyomino.Remove(polysub);
                                描画エリアを空白にもどす(drawCellEntity, polysub, leftUpperEmpty, cellsub);
                                continue;
                            }
                        }

                        //回転
                        if(IncludingRotation)
                        {
                            polysub.Add90Angle();
                        }
                        else
                        {
                            break;
                        }


                    }
                    while (++RotateCounter < 4);

                    //角度は０に戻す
                    if(IncludingRotation)
                    {
                        polysub.ToAngle0();
                    }

                    ////反転考慮しない場合は抜ける
                    if (!IncludingMirror)
                    {
                        break;
                    }

                    //反転させる
                    if (!polysub.IsMirror)
                    {
                        polysub.MirrorModeOn();
                    }
                }
                while (++MirrorCounter < 2); //ミラーモードは一度だけ

                //ミラーモードはオフに戻しておく
                if (IncludingMirror)
                {
                    polysub.MirrorModeOff();
                }

                //角度は０に戻す
                if (IncludingRotation)
                {
                    polysub.ToAngle0();
                }




            }

            return false;




        }

        /// <summary>
        /// 各種チェック
        /// 図形が描画エリアをはみ出さないか
        /// 図形が重ならないか
        /// </summary>
        /// <param name="drawCellEntity"></param>
        /// <param name="polyEntity"></param>
        /// <param name="leftUpperEmpty"></param>
        /// <param name="basecell"></param>
        /// <returns></returns>
        public static bool はみ出しと重なりチェック(DrawCellAreaEntity drawCellEntity, PolyominoEntity polyEntity, CellEntity leftUpperEmpty, CellEntity basecell)
        {
            //左上に合わせる差分を計算
            double sabunX = basecell.X;
            double sabunY = basecell.Y;

            ////左上のセル座標
            //CellEntity leftUpperEmpty = drawCellEntity.EmptyCellLeftUpper();

            foreach (var cell in polyEntity.PolyCells)
            {
                if (cell == basecell)
                {
                    continue;
                }

                ////基準セルを左上セルに合わせたあとのその他のセルの座標
                double afterX = leftUpperEmpty.X + cell.X - sabunX;
                double afterY = leftUpperEmpty.Y + cell.Y - sabunY;


                //はみだしチェック
                if (afterX < 0 || afterY < 0)
                {
                    return false;
                }

                //重なりチェック
                var afterCell = drawCellEntity.GetCellByXY(afterX, afterY);

                //はみだし
                if (afterCell == null)
                {
                    return false;
                }

                //利用不可領域
                if (afterCell.Enable == false)
                {
                    return false;
                }

                //既に描画
                if (afterCell.IsFilled)
                {
                    return false;
                }





            }

            return true;



        }


        /// <summary>
        /// チェック成功時の描画エリアを塗りつぶし操作
        /// </summary>
        /// <param name="drawCellEntity"></param>
        /// <param name="polyEntity"></param>
        /// <param name="leftUpperEmpty"></param>
        /// <param name="basecell"></param>
        public static void 描画エリアを塗りつぶす(DrawCellAreaEntity drawCellEntity, PolyominoEntity polyEntity, CellEntity leftUpperEmpty, CellEntity basecell)
        {

            foreach (var cell in polyEntity.PolyCells)
            {
                var target = drawCellEntity.GetCellByXY(leftUpperEmpty.X + cell.X - basecell.X, leftUpperEmpty.Y + cell.Y - basecell.Y);
                target.BackColor = cell.BackColor;
                target.IsFilled = true;
            }
        }

        /// <summary>
        /// 描画エリアを塗りつぶしを空白に戻す操作
        /// 基本的に塗りつぶし操作のロールバックに使うためのもので引数は同じものを使う
        /// </summary>
        /// <param name="drawCellEntity"></param>
        /// <param name="polyEntity"></param>
        /// <param name="leftUpperEmpty"></param>
        /// <param name="basecell"></param>
        public static void 描画エリアを空白にもどす(DrawCellAreaEntity drawCellEntity, PolyominoEntity polyEntity, CellEntity leftUpperEmpty, CellEntity basecell)
        {
            foreach (var cell in polyEntity.PolyCells)
            {
                var target = drawCellEntity.GetCellByXY(leftUpperEmpty.X + cell.X - basecell.X, leftUpperEmpty.Y + cell.Y - basecell.Y);
                target.BackColor = System.Drawing.Color.White;
                target.IsFilled = false;
            }
        }


    }
}
