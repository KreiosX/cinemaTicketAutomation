   M S B A M L   `   `   `  ���� U  QAsilCinemaTicketAutomation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:AsilCinemaTicketAutomationAsilCinemaTicketAutomation  1    +AsilCinemaTicketAutomation.FilmTransactions   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     :local(clr-namespace:AsilCinemaTicketAutomation     .��� .+��� -   $��insfilm��$��Insert Film��
�������$	��Top=�$��Center=�$	��250��$
��auto��$��	0,0,125,0q�.���� .+��� $��
Movie Name��
�������$��Center=��� -   $��mname��
������ 
�������$	��100���� $��Movie Description��
�������$��Center=��� -   $��minfo��
������ 
�������$	��100���� $��Movie Time(Minute)��
�������$��Center=��� -   $��mtime��
������ 
�������$	��100���� $��Ticket Price��
�������$��Center=�� .+��� $��50,0,0,0q�$
��Full��
��������� $��50,0,0,0q�$��Student��
�������� .+��� -   $	��mpf��
������ 
�������$��35,0,0,0q�$��60���� -   $	��mps��$��35,0,0,0q�$��60��
������ 
�������� .+��� -   $��mis3d��$!��Movie Is Three-Dimensional?��
�������$��35,0,0,0q��� -   $��ifphoto��$
��Fill=�  � ToolTip$&   Only JPG and JPEG Format Photos.��$	��150��$��90��$��0,5,0,5q��� -	   $��sbanner��$  Only JPG Format Photos..!��$	��175��$��20��$��Select Banner��
�������
������ �� $��
Movie Type��$��Center=�
��������� -
   $	��smt��$	��100������ �� $��Vision Entry / Exit Date��
�������$��Center=�� .+�(  "System.Windows.Controls.DatePicker  -   $��ev��$	��100��$��15,0,0,0q�  -   $��ov��$	��100��$��15,0,0,0q��� -   $
��AddF��$��20��$	��100��
�������
������ $��Add Film���� -   $��	inssaloon��$��Insert Saloon��
�������$	��150��$
��Left=�$	��225��$	��Top=�$��0,25,0,0q�.���� .+��� $��10q�$��Enter Saloon Name��
�������$	��Top=�$
��Left=��� -   $��isaloon��
������ 
�������$��10q�$	��Top=�$
��Left=�$��90���� $��10q�
�������$��Seat In The Hill��$	��Top=�$
��Left=��� -   $��iseat��
������ 
�������$��10q�$��90��$	��Top=�$
��Left=��� -   $��CSaloon��
������ 
�������$��10q�$��Create Saloon��$��90��$	��Top=�$
��Left=��� -   $��
inssession��$��Insert Session��
�������$	��150��$
��Left=�$	��300��$	��Top=�$��	175,0,0,0q�.���� .+��� $��Lounge To  Add��$��Center=�
��������� -   $��ss��$��10,0,0,0q�$
��Left=�$	��100������ �� $��Select Session Date��$��Center=�
�������  -   $��
getsesdate��$
��Left=�$��10,0,0,0q�$	��100���� $��5q�$��Enter Session Time��
�������$	��Top=�$
��Left=��� -   $��ESTime��
������ 
������� �� 	MaxLength$ 5��$��5q�$��90��$	��Top=�$
��Left=��� $��5q�
�������$��Assigned Film���� -   $��ssfilm��$	��100��$��5q����� �� -   $��CSession��
������ 
�������$��5q�$��Create Session��$��90��$	��Top=�$
��Left=��� -   $��updordelsession��$��Delete Session��
�������$	��160��$
��Left=�$	��250��$	��Top=�$��175,300,0,0q�.���� .+��� $��Select Date��
�������$
��Left=�$��5q�  -   $��sdate��$	��100��$
��Left=�$��5q��� $��5q�$��Select Saloon��
�������$	��Top=�$
��Left=��� -   $
��SSal��$��5q�$��90��$	��Top=�$
��Left=����� �� $��5q�$��Select Session��
�������$	��Top=�$
��Left=��� -   $��
USessionCB��$��5q�$��90��$	��Top=�$
��Left=����� � .+��� -   $��DSession��$��90��$��20��$��25,0,0,0q�
�������
������ $��Delete���� -   $��updordelsaloon��$��Delete Saloon��
�������$	��150��$
��Left=�$	��150��$	��Top=�$��	0,250,0,0q�.���� .+��� $��10q�$��Select Salon��
�������$	��Top=�$
��Left=��� -   $��SSaloon��$��10q�$��90��$	��Top=�$
��Left=����� � .+��� -   $��DSaloon��$��Center=�$��Center=�$��90��$��20��$��25,0,0,0q�
�������
������ $��Delete���� -    $��updetvision��$��Update Enter The Vision��
�������$��	0,0,250,0q�$	��200��$	��400��$	��Top=�$��Right=�.���� $��Center=�$
��auto��$	��375��$	��Top=�.+��� -!   $��	ETVisions��$	��150������ �� -"   $��upphoto��$
��Fill=�$&   Only JPG and JPEG Format Photos.��$	��150��$	��250��$	��Top=�$��	5,10,5,10q��� -#   $  Only JPG Format Photos..!��$
��auto��
������ $��20��$��Center=�$��Upload��
��������� -$   $��upfftps��
������ $��25��$��10q�$"��Upload Photo From FTP Server��
��������� -%   $��homefrag��$��Update Home Fragman's��
�������$��0,0,25,0q�$	��200��$
��auto��$	��Top=�$��Right=�.���� $��Center=�$
��auto��$	��275��$	��Top=�.+��� -&   $	��fra��$	��150������ r� -'   $��myMediaElement�� r� LoadedBehavior$ Manual=� J� Opacity$	 0.9Q�$y�Visible=�$��	0,25,0,25q�$
��auto��$
��auto���� -(   $��fragup��$  Only MP4 Format Videos..!��$
��auto��
������ $��20��$��Center=�$��Upload��
��������� -)   $	��umf��
������ $��25��$��10q�$"��Upload Video From FTP Server��
�������