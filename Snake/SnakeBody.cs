using SFML.Graphics;
using SFML.Graphics.Glsl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using System.Runtime.ConstrainedExecution;
using SFML.Window;

namespace Snake
{
    public class SnakeBody
    {
        private Vector2f[] _body;
        private Vector2f _velocity;
        private int _length;
        Color[] _colors;
        public SnakeBody()
        {
            _colors=new Color[2];
            _colors[0] = new Color(151,0,196);
            _colors[1] = new Color(196,0,196);
            _body =new Vector2f[100];
            _length = 1;
            _velocity=new Vector2f(1,0);
            _body[0]=new Vector2f(5,5);
        }
        public void updateVelocity(Keyboard.Key k)
        {
            _velocity.X = 0;_velocity.Y = 0;
            if (k == Keyboard.Key.Left)
                _velocity.X = -1;
            if (k == Keyboard.Key.Right)
                _velocity.X = 1;
            if (k == Keyboard.Key.Up)
                _velocity.Y = -1;
            if (k == Keyboard.Key.Down)
                _velocity.Y = 1;
        }
        public bool updateSnakePosition(out bool grew,int[,] field)
        {
            grew = false;
            if (_body[0].X < 9 && _body[0].X >= 0 && _body[0].Y < 9 && _body[0].Y >= 0)
            {

                field[(int)_body[0].X, (int)_body[0].Y] = 0;
                _body[0] += _velocity;
                if (field[(int)_body[0].X, (int)_body[0].Y] == 1)
                    return false;
                else if(field[(int)_body[0].X, (int)_body[0].Y] == 2)
                {
                    grew = true;
                }
                field[(int)_body[0].X, (int)_body[0].Y] = 1;

                return true;
            }
            return false;
        }
        public void Draw(RenderWindow renderWindow)
        {
            for(int it=0;it<_length;it++)
            {
                RectangleShape rec = new RectangleShape(new Vector2f(50, 50))
                {
                    FillColor = _colors[it % 2],
                    Position = new Vector2f(50 * _body[it].X, 50 * _body[it].Y)
                };
                
                renderWindow.Draw(rec);
            }
            
        }
    }
}
