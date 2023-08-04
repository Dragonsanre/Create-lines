using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Collections;

namespace Assignment_4
{
    class Stick
    {
        private double x1;
        private double x2;
        private double y1;
        private double y2;
        private double linecount;
        public double winWidth = 0;
        public double winHeight = 0;
        public int x1Dir = 0;
        public int x2Dir = 0;
        public int y1Dir = 0;
        public int y2Dir = 0;
        private bool state = true;
        
        ArrayList Sticklist = new ArrayList();
        public bool State
        {
            get { return this.state; }
            set { this.state = value; }
        }
        public Stick Assign
        {
            get { return this; }
            set
            {
                this.x1 = value.X1;
                this.x2 = value.X2;
                this.y1 = value.Y1;
                this.y2 = value.Y2;
                this.winWidth = value.winWidth;
                this.winHeight = value.winHeight;
                this.x1Dir = value.x1Dir;
                this.x2Dir = value.x2Dir;
                this.y1Dir = value.y1Dir;
                this.y2Dir = value.y2Dir;
                this.linecount = value.Sticklist.Count;
                this.Sticklist = value.Sticklist;
                this.State = value.State;
            }
        }
        public double Lines
        {
            get { return this.linecount; }
            set { this.linecount = value; }
        }
        public double X1
        {
            get { return this.x1; }
            set { this.x1 = value; }
        }
        public double X2
        {
            get { return this.x2; }
            set { this.x2 = value; }
        }
        public double Y1
        {
            get { return this.y1; }
            set { this.y1 = value; }
        }
        public double Y2
        {
            get { return this.y2; }
            set { this.y2 = value; }
        }
        public Stick(double windowWidthIn, double windowHeightIn)
        {
            this.winWidth = windowWidthIn;
            this.winHeight = windowHeightIn;
            x1Dir = 1;
            x2Dir = 0;
            y1Dir = 0;
            y2Dir = 1;
        }
        public Stick()
        {

        }
        public Line CreateLine(double x1, double x2, double y1, double y2)
        {
            Line newLine = new Line();
            int num = RandomInt();
            if (num == 1)
            {
                newLine.Stroke = Brushes.Red;
            }
            if (num == 2)
            {
                newLine.Stroke = Brushes.Blue;
            }
            if (num == 3)
            {
                newLine.Stroke = Brushes.Purple;
            }
            this.X1 = x1;
            this.X2 = x2;
            this.Y1 = y1;
            this.Y2 = y2;
            newLine.X1 = x1;
            newLine.X2 = x2;
            newLine.Y1 = y1;
            newLine.Y2 = y2;
            Lines++;
            newLine.StrokeThickness = 4;
            Sticklist.Add(newLine);
            return newLine;
        }
        public int RandomInt()
        {
            Random randNum = new Random();
            return randNum.Next(1, 4);
        }
        public Line MoveLine()
        {
            Line newLine = new Line();
            int num = RandomInt();
            if (X1 >= winWidth || x1 <= 0)
            {
                x1Dir = (x1Dir + 1) % 2;
            }
            if (X2 >= winWidth || x2 <= 0)
            {
                x2Dir = (x2Dir + 1) % 2;
            }
            if (Y1 >= winHeight || y1 <= 0)
            {
                y1Dir = (y1Dir + 1) % 2;
            }
            if (Y2 >= winHeight || y2 <= 0)
            {
                y2Dir = (y2Dir + 1) % 2;
            }
            if (x1Dir == 0)
            {
                X1 += 10;
            }
            else
            {
                X1 -= 10;
            }
            if (x2Dir == 0)
            {
                X2 += 10;
            }
            else
            {
                X2 -= 10;
            }
            if (y1Dir == 0)
            {
                Y1 += 10;
            }
            else
            {
                Y1 -= 10;
            }
            if (y2Dir == 0)
            {
                Y2 += 10;
            }
            else
            {
                Y2 -= 10;
            }
            newLine.X1 = x1;
            newLine.X2 = x2;
            newLine.Y1 = y1;
            newLine.Y2 = y2;
            if (num == 1)
            {
                newLine.Stroke = Brushes.Red;
            }
            if (num == 2)
            {
                newLine.Stroke = Brushes.Blue;
            }
            if (num == 3)
            {
                newLine.Stroke = Brushes.Purple;
            }
            newLine.StrokeThickness = 4;
            Lines++;
            Sticklist.Add(newLine);
            return newLine;
        }
        public Line RemoveLine()
        {
            Line line = (Line)Sticklist[0];
            Sticklist.RemoveAt(0);
            Lines -= 1;
            return line;
        }

    }

}
