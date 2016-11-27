using System;
using System.Collections.Generic;
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

        public MainWindow()
        {
            InitializeComponent();
            InitializeMediaElement();
        }

        void InitializeMediaElement()
        {
            //mediaElementを対話操作可能にする。
            mediaElement.LoadedBehavior = MediaState.Manual;
        }

        public void LoadVoice(string filePath)
        {   //音声ファイルのロードを行う
            mediaElement.Volume = 0.1;  // debug
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

        void alertEventMediaOpened()
        {      //開いたことをイベントハンドラで受け取りたいので一旦ポーズする。
            mediaElement.Pause();
        }

        public void PlayVoice()
        {   // 指定したファイルパスの音声を再生する
            mediaElement.Play();
            Console.WriteLine("[再生]" + mediaElement.Source);
        }

        public double GetVoiceDurationTime()
        {   //ファイルの再生時間を秒数で返す
            if (mediaElement.NaturalDuration.HasTimeSpan)
            {
                return mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
            }
            MessageBox.Show("ファイルの再生時間が取得できませんでした。\nもう一度ファイルを開いてみてください。");
            return 0;
        }

    }
}
