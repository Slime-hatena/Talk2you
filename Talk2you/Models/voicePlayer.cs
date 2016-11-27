using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

using Livet;
using Microsoft.Win32;
using System.Windows.Threading;

namespace Talk2you.Models
{
    public class VoicePlayer : NotificationObject
    {
        /*
         * NotificationObjectはプロパティ変更通知の仕組みを実装したオブジェクトです。
         */

        public string SelectVoiceFile()
        {
            //OpenFileDialogクラスのインスタンスを作成
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = "";
            ofd.InitialDirectory = @"C:\";
            ofd.Filter = "wavファイル(*.wav)|*.wav|すべてのファイル(*.*)|*.*";
            ofd.FilterIndex = 1;
            ofd.Title = "音声ファイルを選択";
            ofd.RestoreDirectory = true;

            bool? result = ofd.ShowDialog();
            if (result == true)
            {
                Console.WriteLine(ofd.FileName);
                return ofd.FileName;
            }
            return null;
        }

        public double SetSliderTime(double nowTime, double maxTime, bool isPrev, double absSlideTime)
        {   //渡された値に移動する 最大/最小を越えるなら範囲内に戻す
            if (isPrev)
            {   //左に動く
                if (nowTime - absSlideTime < 0)     return 0;
                return nowTime - absSlideTime;
            }
            else
            {   //右に動く
                if (nowTime + absSlideTime > maxTime) return maxTime;
                return nowTime + absSlideTime;
            }
        }

    }
}
