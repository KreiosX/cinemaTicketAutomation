   M S B A M L   `   `   `  ����U  QAsilCinemaTicketAutomation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:AsilCinemaTicketAutomationAsilCinemaTicketAutomation  5   	   +    %AsilCinemaTicketAutomation.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:AsilCinemaTicketAutomation     5   	   -   5        $��mw��5   _     � Title$  	Main Menu��6	   $��614.4��6   $��1092.8��6+    � WindowStartupLocation$ CenterScreen=�6:    � 
ResizeMode$ NoResize=�6i    � Icon$ Resources/asiltckt.ico��6   ��5
      �� 5   
    �� ImageSource$ Resources/asil2.png��6   69   .��5      � .+�5   
   �� -   5        $��feedback��5   P   
�������$��1q�6%   $��0,540,1005,0q�6`   $��20��6v   
������ $��Feedback��6�   
�������$��70��6�    � 	Resources 5       �� 
TargetType �� Property �� Value��%_   '��       �� 5      " ��6   .�5      ��  �@��CornerRadius$ 10��65   5      5      5      �� 5   
   -   5        $��abtus��5      $y�	Collapsed=�6   $��Resources\about-us.png��64   6V   �� 5   
   -   5        $��txtsc��5      $��1005,540,0,0q�6h   $��About Us��6~   
������ 
�������
�������$��1q�6�   $��20��6�    5      ��%_   '��       �� 5      " ��6   .�5      ��  �@��CornerRadius$ 10��65   5      5      5      �� 5   
   -   5        $��txtScrolling��5   �   
�������$��3q�6&   $y�	Collapsed=�6:   {�.  	 �� TextAlignment$	 Center=�6c   
�������
���   �$��Center=�6�   $��Center=�6�   $
��auto��6�   $
��auto��6�   .��6  z�5      T� 5      -   5        	translate��5   %   .��5    '   
 � Triggers
 5!      :� 5"       :� RoutedEvent$ FrameworkElement.Loaded��6   .[�5#      �� .��5$      ��  g� RepeatBehavior$ Forever��6%   .��5%      W�  W� From$
 -450X�5&      	 W� To$	 450X�6%    �� 
TargetName$ 	translate��5'       �� TargetProperty$ Y�5(       g� Duration$ 0:0:20D�5)      5*      5+      5,      .��5-        5.      ��%_   '��       �� 5/      " ��6   .�50      ��  �@��CornerRadius$ 50��65   51      52      .��6!   53      �� 54   
   -   5        $��userinformationz��54   _   $y�Hidden=�6   $��WrapWithOverflow=�6(    �� TextAlignment$ Center=�6H   
������ $��5q�6�   $��70��6�   $��40��6�   $	��Top=�6�   $
��Left=�6�   
�������6�   �� 55   
   -   5        $��userinformation��55   K   $��WrapWithOverflow=�6   $ Center=�64   
������ $��5q�6{   $
��auto��6�   $
��auto��6�   $	��Top=�6�   $
��Left=�6�   
�������6�   �� 56   
   -	   5        $��date1��56   >   $ Center=�6   
�������
������ $��0,5,10,0q�6d   $
��auto��6v   $
��auto��6�   $	��Top=�6�   $��Right=�6�    � ToolTip$ Date Format DD/MM/YYYY��6�   o� 57   
   
������ $��Center=�6(   $
��auto��6E   $��25��6R   $	��Top=�6^    o� 
IsMainMenu . $��14(�6�   $	��0.5q�6�   
�������
�������.@�58      m� -
   5        $
��frag��58   *   
������ $��25��6O   $
��Home��6[   
������� m� Icon 59      �� 5:      $��Resources\homepage.png��6   6>   5<      m� 5=      -   5        $��TTransaction��5=      
������ $��25��6D   $��Ticket Transactions��6P   
������� 5>      �� 5?      $��Resources\icon_ticket.png��6   6A   .@�5A      m� -   5        $��	cutticket��5A   3   $��
Cut Ticket��6D   
�������
�����p� 5B      �� 5C      $��Resources\cuttckpng.png��6    6C   5E      m� 5F      -   5        $��ticketcancellation��5F   <   
�������
�����p�$+��%_Ticket Cancellation and Reservations��6�    5G      �� 5H      $��Resources\tcar.png��6    6>   5J      5K      m� 5L      -   5        $��MProfile��5L      
������ $��25��6X   $��
My Profile��6d   
������� 5M      �� 5N      $��Resources\user.png��6   6:   5P      m� 5Q      -   5        $��ETVision��5Q   0   
������ $��25��6]   $��Enter The Vision��6i   
������� 5R      �� 5S      $!��Resources\vizyon-misyon.png��6   6C   5U      m� 5V      -   5        $��FOperations��5V   0   
������ $��25��6`   $��Film Operations��6l   
������� 5W      �� 5X      $��Resources\cpb.png��6   69   5Z      m� 5[      -   5        $��ATransactions��5[   0   
������ $��25��6a   $ ��Authorization Transactions��6m   
������� 5\      �� 5]      $��Resources\egm.png��6   69   5_      m� 5`      -   5        $��	EditSeatz��5`   0   
������ $��
Edit Seats��6A   
������� 5a      �� 5b      $��Resources\edittpng.png��6   6>   5d      5e      �� 5f   
   -   5        $��mainprocess��5f   0   $
��auto��6   $
��auto��6"   $��0,25,0,0q�6C   .+�5g      � -   5        $��fragp��5g      $��Center=�6%   $��Center=�6@   $	��800��6]   .+�5h      r� -   5        $��myMediaElement��5h   �    r� LoadedBehavior$ Manual=�6    � Cursor$
 None��67    r� Volume$ 0Q�6l    J� Opacity$	 0.9Q�6�   $y�Visible=�6�   $��0,35,0,0q�6�   $	��400��6  $	��200��6  5i      r� 5j      -   5        $��myMediaElement1��5j   �   $ Manual=�6   $
 None��67   $ 0Q�6m   $	 0.9Q�6�   $y�Visible=�6�   $��0,35,0,0q�6�   $	��400��6  $	��200��6   5k      r� 5l      -   5        $��myMediaElement2��5l   �   $ Manual=�6   $
 None��67   $ 0Q�6m   $	 0.9Q�6�   $y�Visible=�6�   $��0,35,0,0q�6�   $	��400��6  $	��200��6   5m      r� 5n      -   5        $��myMediaElement3��5n   �   $ Manual=�6   $
 None��67   $ 0Q�6m   $	 0.9Q�6�   $y�Visible=�6�   $��0,35,0,0q�6�   $	��400��6  $	��200��6   5o      5p      5q      5r      5s      