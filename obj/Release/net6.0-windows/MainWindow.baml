   M S B A M L   `   `   `  ���� A  =Tetris, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:TetrisTetris      Tetris.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     &localclr-namespace:Tetris     -     � Title$  Tetris��$	��600��$	��800��$	��600��$	��600��
�������$��
sans-serif)�$��28(�.���  � RowDefinitions �� $
��auto ��� $��* � � ColumnDefinitions �� $��* ��� $
��auto ��� $��* �����  �� ImageSource$ assets\background.png��.+�(� $��1��$��1��$��0,0,0,20q�.���� -    ��Name$ 
GameCanvas��$��1��$��1�����#101010$	��250��$	��510��$
}�True���� -    ��Name$ 	ScoreText��$��0��$��1��$��Score: ��$
��0,10q� �� TextAlignment$ Center=��� -   $ Timer��$��Time: 
��$ Center=�$��10,0,10,437q�$��1���� $��1��$��0��$��Center=�$��Right=����#101010$
��10,0q�.+��� $
��Hold��$ Center=��� -    ��Name$ 	HoldImage��$��20q�$	��125��$	��128���� $��1��$��2��$��Center=�$
��Left=����#101010$
��10,0q�.+��� $
��Next��$ Center=��� -   $ 	NextImage��$��20q�$	��125��$	��128��� -    �Name$ GameOverMenu�����	#CC000000$��2��$��3��$y�Hidden=�.+��� $��Center=�$��Center=�.+��� $��	Game Over��$��48(�$ Center=��� -   $ FinalScoreText��$��Score: ��$��30(�$ Center=��� -	   $ 
FinalTimer��$��Time: ��$��24(�$ Center=��� -
   $��
Play Again��
������$��0,20,0,0q�$��5q�