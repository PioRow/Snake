using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;
using SFML.System;
using System.Diagnostics;
namespace Snake
{
    public class MainWindow
    {
        private Color _backGroundColor;
        SnakeBody _snake;
        bool _isStarted;
        int[,] _field;
        bool _grew;
        private RenderWindow _mainWindow;
        public MainWindow()
        {
            
            _mainWindow = new RenderWindow(new VideoMode(500,500),"Snake");
            _mainWindow.Closed += (_, __) => _mainWindow.Close();
            _backGroundColor = new Color(19,4,135);
            _field = new int[10, 10];
            _mainWindow.SetFramerateLimit(30);
            _isStarted = false;
            _mainWindow.KeyPressed += OnKeyPress;
            _snake=new SnakeBody();
            
            Clock clock = new Clock();
            Text gameStartText = new Text
            {
                Font = new Font("arial.ttf"),
                DisplayedString = "Snake\npress space to start",
                Position = new Vector2f(200, 200),
                FillColor = Color.Magenta
            };
            while (_mainWindow.IsOpen)
            {
                
                _mainWindow.DispatchEvents();
                _mainWindow.Clear(_backGroundColor);

                if (_isStarted)
                {
                   
                    clock.Restart();
                   _isStarted= _snake.updateSnakePosition(out _grew,_field);                    
                    _snake.Draw(_mainWindow);
                    if (_grew)
                        GenPoint();
                    drawPoints();
                    while (clock.ElapsedTime.AsMilliseconds() <= 500)
                    {
                        _mainWindow.DispatchEvents();
                        //_mainWindow.Display();
                    }
                    
                }
                else
                {
                    _mainWindow.Draw(gameStartText);
                }
                _mainWindow.Display();
            }
        }
        private void GenPoint()
        {
            Random PointGenerator = new Random();
            bool toPlace = true;
            while (toPlace)
            {
                int x, y;
                x = PointGenerator.Next(10);
                y = PointGenerator.Next(10);
                if (_field[x, y] == 0)
                {
                    _field[x, y] = 2;
                    Console.WriteLine(x + " " + y);
                    toPlace = false;
                }
            }
        }
        private void drawPoints()
        {
            for (int x = 0; x < 10; x++)
            {
                for(int y=0;y<10; y++)
                {
                    if (_field[x,y]==2)
                    {
                        RectangleShape rec = new RectangleShape(new Vector2f(50, 50))
                        {
                            FillColor = Color.Red,
                            Position = new Vector2f(50 * x, 50 * y)
                        };
                        _mainWindow.Draw(rec);
                    }
                }

                
            }
        }
        private void OnKeyPress(object sender,KeyEventArgs e)   
        {
            if(e.Code==Keyboard.Key.Space)
            {
                _snake=new SnakeBody();
                _field = new int[10, 10];
                _field[5, 5] = 1;
                GenPoint();
                _isStarted = true;
            }
            if (e.Code == Keyboard.Key.Escape)
                _mainWindow.Close();
            if(e.Code==Keyboard.Key.Right|| e.Code == Keyboard.Key.Left
                || e.Code == Keyboard.Key.Up|| e.Code == Keyboard.Key.Down)
            {
                _snake.updateVelocity(e.Code);
            }
        }
        
    }
}
