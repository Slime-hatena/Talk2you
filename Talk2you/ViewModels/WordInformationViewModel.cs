using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Livet;
using Livet.Commands;
using Livet.Messaging;
using Livet.Messaging.IO;
using Livet.EventListeners;
using Livet.Messaging.Windows;

using Talk2you.Models;
using Talk2you.Views;
using System.Windows;
using System.Collections.ObjectModel;

namespace Talk2you.ViewModels
{
    /// <summary>
    /// １つのセリフ情報を保存するクラス
    /// </summary>
    public class WordInformationViewModel : ViewModel
    {
        public MainWindowViewModel vm = new MainWindowViewModel();
        public VoicePlayer voicePlayer = new VoicePlayer();
        public ProjectManager projectManager = new ProjectManager();
        public MainWindow ViewSource = (MainWindow)Application.Current.MainWindow;

        public WordInformation Item { get; set; }


        /// <summary>
        /// データグリッドのコンテキストメニューの再生をおした時
        /// </summary>
        public void DataGridClickPlay()
        {

        }

        /// <summary>
        /// dataGridに登録されている項目を編集出来るよう各変数にセットし直す
        /// </summary>
        public void DataGridClickEdit()
        {
            vm.Identifier = Item.Identifier;
            vm.VoiceText = Item.Text;
            vm.Category = Item.Category;
            vm.VolumeChange = Item.Volume;
            vm.StartTime = Item.Start;
            vm.EndTime = Item.End;
            vm.VoiceFile = Item.File;

            //todo 12/4 何故かviewに反映されない
        }

        /// <summary>
        /// dataGridに登録されている項目を削除する
        /// </summary>
        public void DataGridClickDelete()
        {
            Console.WriteLine("てすと");
        }
    }

}

