using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Timers;
namespace UrbanRate
{
    public partial class Form1 : Form
    {        
        public Boolean topState = false, bottomState = false, leftState = true, rightState = true;
        public int topCount = 0, bottomCount = 0, leftCount = 0, rightCount = 0;
        
        System.Windows.Forms.Timer carTimer;
        private readonly Image transparentImg;
        public PictureBox pictureBox1;
        public Form1()
        {
            InitializeComponent();
            pictureBox1 = new PictureBox();
            this.pictureBox1.Location = new System.Drawing.Point(1, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1068, 696);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.Controls.Add(this.pictureBox1);
            transparentImg = Properties.Resources.background;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.DrawImageUnscaled(transparentImg, new Point(0, 0));      // image1
            //g.DrawImageUnscaled(transparentImg, new Point(140, 20));     // image2
           // g.DrawImageUnscaled(transparentImg, movingPicturePosition);  // image3
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /**Car c = new Car(100, this, new List<Point> { new Point(805, 130), new Point(385, 130), new Point(385, -180) });
            c.Start();
            Car fc = new Car(1000, this, new List<Point> { new Point(-256, 250), new Point(600, 250) });
            fc.Start();
            Ambulance a = new Ambulance(100, this, new List<Point> { new Point(805, 130), new Point(-180, 130) });
            a.Start();*/
            CrossRoadTimer t = new CrossRoadTimer(this);
            t.Start();
            generateCars();
            button1.Enabled = false;
        }
        //left:   Ambulance a = new Ambulance(100, this, new List<Point> { new Point(-256, 250), new Point(600, 250) });
        //right:  Ambulance a = new Ambulance(100, this, new List<Point> { new Point(805, 130), new Point(-180, 130) });
        //bottom: Ambulance a = new Ambulance(100, this, new List<Point> { new Point(385, 560), new Point(385, -180) });
        //top:    Ambulance a = new Ambulance(100, this, new List<Point> { new Point(250, -180), new Point(250, 560) });
        //bottom: Car c = new Car(100, this, new List<Point> { new Point(385, 560), new Point(385, 250), new Point(600, 250) });
        //left:   Car c = new Car(100, this, new List<Point> { new Point(-256, 250), new Point(275, 250), new Point(275, 600) });
        //top:    Car c = new Car(100, this, new List<Point> { new Point(250, -180), new Point(250, 130), new Point(-180, 130) });
        //right:  Car c = new Car(100, this, new List<Point> { new Point(805, 130), new Point(385, 130), new Point(385, -180) });
        
        	
        public void generateCars()
        {
        	Random rnd = new Random();
        	int milisCount = rnd.Next(2000, 5000);
        	carTimer = new System.Windows.Forms.Timer();
		    carTimer.Tick += new EventHandler(launchCarTimerEvent);
		    carTimer.Interval = milisCount;
		    carTimer.Start();
        }
        
		public void launchCarTimerEvent(object source, EventArgs e)
		{			
			Random rnd = new Random();
        	int carSpeed = 250;
        	int wayPossibility = rnd.Next(0, 8);
        	while ( ((wayPossibility == 0 || wayPossibility == 5) && leftCount >= 2)   || 
					((wayPossibility == 1 || wayPossibility == 7) && rightCount >= 2)  ||
					((wayPossibility == 2 || wayPossibility == 4) && bottomCount >= 1) ||
					((wayPossibility == 3 || wayPossibility == 6) && topCount >= 1)    )
        	{
        		if (leftCount >= 2 && rightCount >= 2 && bottomCount >= 1 && topCount >= 1) return;
        		wayPossibility = rnd.Next(0, 8);	
        	}
        	int isAmbulance = rnd.Next(0, 7); // 1 in 7 probability to get an ambulance
        	if (isAmbulance == 5)
        	{
        		Ambulance a;
	        	switch (wayPossibility)
	        	{
	        		case 0: 
        				a = new Ambulance(carSpeed, this, new List<Point> { new Point(-256, 250), new Point(600, 250) });
						a.Start();
						break;
	        		case 1: 
        				a = new Ambulance(carSpeed, this, new List<Point> { new Point(805, 130), new Point(-180, 130) });
						a.Start();
						break;
	        		case 2: 
        				a = new Ambulance(carSpeed, this, new List<Point> { new Point(385, 560), new Point(385, -180) });
						a.Start();
						break;
	        		case 3: 
        				a = new Ambulance(carSpeed, this, new List<Point> { new Point(250, -180), new Point(250, 560) });
						a.Start();
						break;
	        		case 4: 
        				a = new Ambulance(carSpeed, this, new List<Point> { new Point(385, 560), new Point(385, 250), new Point(600, 250) });
						a.Start();
						break;
	        		case 5: 
        				a = new Ambulance(carSpeed, this, new List<Point> { new Point(-256, 250), new Point(275, 250), new Point(275, 600) });
						a.Start();
						break;
	        		case 6: 
        				a = new Ambulance(carSpeed, this, new List<Point> { new Point(250, -180), new Point(250, 130), new Point(-180, 130) });
						a.Start();
						break;
	        		case 7: 
        				a = new Ambulance(carSpeed, this, new List<Point> { new Point(805, 130), new Point(385, 130), new Point(385, -180) });
						a.Start();
						break;
	        		default:
						break;
	        	}
        	}
        	else
        	{
        		if (carSpeed>700) carSpeed = rnd.Next(100, 700);
        		Car c;
        		switch (wayPossibility)
	        	{
	        		case 0: 
        				c = new Car(carSpeed, this, new List<Point> { new Point(-256, 250), new Point(600, 250) });
						c.Start();
						break;
	        		case 1: 
        				c = new Car(carSpeed, this, new List<Point> { new Point(805, 130), new Point(-180, 130) });
						c.Start();
						break;
	        		case 2: 
        				c = new Car(carSpeed, this, new List<Point> { new Point(385, 560), new Point(385, -180) });
						c.Start();
						break;
	        		case 3: 
        				c = new Car(carSpeed, this, new List<Point> { new Point(250, -180), new Point(250, 560) });
						c.Start();
						break;
	        		case 4: 
        				c = new Car(carSpeed, this, new List<Point> { new Point(385, 560), new Point(385, 250), new Point(600, 250) });
						c.Start();
						break;
	        		case 5: 
        				c = new Car(carSpeed, this, new List<Point> { new Point(-256, 250), new Point(275, 250), new Point(275, 600) });
						c.Start();
						break;
	        		case 6: 
        				c = new Car(carSpeed, this, new List<Point> { new Point(250, -180), new Point(250, 130), new Point(-180, 130) });
						c.Start();
						break;
	        		case 7: 
        				c = new Car(carSpeed, this, new List<Point> { new Point(805, 130), new Point(385, 130), new Point(385, -180) });
						c.Start();
						break;
	        		default:
						break;
	        	}
        	}
        	
			
			
			generateCars();
			carTimer.Stop();
		}

        public void setRedTop()
        {
            timerTop.ForeColor = Color.Red;
            greenTop.BackColor =  Color.DarkGreen;
            yellowTop.BackColor = Color.DarkGoldenrod;
            redTop.BackColor = Color.Red;
            topState = false;
        }

        public void setYellowTop(bool isGreen)
        {
            if (isGreen) barrierTop.BackColor = Color.FromArgb(255, 128, 128);
            greenTop.BackColor =  Color.DarkGreen;
            yellowTop.BackColor = Color.Yellow;
            redTop.BackColor = Color.DarkRed;
            topState = false;
        }

        public void setGreenTop()
        {
            barrierTop.BackColor = Color.FromArgb(192, 255, 192);
            timerTop.ForeColor = Color.Green;
            greenTop.BackColor =  Color.Chartreuse;
            yellowTop.BackColor = Color.DarkGoldenrod;
            redTop.BackColor = Color.DarkRed;
            topState = true;
        }

        public void setRedBottom()
        {
            timerBottom.ForeColor = Color.Red;
            greenBottom.BackColor =  Color.DarkGreen;
            yellowBottom.BackColor = Color.DarkGoldenrod;
            redBottom.BackColor = Color.Red;
            bottomState = false;
        }

        public void setYellowBottom(bool isGreen)
        {
            if (isGreen)
            {
                pbarrierLB.BackColor = Color.FromArgb(255, 128, 128);
                pbarrierLT.BackColor = Color.FromArgb(255, 128, 128);
                pbarrierRB.BackColor = Color.FromArgb(255, 128, 128);
                pbarrierRT.BackColor = Color.FromArgb(255, 128, 128);

                pbarrierTL.BackColor = Color.FromArgb(192, 255, 192);
                pberrierTR.BackColor = Color.FromArgb(192, 255, 192);
                pbarrierBL.BackColor = Color.FromArgb(192, 255, 192);
                pbarrierBR.BackColor = Color.FromArgb(192, 255, 192);

                barrierBottom.BackColor = Color.FromArgb(255, 128, 128);
            }
            greenBottom.BackColor =  Color.DarkGreen;
            yellowBottom.BackColor = Color.Yellow;
            redBottom.BackColor = Color.DarkRed;
            bottomState = false;
        }

        public void setGreenBottom()
        {   pbarrierLB.BackColor= Color.FromArgb(192, 255, 192);
            pbarrierLT.BackColor= Color.FromArgb(192, 255, 192);
            pbarrierRB.BackColor = Color.FromArgb(192, 255, 192);
            pbarrierRT.BackColor = Color.FromArgb(192, 255, 192);
            
            pbarrierTL.BackColor = Color.FromArgb(255, 128, 128);
            pberrierTR.BackColor = Color.FromArgb(255, 128, 128);
            pbarrierBL.BackColor = Color.FromArgb(255, 128, 128);
            pbarrierBR.BackColor = Color.FromArgb(255, 128, 128);

            barrierBottom.BackColor = Color.FromArgb(192, 255, 192);
            timerBottom.ForeColor = Color.Green;
            greenBottom.BackColor =  Color.Chartreuse;
            yellowBottom.BackColor = Color.DarkGoldenrod;
            redBottom.BackColor = Color.DarkRed;
            bottomState = true;
        }



        public void setRedRight()
        {
            timerRight.ForeColor = Color.Red;
            greenRight.BackColor = Color.DarkGreen;
            yellowRight.BackColor = Color.DarkGoldenrod;
            redRight.BackColor = Color.Red;
            rightState = false;
        }

        public void setYellowRight(bool isGreen)
        {
            if (isGreen)
            {
                pbarrierLB.BackColor = Color.FromArgb(192, 255, 192);
                pbarrierLT.BackColor = Color.FromArgb(192, 255, 192);
                pbarrierRB.BackColor = Color.FromArgb(192, 255, 192);
                pbarrierRT.BackColor = Color.FromArgb(192, 255, 192);

                pbarrierTL.BackColor = Color.FromArgb(255, 128, 128);
                pberrierTR.BackColor = Color.FromArgb(255, 128, 128);
                pbarrierBL.BackColor = Color.FromArgb(255, 128, 128);
                pbarrierBR.BackColor = Color.FromArgb(255, 128, 128);

                barrierRight.BackColor = Color.FromArgb(255, 128, 128);
            }
            greenRight.BackColor = Color.DarkGreen;
            yellowRight.BackColor = Color.Yellow;
            redRight.BackColor = Color.DarkRed;
            rightState = false;
        }

        public void setGreenRight()
        {
            pbarrierLB.BackColor = Color.FromArgb(255, 128, 128);
            pbarrierLT.BackColor = Color.FromArgb(255, 128, 128);
            pbarrierRB.BackColor = Color.FromArgb(255, 128, 128);
            pbarrierRT.BackColor = Color.FromArgb(255, 128, 128);

            pbarrierTL.BackColor = Color.FromArgb(192, 255, 192);
            pberrierTR.BackColor = Color.FromArgb(192, 255, 192);
            pbarrierBL.BackColor = Color.FromArgb(192, 255, 192);
            pbarrierBR.BackColor = Color.FromArgb(192, 255, 192);


            barrierRight.BackColor = Color.FromArgb(192, 255, 192);
            timerRight.ForeColor = Color.Green;
            greenRight.BackColor = Color.Chartreuse;
            yellowRight.BackColor = Color.DarkGoldenrod;
            redRight.BackColor = Color.DarkRed;
            rightState = true;
        }





        public void setRedLeft()
        {
            timerLeft.ForeColor = Color.Red;
            greenLeft.BackColor = Color.DarkGreen;
            yellowLeft.BackColor = Color.DarkGoldenrod;
            redLeft.BackColor = Color.Red;
            leftState = false;
        }

        public void setYellowLeft(bool isGreen)
        {
            if (isGreen) barrierLeft.BackColor = Color.FromArgb(255, 128, 128);
            greenLeft.BackColor = Color.DarkGreen;
            yellowLeft.BackColor = Color.Yellow;
            redLeft.BackColor = Color.DarkRed;
            leftState = false;
        }

        public void setGreenLeft()
        {
            barrierLeft.BackColor = Color.FromArgb(192, 255, 192);
            timerLeft.ForeColor = Color.Green;
            greenLeft.BackColor = Color.Chartreuse;
            yellowLeft.BackColor = Color.DarkGoldenrod;
            redLeft.BackColor = Color.DarkRed;
            leftState = true;
        }

        public string cntTopText
        {
            get { return timerTop.Text; }
            set { timerTop.Text = value; }
        }

        public string cntBotText
        {
            get { return timerBottom.Text; }
            set { timerBottom.Text = value; }
        }

        public string cntLeftText
        {
            get { return timerLeft.Text; }
            set { timerLeft.Text = value; }
        }

        public string cntRightText
        {
            get { return timerRight.Text; }
            set { timerRight.Text = value; }
        }
        
        void Button2Click(object sender, EventArgs e)
        { 
			Random rnd = new Random();
        	int carSpeed = rnd.Next(700, 1000);
        	int wayPossibility = rnd.Next(0, 8);    
			while ( ((wayPossibility == 0 || wayPossibility == 5) && leftCount >= 2)   || 
					((wayPossibility == 1 || wayPossibility == 7) && rightCount >= 2)  ||
					((wayPossibility == 2 || wayPossibility == 4) && bottomCount >= 1) ||
					((wayPossibility == 3 || wayPossibility == 6) && topCount >= 1)    )
        	{
        		if (leftCount >= 2 && rightCount >= 2 && bottomCount >= 2 && topCount >= 1) return;
        		wayPossibility = rnd.Next(0, 8);	
        	}        	
        	Car c;
    		switch (wayPossibility)
        	{
        		case 0: 
    				c = new Car(carSpeed, this, new List<Point> { new Point(-256, 250), new Point(600, 250) });
					c.Start();
					break;
        		case 1: 
    				c = new Car(carSpeed, this, new List<Point> { new Point(805, 130), new Point(-180, 130) });
					c.Start();
					break;
        		case 2: 
    				c = new Car(carSpeed, this, new List<Point> { new Point(385, 560), new Point(385, -180) });
					c.Start();
					break;
        		case 3: 
    				c = new Car(carSpeed, this, new List<Point> { new Point(250, -180), new Point(250, 560) });
					c.Start();
					break;
        		case 4: 
    				c = new Car(carSpeed, this, new List<Point> { new Point(385, 560), new Point(385, 250), new Point(600, 250) });
					c.Start();
					break;
        		case 5: 
    				c = new Car(carSpeed, this, new List<Point> { new Point(-256, 250), new Point(275, 250), new Point(275, 600) });
					c.Start();
					break;
        		case 6: 
    				c = new Car(carSpeed, this, new List<Point> { new Point(250, -180), new Point(250, 130), new Point(-180, 130) });
					c.Start();
					break;
        		case 7: 
    				c = new Car(carSpeed, this, new List<Point> { new Point(805, 130), new Point(385, 130), new Point(385, -180) });
					c.Start();
					break;
        		default:
					break;
        	}
        }
        
        void Button3Click(object sender, EventArgs e)
        {
			Random rnd = new Random();
        	int carSpeed = rnd.Next(100, 1000);
        	int wayPossibility = rnd.Next(0, 8);
        	while ( ((wayPossibility == 0 || wayPossibility == 5) && leftCount >= 2)   || 
					((wayPossibility == 1 || wayPossibility == 7) && rightCount >= 2)  ||
					((wayPossibility == 2 || wayPossibility == 4) && bottomCount >= 2) ||
					((wayPossibility == 3 || wayPossibility == 6) && topCount >= 2)    )
        	{
        		if (leftCount >= 2 && rightCount >= 2 && bottomCount >= 2 && topCount >= 2) return;
        		wayPossibility = rnd.Next(0, 8);	
        	}
        	Ambulance a;
        	switch (wayPossibility)
        	{
        		case 0: 
    				a = new Ambulance(carSpeed, this, new List<Point> { new Point(-256, 250), new Point(600, 250) });
					a.Start();
					break;
        		case 1: 
    				a = new Ambulance(carSpeed, this, new List<Point> { new Point(805, 130), new Point(-180, 130) });
					a.Start();
					break;
        		case 2: 
    				a = new Ambulance(carSpeed, this, new List<Point> { new Point(385, 560), new Point(385, -180) });
					a.Start();
					break;
        		case 3: 
    				a = new Ambulance(carSpeed, this, new List<Point> { new Point(250, -180), new Point(250, 560) });
					a.Start();
					break;
        		case 4: 
    				a = new Ambulance(carSpeed, this, new List<Point> { new Point(385, 560), new Point(385, 250), new Point(600, 250) });
					a.Start();
					break;
        		case 5: 
    				a = new Ambulance(carSpeed, this, new List<Point> { new Point(-256, 250), new Point(275, 250), new Point(275, 600) });
					a.Start();
					break;
        		case 6: 
    				a = new Ambulance(carSpeed, this, new List<Point> { new Point(250, -180), new Point(250, 130), new Point(-180, 130) });
					a.Start();
					break;
        		case 7: 
    				a = new Ambulance(carSpeed, this, new List<Point> { new Point(805, 130), new Point(385, 130), new Point(385, -180) });
					a.Start();
					break;
        		default:
					break;
        	}
        }
    }
}
