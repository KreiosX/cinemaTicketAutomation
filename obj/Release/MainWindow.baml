   M S B A M L   `   `   `  ���� U  QAsilCinemaTicketAutomation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:AsilCinemaTicketAutomationAsilCinemaTicketAutomation  +    %AsilCinemaTicketAutomation.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     :local(clr-namespace:AsilCinemaTicketAutomation     -   $��mw��  � Title$  	Main Menu��$��614.4��$��1092.8�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=� � Icon$ Resources/asiltckt.ico������  �� ImageSource$ Resources/asil2.png��.��� .+��� -   $��feedback��
�������$��1q�$��0,540,1005,0q�$��20��
������ $��Feedback��
�������$��70�� � 	Resources  �� 
TargetType �� Property �� Value��%:   '��       �� " ��.���  �@��CornerRadius$ 10���� -   $��abtus��$y�	Collapsed=�$��Resources\about-us.png���� -   $��txtsc��$��1005,540,0,0q�$��About Us��
������ 
�������
�������$��1q�$��20�� ��%:   '��       �� " ��.���  �@��CornerRadius$ 10���� -   $��txtScrolling��
�������$��3q�$y�	Collapsed=�{�.  	 �� TextAlignment$	 Center=�
�������
���   �$��Center=�$��Center=�$
��auto��$
��auto��.��z�T� -   	translate��.��
 � Triggers
 :�  :� RoutedEvent$ FrameworkElement.Loaded��.[��� .����  g� RepeatBehavior$ Forever��.��W�  W� From$
 -450X�	 W� To$	 450X� �� 
TargetName$ 	translate�� �� TargetProperty$ Y� g� Duration$ 0:0:20D�.�� ��%:   '��       �� " ��.���  �@��CornerRadius$ 50��.���� -   $��userinformationz��$y�Hidden=�$��WrapWithOverflow=� �� TextAlignment$ Center=�
������ $��5q�$��70��$��40��$	��Top=�$
��Left=�
��������� -   $��userinformation��$��WrapWithOverflow=�$ Center=�
������ $��5q�$
��auto��$
��auto��$	��Top=�$
��Left=�
��������� -	   $��date1��$ Center=�
�������
������ $��0,5,10,0q�$
��auto��$
��auto��$	��Top=�$��Right=� � ToolTip$ Date Format DD/MM/YYYY��o� 
������ $��Center=�$
��auto��$��25��$	��Top=� o� 
IsMainMenu . $��14(�$	��0.5q�
�������
�������.@�m� -
   $
��frag��
������ $��25��$
��Home��
������� m� Icon �� $��Resources\homepage.png��m� -   $��TTransaction��
������ $��25��$��Ticket Transactions��
������� �� $��Resources\icon_ticket.png��.@�m� -   $��	cutticket��$��
Cut Ticket��
�������
�����p� �� $��Resources\cuttckpng.png��m� -   $��ticketcancellation��
�������
�����p�$+��%_Ticket Cancellation and Reservations�� �� $��Resources\tcar.png��m� -   $��MProfile��
������ $��25��$��
My Profile��
������� �� $��Resources\user.png��m� -   $��ETVision��
������ $��25��$��Enter The Vision��
������� �� $!��Resources\vizyon-misyon.png��m� -   $��FOperations��
������ $��25��$��Film Operations��
������� �� $��Resources\cpb.png��m� -   $��ATransactions��
������ $��25��$ ��Authorization Transactions��
������� �� $��Resources\egm.png��m� -   $��	EditSeatz��
������ $��
Edit Seats��
������� �� $��Resources\edittpng.png���� -   $��mainprocess��$
��auto��$
��auto��$��0,25,0,0q�.+�� -   $��fragp��$��Center=�$��Center=�$	��800��.+�r� -   $��myMediaElement�� r� LoadedBehavior$ Manual=� � Cursor$
 None�� r� Volume$ 0Q� J� Opacity$	 0.9Q�$y�Visible=�$��0,35,0,0q�$	��400��$	��200��r� -   $��myMediaElement1��$ Manual=�$
 None��$ 0Q�$	 0.9Q�$y�Visible=�$��0,35,0,0q�$	��400��$	��200��r� -   $��myMediaElement2��$ Manual=�$
 None��$ 0Q�$	 0.9Q�$y�Visible=�$��0,35,0,0q�$	��400��$	��200��r� -   $��myMediaElement3��$ Manual=�$
 None��$ 0Q�$	 0.9Q�$y�Visible=�$��0,35,0,0q�$	��400��$	��200��