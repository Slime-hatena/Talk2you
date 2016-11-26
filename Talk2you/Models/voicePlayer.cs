using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

using Livet;
using Microsoft.Win32;

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
    }
}
