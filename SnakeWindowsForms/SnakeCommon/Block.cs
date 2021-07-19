using System.Windows.Forms;

namespace SnakeWindowsForms
{
    public abstract class Block : PictureBox
    {
        public int Row
        {
            get
            {
                return Top / Height;
            }
            protected set
            {
                Top = value * Height;
            }
        }
        public int Column
        {   get
            {
                return Left / Width;
            }
            protected set
            {
                Left = value * Width;
            }
        }
        protected Form form;
        public Block(int row, int column, int size, Form form)
        {
            Width = size;
            Height = size;
            Row = row;
            Column = column;
            this.form = form;
            form.Controls.Add(this);
            
        }

        public void MoveToCell(Block block)
        {
            Row = block.Row;
            Column = block.Column;
        }

        public bool Intersect(Block block)
        {
            return Row == block.Row && Column == block.Column;
        }
    }
}
