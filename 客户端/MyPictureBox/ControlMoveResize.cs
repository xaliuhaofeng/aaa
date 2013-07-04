namespace MyPictureBox
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    internal class ControlMoveResize
    {
        private Control Containe = null;
        private int controlType;
        private Control ctrl = null;
        private bool IsMoving = false;
        private Point pCtrlLastCoordinate = new Point(0, 0);
        private Point pCursorLastCoordinate = new Point(0, 0);
        private Point pCursorOffset = new Point(0, 0);
        private Point storedPoint;
        private int type;

        public ControlMoveResize(Control c, Control parentContain, int controlType, bool IsEdit, int type)
        {
            this.ctrl = c;
            this.type = type;
            this.Containe = parentContain;
            this.controlType = controlType;
            if (IsEdit)
            {
                this.ctrl.MouseDown += new MouseEventHandler(this.MouseDown);
                this.ctrl.MouseUp += new MouseEventHandler(this.MouseUp);
            }
            this.ctrl.MouseMove += new MouseEventHandler(this.MouseMove);
            this.ctrl.MouseLeave += new EventHandler(this.ctrl_MouseLeave);
            this.ctrl.MouseEnter += new EventHandler(this.ctrl_MouseEnter);
        }

        private void ctrl_MouseEnter(object sender, EventArgs e)
        {
            this.ctrl.Cursor = Cursors.Hand;
        }

        private void ctrl_MouseLeave(object sender, EventArgs e)
        {
            this.ctrl.Cursor = Cursors.Arrow;
            if (this.controlType == 0)
            {
                ((ALittlePicture) this.ctrl).ShowInfo(false, 0, 0);
            }
            else if (this.controlType == 1)
            {
                ((AWordTuYuan) this.ctrl).ShowInfo(false, 0, 0);
            }
            else if (this.controlType == 2)
            {
                ((TransLittlePicture) this.ctrl).ShowInfo(false, 0, 0);
            }
        }

        private void MouseDown(object sender, MouseEventArgs e)
        {
            if ((this.Containe != null) && (e.Button == MouseButtons.Left))
            {
                this.IsMoving = true;
                this.pCtrlLastCoordinate.X = this.ctrl.Left;
                this.pCtrlLastCoordinate.Y = this.ctrl.Top;
                this.pCursorLastCoordinate.X = Cursor.Position.X;
                this.pCursorLastCoordinate.Y = Cursor.Position.Y;
            }
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            if (this.Containe != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (this.IsMoving && (this.ctrl.Cursor == Cursors.Hand))
                    {
                        Point pCursor = new Point(Cursor.Position.X, Cursor.Position.Y);
                        this.pCursorOffset.X = pCursor.X - this.pCursorLastCoordinate.X;
                        this.pCursorOffset.Y = pCursor.Y - this.pCursorLastCoordinate.Y;
                        if (((((this.pCtrlLastCoordinate.X + this.pCursorOffset.X) > this.Containe.Left) && ((this.pCtrlLastCoordinate.Y + this.pCursorOffset.Y) > this.Containe.Top)) && (((this.pCtrlLastCoordinate.X + this.pCursorOffset.X) + this.ctrl.Width) < this.Containe.Right)) && (((this.pCtrlLastCoordinate.Y + this.pCursorOffset.Y) + this.ctrl.Height) < this.Containe.Bottom))
                        {
                            this.ctrl.Left = this.pCtrlLastCoordinate.X + this.pCursorOffset.X;
                            this.ctrl.Top = this.pCtrlLastCoordinate.Y + this.pCursorOffset.Y;
                            this.ctrl.BringToFront();
                            if (this.controlType == 0)
                            {
                                ((ALittlePicture) this.ctrl).calculateOffset();
                            }
                            else if (this.controlType == 1)
                            {
                                ((AWordTuYuan) this.ctrl).calculateOffset();
                            }
                            else if (this.controlType == 2)
                            {
                                ((TransLittlePicture) this.ctrl).calculateOffset();
                            }
                        }
                    }
                }
                else
                {
                    if (this.controlType == 0)
                    {
                        ((ALittlePicture) this.ctrl).ShowInfo(true, e.X, e.Y);
                    }
                    else if (this.controlType == 1)
                    {
                        ((AWordTuYuan) this.ctrl).ShowInfo(true, e.X, e.Y);
                    }
                    else if (this.controlType == 2)
                    {
                    }
                    if ((this.type == 2) || (this.type == 1))
                    {
                        if (((TransLittlePicture) this.ctrl).NearTheTail(e.X, e.Y))
                        {
                            this.ctrl.Cursor = Cursors.Cross;
                            this.storedPoint = e.Location;
                        }
                        else
                        {
                            this.ctrl.Cursor = Cursors.Hand;
                        }
                    }
                }
            }
        }

        private void MouseUp(object sender, MouseEventArgs e)
        {
            if ((this.Containe != null) && this.IsMoving)
            {
                this.IsMoving = false;
                if (this.ctrl.Cursor == Cursors.Hand)
                {
                    if ((this.pCursorOffset.X != 0) || (this.pCursorOffset.Y != 0))
                    {
                        if (((this.pCtrlLastCoordinate.X + this.pCursorOffset.X) + this.ctrl.Width) > 0)
                        {
                            this.ctrl.Left = this.pCtrlLastCoordinate.X + this.pCursorOffset.X;
                        }
                        else
                        {
                            this.ctrl.Left = 0;
                        }
                        if (((this.pCtrlLastCoordinate.Y + this.pCursorOffset.Y) + this.ctrl.Height) > 0)
                        {
                            this.ctrl.Top = this.pCtrlLastCoordinate.Y + this.pCursorOffset.Y;
                        }
                        else
                        {
                            this.ctrl.Top = 0;
                        }
                        this.pCursorOffset.X = 0;
                        this.pCursorOffset.Y = 0;
                    }
                }
                else
                {
                    int tempY;
                    int tempX;
                    if (this.ctrl.Cursor == Cursors.SizeWE)
                    {
                        tempY = this.ctrl.Height;
                        tempX = (this.ctrl.Width + e.X) - this.storedPoint.X;
                        if (tempX > 10)
                        {
                            this.ctrl.Size = new Size(tempX, tempY);
                        }
                        if (this.controlType == 0)
                        {
                            ((ALittlePicture) this.ctrl).calculateOffset();
                        }
                        else if (this.controlType == 1)
                        {
                            ((AWordTuYuan) this.ctrl).calculateOffset();
                        }
                        else if (this.controlType == 2)
                        {
                            ((TransLittlePicture) this.ctrl).calculateOffset();
                        }
                    }
                    else if (this.ctrl.Cursor == Cursors.SizeNS)
                    {
                        tempY = (this.ctrl.Height + e.Y) - this.storedPoint.Y;
                        tempX = this.ctrl.Width;
                        if (tempY > 10)
                        {
                            this.ctrl.Size = new Size(tempX, tempY);
                        }
                        if (this.controlType == 0)
                        {
                            ((ALittlePicture) this.ctrl).calculateOffset();
                        }
                        else if (this.controlType == 1)
                        {
                            ((AWordTuYuan) this.ctrl).calculateOffset();
                        }
                        else if (this.controlType == 2)
                        {
                            ((TransLittlePicture) this.ctrl).calculateOffset();
                        }
                    }
                    else if (this.ctrl.Cursor == Cursors.Cross)
                    {
                        ((TransLittlePicture) this.ctrl).ReCalculateAngleAndSize(e.X, e.Y);
                    }
                }
            }
        }
    }
}

