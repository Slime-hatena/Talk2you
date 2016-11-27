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

namespace Talk2you.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        /* コマンド、プロパティの定義にはそれぞれ 
         * 
         *  lvcom   : ViewModelCommand
         *  lvcomn  : ViewModelCommand(CanExecute無)
         *  llcom   : ListenerCommand(パラメータ有のコマンド)
         *  llcomn  : ListenerCommand(パラメータ有のコマンド・CanExecute無)
         *  lprop   : 変更通知プロパティ(.NET4.5ではlpropn)
         *  
         * を使用してください。
         * 
         * Modelが十分にリッチであるならコマンドにこだわる必要はありません。
         * View側のコードビハインドを使用しないMVVMパターンの実装を行う場合でも、ViewModelにメソッドを定義し、
         * LivetCallMethodActionなどから直接メソッドを呼び出してください。
         * 
         * ViewModelのコマンドを呼び出せるLivetのすべてのビヘイビア・トリガー・アクションは
         * 同様に直接ViewModelのメソッドを呼び出し可能です。
         */

        /* ViewModelからViewを操作したい場合は、View側のコードビハインド無で処理を行いたい場合は
         * Messengerプロパティからメッセージ(各種InteractionMessage)を発信する事を検討してください。
         */

        /* Modelからの変更通知などの各種イベントを受け取る場合は、PropertyChangedEventListenerや
         * CollectionChangedEventListenerを使うと便利です。各種ListenerはViewModelに定義されている
         * CompositeDisposableプロパティ(LivetCompositeDisposable型)に格納しておく事でイベント解放を容易に行えます。
         * 
         * ReactiveExtensionsなどを併用する場合は、ReactiveExtensionsのCompositeDisposableを
         * ViewModelのCompositeDisposableプロパティに格納しておくのを推奨します。
         * 
         * LivetのWindowテンプレートではViewのウィンドウが閉じる際にDataContextDisposeActionが動作するようになっており、
         * ViewModelのDisposeが呼ばれCompositeDisposableプロパティに格納されたすべてのIDisposable型のインスタンスが解放されます。
         * 
         * ViewModelを使いまわしたい時などは、ViewからDataContextDisposeActionを取り除くか、発動のタイミングをずらす事で対応可能です。
         */

        /* UIDispatcherを操作する場合は、DispatcherHelperのメソッドを操作してください。
         * UIDispatcher自体はApp.xaml.csでインスタンスを確保してあります。
         * 
         * LivetのViewModelではプロパティ変更通知(RaisePropertyChanged)やDispatcherCollectionを使ったコレクション変更通知は
         * 自動的にUIDispatcher上での通知に変換されます。変更通知に際してUIDispatcherを操作する必要はありません。
         */

        VoicePlayer voicePlayer = new VoicePlayer();
        MainWindow ViewSource = (MainWindow)System.Windows.Application.Current.MainWindow;

        //巻き戻し早送り移動秒数指定
        private const double kFastTime = 0.1;
        private const double kslowTime = 0.01;


        #region VoiceFile変更通知プロパティ
        private string _VoiceFile;
        public string VoiceFile
        {
            get
            { return _VoiceFile; }
            set
            {
                if (_VoiceFile == value)
                    return;
                _VoiceFile = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region StartTime変更通知プロパティ
        private double _StartTime;

        public double StartTime
        {
            get
            { return _StartTime; }
            set
            {
                if (_StartTime == value)
                    return;
                _StartTime = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region EndTime変更通知プロパティ
        private double _EndTime;

        public double EndTime
        {
            get
            { return _EndTime; }
            set
            {
                if (_EndTime == value)
                    return;
                _EndTime = value;
                RaisePropertyChanged();
            }
        }
        #endregion
        #region MaximumTime変更通知プロパティ
        private double _MaximumTime;

        public double MaximumTime
        {
            get
            { return _MaximumTime; }
            set
            {
                if (_MaximumTime == value)
                    return;
                _MaximumTime = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        // メソッドここから

        public void Initialize()
        {
            Console.WriteLine("Initialized");
        }

        public void voiceFileSelectButtonClick()
        {   //ファイルの選択からロードまでを行う
            string path = voicePlayer.SelectVoiceFile();
            if (path == null) return;

            VoiceFile = path;
            ViewSource.LoadVoice(VoiceFile);
        }



        public void allPlayButtonClick()
        {   //全再生ボタンをおした時
            ViewSource.LoadVoice(VoiceFile);
            ViewSource.PlayVoice();
        }

        public void MediaOpened()
        {   //メディアを開いたときの処理
            MaximumTime = ViewSource.GetVoiceDurationTime();    //ファイルの最大時間を更新する
        }

        // 送り戻し機能郡
        public void StartFastPrevButton()
        {   //スタート早戻しボタン
            StartTime = voicePlayer.SetSliderTime(StartTime, MaximumTime, true, kFastTime);
        }
        public void StartSlowPrevButton()
        {   //スタート遅戻しボタン
            StartTime = voicePlayer.SetSliderTime(StartTime, MaximumTime, true, kslowTime);
        }
        public void StartSlowNextButton()
        {   //スタート早戻しボタン
            StartTime = voicePlayer.SetSliderTime(StartTime, MaximumTime, false, kslowTime);
        }
        public void StartFastNextButton()
        {   //スタート遅戻しボタン
            StartTime = voicePlayer.SetSliderTime(StartTime, MaximumTime, false, kFastTime);
        }
        public void EndFastPrevButton()
        {   //エンド早戻しボタン
            EndTime = voicePlayer.SetSliderTime(EndTime, MaximumTime, true, kFastTime);
        }
        public void EndSlowPrevButton()
        {   //スタート遅戻しボタン
            EndTime = voicePlayer.SetSliderTime(EndTime, MaximumTime, true, kslowTime);
        }
        public void EndSlowNextButton()
        {   //スタート早戻しボタン
            EndTime = voicePlayer.SetSliderTime(EndTime, MaximumTime, false, kslowTime);
        }
        public void EndFastNextButton()
        {   //スタート遅戻しボタン
            EndTime = voicePlayer.SetSliderTime(EndTime, MaximumTime, false, kFastTime);
        }

        

    }
}

