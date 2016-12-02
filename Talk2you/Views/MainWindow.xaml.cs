using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Talk2you.Models;
using Talk2you.ViewModels;

namespace Talk2you.Views
{
    /* 
	 * ViewModelからの変更通知などの各種イベントを受け取る場合は、PropertyChangedWeakEventListenerや
     * CollectionChangedWeakEventListenerを使うと便利です。独自イベントの場合はLivetWeakEventListenerが使用できます。
     * クローズ時などに、LivetCompositeDisposableに格納した各種イベントリスナをDisposeする事でイベントハンドラの開放が容易に行えます。
     * WeakEventListenerなので明示的に開放せずともメモリリークは起こしませんが、できる限り明示的に開放するようにしましょう。
     */

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// MainWindow内のmediaElementの操作に使用
    /// </summary>
    //mediaElementはmodel側では制御できそうに無いのでコードビハインド側で制御することにする。

    public partial class MainWindow : Window
    {



        /// <summary>
        /// 初期化処理
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            InitializeMediaElement();
        }

        /// <summary>
        /// mediaElementの初期化処理
        /// </summary>
        void InitializeMediaElement()
        {
            //mediaElementを対話操作可能にする。
            mediaElement.LoadedBehavior = MediaState.Manual;
        }

        /// <summary>
        /// 音声ファイルのロードを行う
        /// </summary>
        /// <param name="filePath">音声ファイルのフルパス</param>
        public void LoadVoice(string filePath)
        {
            try
            {
                mediaElement.Source = new Uri(filePath, UriKind.Absolute);
            }
            catch (UriFormatException e)
            {
                MessageBox.Show("指定が間違っています。");
                Console.WriteLine(e);
            }
            catch (ArgumentNullException e)
            {
                MessageBox.Show("何かを入力してください。");
                Console.WriteLine(e);
            }
            alertEventMediaOpened();
        }

        /// <summary>
        /// 開いたことをイベントハンドラで受け取りたいので一旦ポーズするためのメソッド。
        /// </summary>
        void alertEventMediaOpened()
        {
            mediaElement.Pause();
        }

        /// <summary>
        /// 指定したファイルパスの音声を再生する。
        /// </summary>
        public void PlayVoice(double volume)
        {
            mediaElement.Volume = volume;
            mediaElement.Play();
            Console.WriteLine("[再生]" + mediaElement.Source + "\n[音量]" + volume);
        }

        /// <summary>
        ///  指定位置にシークする。
        /// </summary>
        /// <param name="time">シークしたい秒数</param>
        public void SeekVoice(double time)
        {
            mediaElement.Position = TimeSpan.FromSeconds(time);
        }

        /// <summary>
        /// 指定したファイルパスの音声を停止する。
        /// </summary>
        public void StopVoice()
        {
            mediaElement.Stop();
            Console.WriteLine("[停止]" + mediaElement.Source);
        }

        /// <summary>
        /// 指定位置にシークをし、そこから指定秒数までを再生する。
        /// </summary>
        /// <param name="startTime">再生したい秒数</param>
        /// <param name="stopTime">そこから何秒再生するか</param>
        public void SeekPlayAndTimerStop(double startTime, double stopTime, double volume)
        {
            Console.WriteLine("[指定]" + stopTime + "s");
            DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(stopTime) };
            SeekVoice(startTime);
            PlayVoice(volume);
            timer.Start();
            timer.Tick += (s, args) =>
            {
                // TimeSpan.FromSeconds(stopTime)秒後に実行
                timer.Stop();
                StopVoice();
            };
        }

        /// <summary>
        /// ファイルの長さを秒数で返す
        /// </summary>
        /// <returns>再生時間</returns>
        public double GetVoiceDurationTime()
        {
            if (mediaElement.NaturalDuration.HasTimeSpan)
            {
                return mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
            }
            MessageBox.Show("ファイルの再生時間が取得できませんでした。\nもう一度ファイルを開いてみてください。");
            return 0;
        }

        private void DataGridClickEdit(object sender, RoutedEventArgs e)
        {
            if (((MenuItem)sender).DataContext is WordInformation) {
                var data = (WordInformation)((MenuItem)sender).DataContext;
                //データコンテキストの型はWordInformationやで
            }else
            {
                Console.WriteLine("ちがうやで");
                Console.WriteLine(DataContext);
            }
        }
    }
}
