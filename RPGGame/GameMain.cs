using System;
using System.Windows.Forms;
using TeamAppleThief.CoreLogic;
using TeamAppleThief.Exceptions;

namespace TeamAppleThief
{
    class GameMain
    {
        static void Main(string[] args)
        {
            var context = new RPGGameDBContext();
            try
            {
                using ( var engine = new Engine(context))
                {
                    engine.Run();
                }
            }
            catch (ResourcesNotFoundException re)
            {
                MessageBox.Show(re.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                MessageBox.Show(e.StackTrace);
            }
        }
    }
}
