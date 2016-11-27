using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace Talk2you.Models
{
    /// <summary>
    /// プロジェクト(キャラクター等全体のデータ)を管理するクラス
    /// </summary>
    public class ProjectManager : NotificationObject
    {
        /// <summary>
        /// 声調のカテゴリ
        /// </summary>
        enum category
       {
            normal,
            whisper,
            angry,
            sad
       }

        /// <summary>
        /// プロジェクトの情報を保存するクラス
        /// </summary>
        class projectInformation
        {   
            string projectName { set; get; }
            string charaName { set; get; }
            string workName { set; get; }
        }

        /// <summary>
        /// １つのセリフ情報を保存するクラス
        /// </summary>
        class serifInformation
        {   
            string identifier { set; get; }
            string plainText { set; get; }
            category category { set; get; }
            int volume { set; get; }
            double start { set; get; }
            double end { set; get; }
            string file { set; get; }
        }

        /// <summary>
        /// 動作中のアプリケーションのファイルパスを返します。
        /// </summary>
        /// <returns>string 動作しているフルパス</returns>
        public string GetLocationPath()
        {   //動作しているパスを返す
            return System.Reflection.Assembly.GetExecutingAssembly().Location;
        }



    }
}
