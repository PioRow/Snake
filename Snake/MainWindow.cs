using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using SFML.Audio;
using SFML.System;
namespace Snake
{
    public class MainWindow
    {
        private Color _backGroundColor;
        SnakeBody _snake;
        bool _isStarted;
        private RenderWindow _mainWindow;
        public MainWindow()
        {
            
            _mainWindow = new RenderWindow(new VideoMode(500,500),"Snake");
            _mainWindow.Closed += (_, __) => _mainWindow.Close();
            _backGroundColor = new Color(19,4,135);
            
            _mainWindow.SetFramerateLimit(30);
            _isStarted = false;
            _mainWindow.KeyPressed += OnKeyPress;
            _snake=new SnakeBody();
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
                   _isStarted= _snake.updateSnakePosition();                    
                    _snake.Draw(_mainWindow);
                }
                else
                {
                    _mainWindow.Draw(gameStartText);
                }
                
                _mainWindow.Display();
            }
        }
        private void OnKeyPress(object sender,KeyEventArgs e)
        {
            if(e.Code==Keyboard.Key.Space)
            {
                _snake=new SnakeBody();
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
