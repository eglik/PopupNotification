   M S B A M L   `   `   `  ����E  AStalkerEye, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PopupNotificationPopupNotification  5   	       StalkerEye.Popup   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:PopupNotification     5   	   -   5          �Name$  popup��5   	    � WindowStyle$
 None=�5	   	    � 
ResizeMode$ NoResize=�5
   	    � AllowsTransparency .  � Title$ PopupNotification��5   	   $��44��6#   $	��800��6/    � ShowInTaskbar .   � Topmost . ��6;   Z� ��6Q   Z� .��5      � .+�5   
   �� -   5         ��Name$ 
background��5      ���#2d2d2d � Triggers 5      :� 5      	 :� RoutedEvent$	 Loaded��6   .[�5      �� .��5      �� .��5      W� 
 W� From$	
 0.0X�6.   	 W� To$	 1.0X�69    g� Duration$ 0:0:0.5D�5   -    �� 
TargetName$ popup��5   -    �� TargetProperty$ Opacity�5   -   5      5      5      5      � 5   
    � ColumnDefinitions 5       �� 5!      $��50 �6#   �� 5"      $��* �6#   .+�5$      �� -   5        $��image��5$      $��25��6!   $��Center=�5%      $��Center=�5&      $e��_C:\Users\FAMOZ-22\source\repos\PopupNotification\PopupNotification\Resources\notification-3.png��5'      �� 5(      -   5         ��Name$
 text��5(      $��Stretch=�6&   $��Center=�6D   $��1��5)      $ ��CustomNotification Message��6,   
�������$
��Wrap=�6a   $��NanumGothic)�6u   $��14(�6�   5*      5+      5,      