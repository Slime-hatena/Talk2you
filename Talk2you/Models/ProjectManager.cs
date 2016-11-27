﻿using System;
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
        Normal,
        Whisper,
        Angry,
        Sad
    }

    /// <summary>
    /// プロジェクトの情報を保存するクラス
    /// </summary>
    public class ProjectInformation
    {
        public string ProjectName { set; get; }
        public string CharaName { set; get; }
        public string WorkName { set; get; }
    }

    /// <summary>
    /// １つのセリフ情報を保存するクラス
    /// </summary>
    public class WordInformation
    {
        public string Identifier { set; get; }
        public string Text { set; get; }
        public VoiceCategory Category { set; get; }
        public int Volume { set; get; }
        public double Start { set; get; }
        public double End { set; get; }
        public string File { set; get; }
    }

    /// <summary>
    /// プロジェクト(キャラクター等全体のデータ)を管理するクラス
    /// </summary>
    public class ProjectManager : NotificationObject
    {

        /// <summary>
        /// 動作中のアプリケーションのファイルパスを返す。
        /// </summary>
        /// <returns>string 動作しているフルパス</returns>
        public static string GetLocationPath()
        {   //動作しているパスを返す
            return System.Reflection.Assembly.GetExecutingAssembly().Location;
        }
    }
}
