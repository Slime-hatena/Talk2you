using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace Talk2you.Models
{

    /// <summary>
    /// 声調のカテゴリ
    /// </summary>
    public enum VoiceCategory
    {
        normal,
        whisper,
        angry,
        sad
    }

    /// <summary>
    /// プロジェクト(キャラクター等全体のデータ)を管理するクラス
    /// </summary>
    public class ProjectManager : NotificationObject
    {

        /// <summary>
        /// プロジェクトの情報を保存するクラス
        /// </summary>
        class ProjectInformation
        {   
            string ProjectName { set; get; }
            string CharaName { set; get; }
            string WorkName { set; get; }
        }

        /// <summary>
        /// １つのセリフ情報を保存するクラス
        /// </summary>
        class WordsInformation
        {   
            string Identifier { set; get; }
            string Text { set; get; }
            VoiceCategory Category { set; get; }
            int Volume { set; get; }
            double Start { set; get; }
            double End { set; get; }
            string File { set; get; }
        }

        /// <summary>
        /// 動作中のアプリケーションのファイルパスを返す。
        /// </summary>
        /// <returns>string 動作しているフルパス</returns>
        public string GetLocationPath()
        {   //動作しているパスを返す
            return System.Reflection.Assembly.GetExecutingAssembly().Location;
        }



    }
}
