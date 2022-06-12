﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Reflection;
using System.Media;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;


namespace Tetris
{
    public partial class MainWindow : Window
    {
        private readonly ImageSource[] tiles = new ImageSource[]
        {
            new BitmapImage(new Uri("assets/Ghost/Single.png", UriKind.Relative)),
            new BitmapImage(new Uri("assets/Tiles/LightBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("assets/Tiles/Blue.png", UriKind.Relative)),
            new BitmapImage(new Uri("assets/Tiles/Orange.png", UriKind.Relative)),
            new BitmapImage(new Uri("assets/Tiles/Yellow.png", UriKind.Relative)),
            new BitmapImage(new Uri("assets/Tiles/Green.png", UriKind.Relative)),
            new BitmapImage(new Uri("assets/Tiles/Purple.png", UriKind.Relative)),
            new BitmapImage(new Uri("assets/Tiles/Red.png", UriKind.Relative))

        };
        private readonly ImageSource[] blocks = new ImageSource[]
        {
            new BitmapImage(new Uri("assets/Ghost/Single.png", UriKind.Relative)),
            new BitmapImage(new Uri("assets/Blocks/I.png", UriKind.Relative)),
            new BitmapImage(new Uri("assets/Blocks/J.png", UriKind.Relative)),
            new BitmapImage(new Uri("assets/Blocks/L.png", UriKind.Relative)),
            new BitmapImage(new Uri("assets/Blocks/O.png", UriKind.Relative)),
            new BitmapImage(new Uri("assets/Blocks/S.png", UriKind.Relative)),
            new BitmapImage(new Uri("assets/Blocks/T.png", UriKind.Relative)),
            new BitmapImage(new Uri("assets/Blocks/Z.png", UriKind.Relative))
        };

        private readonly Image[,] imgCtrl;
        private readonly int maxDelay = 1000;
        private readonly int minDelay = 75;
        private readonly int delayDecrease = 2;
        private GameState state = new GameState();
        private MediaPlayer music = new MediaPlayer();
        private MediaPlayer clearedLine = new MediaPlayer();
        private int curGameScore = 0;
        private Stopwatch timeElapsed = new Stopwatch();

        public MainWindow()
        {
            InitializeComponent();
            StartBackgroundMusic();
            imgCtrl = SetUpGameCanvas(state.GameGrid);
        }

        private Image[,] SetUpGameCanvas(GameGrid grid)
        {
            Image[,] imgCtrl = new Image[grid.Rows, grid.Cols];
            int cellSize = 25;
            for(int row=0; row<grid.Rows; row++)
            {
                for(int col=0; col<grid.Cols; col++)
                {
                    Image control = new Image { Width = cellSize, Height = cellSize };
                    Canvas.SetTop(control, (row - 2) * cellSize + 10);
                    Canvas.SetLeft(control, col * cellSize);
                    GameCanvas.Children.Add(control);
                    imgCtrl[row, col] = control;
                }
            }
            return imgCtrl;
        }

        private void DrawGrid(GameGrid grid)
        {
            for(int row=0; row < grid.Rows; row++)
            {
                for(int col=0; col<grid.Cols; col++)
                {
                    int id = grid[row, col];
                    imgCtrl[row, col].Opacity = 1;
                    imgCtrl[row, col].Source = tiles[id];
                }
            }
        }

        private void NextBlock(BlockQueue queue)
        {
            Block next = queue.NextBlock;
            NextImage.Source = blocks[next.Id];
        }

        private void drawHeldBlock(Block held)
        {
            if(held == null)
            {
                HoldImage.Source = null;
            }
            else
            {
                HoldImage.Source = blocks[held.Id];
            }
        }

        private void drawGhost(Block block)
        {
            int dropDist = state.blockDropDistance();
            foreach(Position pos in block.tilePositions())
            {
                imgCtrl[pos.Row + dropDist, pos.Col].Opacity = 0.25;
                imgCtrl[pos.Row + dropDist, pos.Col].Source = tiles[block.Id] ;
            }
        }

        private void Draw(GameState state)
        {
            DrawGrid(state.GameGrid);
            drawGhost(state.Current);
            DrawBlock(state.Current);
            NextBlock(state.BlockQueue);
            drawHeldBlock(state.held);
            ScoreText.Text = $"Score: {state.score * 10}";
            var time = timeElapsed.Elapsed;
            Timer.Text = $"Timer: \n{time.Minutes:00}:{time.Seconds:00}";
        }

        private void DrawBlock(Block block)
        {
            foreach(Position coor in block.tilePositions())
            {
                imgCtrl[coor.Row, coor.Col].Opacity = 1;
                imgCtrl[coor.Row, coor.Col].Source = tiles[block.Id];
            }
        }
        private async Task GameLoop()
        {
            Draw(state);
            Uri pathToFile = new Uri(AppDomain.CurrentDomain.BaseDirectory + "assets\\Music\\lineClear.wav",UriKind.Relative);
            clearedLine.Open(pathToFile);
            while (!state.gameOver)
            {
                timeElapsed.Start();
                int delay = Math.Max(minDelay, maxDelay - (state.score * delayDecrease));
                await Task.Delay(delay);
                state.moveBlockDown();
                Draw(state);
                if(curGameScore != state.score)
                {
                    clearedLine.Play();
                    curGameScore = state.score;
                    clearedLine.Position = TimeSpan.Zero;
                }
            }
            timeElapsed.Stop();
            GameOverMenu.Visibility = Visibility.Visible;
            FinalScoreText.Text = $"Score: {state.score * 10}";
            FinalTimer.Text = $"Time: {timeElapsed.Elapsed.Minutes}:{timeElapsed.Elapsed.Seconds}";
        }

        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
            if (state.gameOver)
            {
                return;
            }

            switch (e.Key)
            {
                case Key.Left:
                    state.moveLeft();
                    break;
                case Key.Right:
                    state.moveRight();
                    break;
                case Key.Down:
                    state.moveBlockDown();
                    break;
                case Key.Up:
                    state.rotateBlockRight();
                    break;
                case Key.Z:
                    state.rotateBlockLeft();
                    break;
                case Key.C:
                    state.hold();
                    break;
                case Key.Space:
                    state.drop();
                    break;
                default:
                    return;
            }

            Draw(state);
        }

        private async void GameCanvasLoaded(object sender, RoutedEventArgs e)
        {
            await GameLoop();
        }

        private async void PlayAgainClick(object sender, RoutedEventArgs e)
        {
            state = new GameState();
            GameOverMenu.Visibility = Visibility.Hidden;
            timeElapsed.Reset();
            await GameLoop();
        }

        public void StartBackgroundMusic()
        {
            Uri pathToFile = new Uri(AppDomain.CurrentDomain.BaseDirectory + "assets\\Music\\mainTheme.wav",UriKind.Relative);
            music.Open(pathToFile);
            music.Volume = 0.20; //0.0 - 1.0
            music.MediaEnded += new EventHandler(BackgroundMusicEnded);
            music.Play();
        }
        private void BackgroundMusicEnded(object sender, EventArgs e)
        {
            music.Position = TimeSpan.Zero;
            music.Play();
        }

    }
}

