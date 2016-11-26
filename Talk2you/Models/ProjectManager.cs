using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Livet;

namespace Talk2you.Models
{
    public class ProjectManager : NotificationObject
    {
        class projectSettiings
        {   //プロジェクトの情報をセットするもの
            string projectName { set; get; }
            string charaName { set; get; }
            string workName { set; get; }
        }

        class serifInformation
        {   //セリフの情報をセットするもの
            string identifier { set; get; }
            string plainText { set; get; }
            int category { set; get; }
            int volume { set; get; }
            double start { set; get; }
            double end { set; get; }
            string file { set; get; }
        }



    }
}
