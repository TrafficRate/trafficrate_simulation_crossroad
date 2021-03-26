using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

namespace UrbanRate
{

    class CrossRoadTimer
    {

        Form1 mainForm;
        System.Timers.Timer T;

        int cntTop, cntBot, cntLeft, cntRight;
        bool stTop, stBot, stLeft, stRight;

        int redDuration = 20, greenDuration = 20;

        public CrossRoadTimer(Form1 f)
        {
            mainForm = f;
            cntTop = cntBot = 20; stTop = stBot = false;
            cntLeft = cntRight = 20; stLeft = stRight = true;
        }

        public void Start()
        {
            T = new System.Timers.Timer(1000);
            T.Elapsed += OnTimerTick;
            T.Enabled = true;
        }
        
        public void Stop()
        {
            T.Stop();
            T.Enabled = false;
        }

        void OnTimerTick(Object sender, ElapsedEventArgs e)
        {
            cntTop--;
            cntBot--;
            cntLeft--;
            cntRight--;
            if(cntTop == 0)
            {
                stTop = !stTop;
                if (stTop) cntTop = greenDuration;
                else cntTop = redDuration;
            }
            if(cntBot == 0)
            {
                stBot = !stBot;
                if (stBot) cntBot = greenDuration;
                else cntBot = redDuration;
            }
            if(cntLeft == 0)
            {
                stLeft = !stLeft;
                if (stLeft) cntLeft = greenDuration;
                else cntLeft = redDuration;
            }
            if(cntRight == 0)
            {
                stRight = !stRight;
                if (stRight) cntRight = greenDuration;
                else cntRight = redDuration;
            }
            mainForm.BeginInvoke((MethodInvoker) delegate() {
                mainForm.cntTopText = "" + cntTop;
                mainForm.cntBotText = "" + cntBot;
                mainForm.cntLeftText = "" + cntLeft;
                mainForm.cntRightText = "" + cntRight;
                if (cntTop <= 3) mainForm.setYellowTop(stTop);
                else if (stTop) mainForm.setGreenTop();
                else mainForm.setRedTop();
                if (cntBot <= 3) mainForm.setYellowBottom(stBot);
                else if (stBot) mainForm.setGreenBottom();
                else mainForm.setRedBottom();
                if (cntLeft <= 3) mainForm.setYellowLeft(stLeft);
                else if (stLeft) mainForm.setGreenLeft();
                else mainForm.setRedLeft();
                if (cntRight <= 3) mainForm.setYellowRight(stRight);
                else if (stRight) mainForm.setGreenRight();
                else mainForm.setRedRight();
            });
        }
    }

}
